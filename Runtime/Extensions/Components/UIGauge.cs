using UnityEngine;
using UnityEngine.UI;

namespace PhEngine.UI
{
    [RequireComponent(typeof(Slider))]
    public class UIGauge : UITextAndIcon
    {
        Slider unsafeSlider;
        public Slider Slider
        {
            get
            {
                if (unsafeSlider == null)
                    unsafeSlider = GetComponent<Slider>();

                return unsafeSlider;
            }
        }

        public float CurrentScale => currentScale;
        [Range(0,1f)] [SerializeField] float currentScale;
        
        public void SetFill(int current, int max, UITextAndIconData contentData = null) =>
            SetFill((float) current, (float) max, contentData);

        public void SetFill(float current, float max, UITextAndIconData contentData = null) =>
            SetFill(max == 0 ? 0 : current / max, contentData);

        public void SetFill(float scale, UITextAndIconData textAndIconDataToOverride = null)
        {
            currentScale =  Mathf.Clamp01(scale);
            Slider.minValue = 0f;
            Slider.maxValue = 1f;
            Slider.value = currentScale;

            if (textAndIconDataToOverride != null)
                SetData(textAndIconDataToOverride);
        }

        void Repaint()
        {
            SetFill(currentScale);
        }

        void OnValidate()
        {
            Repaint();
        }
    }
}