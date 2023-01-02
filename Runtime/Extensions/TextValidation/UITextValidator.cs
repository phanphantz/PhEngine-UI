using System;
using System.Linq;
using UnityEngine;

namespace PhEngine.UI
{
    public abstract class UITextValidator : MonoBehaviour
    {
        [SerializeField] bool isAutoValidateOnTextChange = false;
        [SerializeField] bool isAutoValidateOnEnable = true;
        [SerializeField] UITextValidation[] validationsInOrder;

        [Header("Info")]
        [SerializeField] string lastKnownTextValue;
        public string LastKnownTextValue => lastKnownTextValue;
        
        [SerializeField] bool isCurrentTextValid;
        public bool IsCurrentTextValid => isCurrentTextValid;

        void OnEnable()
        {
            if (isAutoValidateOnEnable)
                Validate();
        }

        public (bool isResultFullyValid, string resultText) GetValidatedText(string textValue)
        {
            if (string.IsNullOrEmpty(textValue))
                return (true, textValue);
            
            var isValid = true;
            if (validationsInOrder == null)
                return (true , textValue);

            foreach (var textValidation in validationsInOrder)
            {
                var (isTextValidationValid, resultText) = textValidation.GetValidatedText(textValue);
                if (!isTextValidationValid)
                    isValid = false;

                textValue = resultText;
            }

            return (isValid , textValue);
        }
        
        public bool TryValidate()
        {
            var isValid = Validate();
            return isValid;
        }

        bool Validate()
        {
            var (isValid, resultText) = GetValidatedText(GetCurrentText());
            isCurrentTextValid = isValid;
            SetText(resultText);
            return isValid;
        }

        void Update()
        {
            if (!isAutoValidateOnTextChange)
                return;

            var currentText = GetCurrentText();
            if (lastKnownTextValue.Equals(currentText))
                return;

            Validate();
            lastKnownTextValue = currentText;
        }

        public abstract string GetCurrentText();
        public abstract void SetText(string textValue);
    }
}