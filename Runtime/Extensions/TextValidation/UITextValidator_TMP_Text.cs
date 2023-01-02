using System;
using TMPro;
using UnityEngine;

namespace PhEngine.UI
{
    public class UITextValidator_TMP_Text : UITextValidator
    {
        [SerializeField] TMP_Text text;
        public override string GetCurrentText()
        {
            return text.text;
        }

        public override void SetText(string textValue)
        {
            text.text = textValue;
        }
        
    }
}