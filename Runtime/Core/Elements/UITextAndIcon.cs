using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PhEngine.UI
{
    public class UITextAndIcon : UIElement
    {
        [Header("Text And Icon")]
        [SerializeField] TMP_Text text;
        public TMP_Text Text => text;
        
        [SerializeField] Image iconImage;
        public Image IconImage => iconImage;

        public void SetData(UITextAndIconData data)
        {
            if (data == null)
                return;
            
            SetText(data.Text);
            SetIcon(data.Icon);
        }
        
        public void SetIcon(Sprite sprite)
        {
            if (iconImage == null)
                return;

            iconImage.sprite = sprite;
            iconImage.gameObject.SetActive(sprite);
        }

        public void SetText(string message)
        {
            if (text == null)
                return;

            var isValid = !string.IsNullOrEmpty(message);
            text.text = isValid ? message : string.Empty;
            text.gameObject.SetActive(isValid);
        }
    }
}