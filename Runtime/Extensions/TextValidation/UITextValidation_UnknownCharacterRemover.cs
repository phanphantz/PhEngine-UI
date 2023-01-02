using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace PhEngine.UI
{
    [CreateAssetMenu(menuName = "PhEngine/UI/TextValidation/UnknownCharacterRemover", fileName = "UITextValidation_UnknownCharacterRemover", order = 0)]
    public class UITextValidation_UnknownCharacterRemover: UITextValidation
    {
        public override (bool isValid, string resultText) GetValidatedText(string textValue)
        {
            return (true, CleanInput(textValue));
        }
        
        static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            try {
                return Regex.Replace(strIn, @"[^\w\.@-]", "",
                    RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters,
            // we should return Empty.
            catch (RegexMatchTimeoutException) {
                return string.Empty;
            }
        }
    }
    
   
}