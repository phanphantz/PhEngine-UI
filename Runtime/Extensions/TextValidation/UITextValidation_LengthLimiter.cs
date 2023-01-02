using UnityEngine;

namespace PhEngine.UI
{
    [CreateAssetMenu(menuName = "PhEngine/UI/TextValidation/LengthLimiter", fileName = "UITextValidation_LengthLimiter")]
    public class UITextValidation_LengthLimiter: UITextValidation
    {
        public bool isHasMinLimit;
        public int minLimit;
        public bool isHasMaxLimit;
        public int maxLimit;
        
        public override (bool isValid, string resultText) GetValidatedText(string textValue)
        {
            var isValid = !(isHasMinLimit && textValue.Length < minLimit);
            if (isHasMaxLimit)
                textValue = textValue.Substring(0, Mathf.Clamp( maxLimit, 0 , textValue.Length) );

            return (isValid, textValue);
        }
    }
}