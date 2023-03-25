
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using HoshinoLabs.IwaSync3.Udon;
using UnityEngine.UI;

namespace hiscan.udon
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)] public class hiscan : VideoControllerEventListener
    {
        public VideoCore core;
        public Text text;
        public float interval = 5.0f;
        private float _timerCount = 0.0f;
        private bool _isUrlChanged = false;
        private string _messageOld = "";

        private void Start()
        {
            text.text = "Udoooon!!";
            core.AddListener(this);
        }

        public override void OnChangeURL()
        {
            _isUrlChanged = true;
        }

        private void Update()
        {
            if (_isUrlChanged && core.isPlaying) 
            {
                if (interval <= _timerCount)
                {
                    _timerCount = 0.0f;
                    if (!_messageOld.Equals(core.Message))
                    {
                        UpdateText();
                    }
                }
                else
                {
                    _timerCount += Time.deltaTime;
                }
            }
        }

        private void UpdateText()
        {
            text.text = $"<color=silver>[{System.DateTime.Now.ToString()}]</color>\n" +
                $"Title:{core.Message}\n" +
                $"<color=teal>URL:{core.url.ToString()}</color>\n\n" +
                $"{text.text}";
            _messageOld = core.Message;
            _isUrlChanged = false;
        }

    }
}