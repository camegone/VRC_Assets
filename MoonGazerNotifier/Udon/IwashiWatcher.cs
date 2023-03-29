
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;


using HoshinoLabs.IwaSync3.Udon;

namespace camegone.MoonGazerNotifier
{
    public class IwashiWatcher : VideoControllerEventListener
    {
        [SerializeField] Sender _sender;
        [SerializeField] VideoCore _videocore;
        [SerializeField] float _interval = 1.0f;

        private float _timerCount = 0.0f;
        private bool _isUrlChanged = false;
        private string _messageOld = "";

        const string _title = "Iwasync3VideoPlayer";

        void Start() 
        {
            if (_sender == null)
                _sender = this.GetComponent<Sender>();
            if (_videocore == null)
                _videocore = GameObject.Find("Udon (VideoCore)").GetComponent<VideoCore>();

            _videocore.AddListener(this);
        }

        public override void OnChangeURL()
        {
            _isUrlChanged = true;
        }

        private void Update()
        {
            if (_isUrlChanged && _videocore.isPlaying)
            {
                if (_interval <= _timerCount)
                {
                    _timerCount = 0.0f;
                    if (!_messageOld.Equals(_videocore.Message))
                    {
                        OnChangeVideo();
                    }
                }
                else
                {
                    _timerCount += Time.deltaTime;
                }
            }
        }

        private void OnChangeVideo()
        {
            _sender.SendNotification("Now Playing", FormattedMessage());
            _messageOld = _videocore.Message;
            _isUrlChanged = false;
            _timerCount = 0.0f;
        }

        private string FormattedMessage()
        {
            if (_videocore.Message == "")
                return _videocore.url.ToString();
            else
            {
                return _videocore.Message;
            }
        }

    }
}
