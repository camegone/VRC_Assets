
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using UnityEngine.UI;

namespace camegone.Darkener
{
    public class Darkener : UdonSharpBehaviour
    {
        public float _brightness = 1.0f;
        public Color _tint = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        [SerializeField] private Slider _bright;
        [SerializeField] private Slider _red;
        [SerializeField] private Slider _green;
        [SerializeField] private Slider _blue;

        [SerializeField] private MeshRenderer _darkener;

        private Color white = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        void Start()
        {
            UpdateColor();
        }

        public void UpdateColor() {
            UpdateRenderer();
            _darkener.material.SetColor("_UdonDarkenerColor", _tint * VecBright(_brightness));
        }

        Color VecBright(float b)
        {
            return new Color(b, b, b, 1.0f);
        }

        void UpdateRenderer()
        {
            if (_brightness >= 1.0f && _tint == white)
                _darkener.enabled = false;
            else
                _darkener.enabled = true;
        }

        float GetSliderVal(Slider slider) 
        {
            return slider.value;
        }

        public void OnBrightnessChanged()
        {
            _brightness = GetSliderVal(_bright);
            UpdateColor();
        }

        public void OnRedChanged()
        {
            _tint.r = GetSliderVal(_red);
            UpdateColor();
        }

        public void OnGreenChanged()
        {
            _tint.g = GetSliderVal(_green);
            UpdateColor();
        }

        public void OnBlueChanged()
        {
            _tint.b = GetSliderVal(_blue);
            UpdateColor();
        }

    }
}