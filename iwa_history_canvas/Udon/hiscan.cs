
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
        [SerializeField] private VideoCore _core;
        [SerializeField] private Text _text;
        [SerializeField] private float _interval = 5.0f;
        [SerializeField] private string _initialText = "Udoooon!!";
        private float _timerCount = 0.0f;
        private bool _isUrlChanged = false;

        private void Start()
        {
            if (_core == null)
                _core = GameObject.Find("Udon (VideoCore)").GetComponent<VideoCore>();
            if (_text == null)
                _text = this.GetComponent<Text>();

            _text.text = _initialText;
            _core.AddListener(this);
        }

        public override void OnChangeURL()
        {
            _isUrlChanged = true;
        }

        private void Update()
        {
            if (_isUrlChanged && _core.isPlaying) 
            {
                if (_interval <= _timerCount)
                {
                    _timerCount = 0.0f;
                    UpdateText();
                }
                else
                {
                    _timerCount += Time.deltaTime;
                }
            }
        }

        private void UpdateText()
        {
            _text.text = $"<color=silver>[{System.DateTime.Now.ToString()}]</color>\n" +
                $"Title:{_core.Message}\n" +
                $"<color=teal>URL:{_core.url.ToString()}</color>\n\n" +
                $"{_text.text}";
            _timerCount = 0.0f;
            _isUrlChanged = false;
        }

    }
}