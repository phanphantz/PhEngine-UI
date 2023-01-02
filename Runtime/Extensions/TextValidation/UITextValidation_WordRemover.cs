using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace PhEngine.UI
{
    [CreateAssetMenu(menuName = "PhEngine/UI/TextValidation/WordRemover", fileName = "UITextValidation_WordRemover", order = 0)]
    public class UITextValidation_WordRemover : UITextValidation
    {
        public bool isCaseSensitive = false;
        [SerializeField] TextAsset wordsToRemoveTextAsset;
        public override (bool isValid, string resultText) GetValidatedText(string textValue)
        {
            var words = Regex.Split(wordsToRemoveTextAsset.text, "\\\r");
            foreach (var word in words)
            {
                var actualWord = word.Trim();
                if (isCaseSensitive)
                {
                    textValue = textValue.Replace(actualWord, "");
                }
                else
                {
                   textValue = Regex.Replace(textValue, Regex.Escape(actualWord), "".Replace("$","$$"), RegexOptions.IgnoreCase);
                }
                
            }
            
            return (true, textValue);
        }
    }
}