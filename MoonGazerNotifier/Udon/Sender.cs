
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace camegone.MoonGazerNotifier
{
    public class Sender : UdonSharpBehaviour
    {
        [SerializeField] private LWNController _LWN;
        [SerializeField] private bool _muteNotification = true;
        [SerializeField] private AudioSource _sourceToMute;
        

        private readonly float _waitTime = 2.0f;
        private float _timerCount = 0.0f;
        private bool _isMuted = false;

        void Start() 
        {
            if (_LWN == null)
                _LWN = GameObject.Find("LunarWorldNotification").GetComponent<LWNController>();
            if (_sourceToMute == null && _muteNotification)
                _sourceToMute = GameObject.Find("LunarWorldNotification").GetComponent<AudioSource>();
        }

        void Update() 
        {
            if (_isMuted)
            {
                if (_timerCount >= _waitTime)
                {
                    _timerCount = 0.0f;
                    _isMuted = false;
                    _sourceToMute.enabled = true;
                }
                else
                {
                    _timerCount += Time.deltaTime;
                }
            }

        }
        

        public void SendNotification(string title, string message)
        {
            if (_muteNotification)
            {
                _sourceToMute.enabled = false;
                _isMuted = true;
            }
            _LWN.PlayCustomNotification(title, ExcludeTags(message));
        }

        private readonly string[] simpleTags = { "<b>", "<i>", "</b>", "</i>", "</size>", "</color>", "</material>" };
        private readonly string[] complexTags = { "<size=", "<color=", "<material=" , "<quad " };

        private string ExcludeTags(string tex) // remove rich text tags
        {
            foreach (string tag in simpleTags) // remove simple tags
            {
                tex = tex.Replace(tag, "");
            }

            foreach (string tag in complexTags)
            {
                int head = 0;
                for (int s = tex.IndexOf(tag, head); s >= 0; s = tex.IndexOf(tag, head))
                {
                    head = s + 1;
                    int e = tex.IndexOf(">", head);
                    if (e < 0)
                        continue;
                    else
                    {
                        tex = tex.Remove(s, e - s + 1);
                        head = 0;
                    }
                }
            }

            return tex;
        }
    }
}
