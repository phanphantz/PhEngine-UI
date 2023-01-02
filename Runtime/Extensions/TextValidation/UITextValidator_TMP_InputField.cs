using System;
using TMPro;
using UnityEngine;

namespace PhEngine.UI
{
    public class UITextValidator_TMP_InputField : UITextValidator
    {
        [SerializeField] TMP_InputField inputField;
        public override string GetCurrentText()
        {
            return inputField.text;
        }

        public override void SetText(string textValue)
        {
            inputField.text = textValue;
        }
        
    }
}