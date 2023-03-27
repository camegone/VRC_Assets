
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
        public MeshRenderer _darkener;
        public Slider _bright;
        public Slider _red;
        public Slider _green;
        public Slider _blue;

        void Start()
        {
            UpdateColor();
        }

        public void UpdateColor() {
            _darkener.material.SetColor("_UdonDarkenerColor", _tint * VecBright(_brightness));
        }

        Color VecBright(float b)
        {
            return new Color(b, b, b, 1);
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