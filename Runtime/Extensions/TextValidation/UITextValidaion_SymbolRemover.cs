using System.Text.RegularExpressions;
using UnityEngine;

namespace PhEngine.UI
{
    [CreateAssetMenu(menuName = "PhEngine/UI/TextValidation/SymbolRemover", fileName = "UITextValidation_SymbolRemover", order = 0)]
    public class UITextValidaion_SymbolRemover : UITextValidation
    {
        [SerializeField] char[] symbolsToRemove;
        public override (bool isValid, string resultText) GetValidatedText(string textValue)
        {
            foreach (var c in symbolsToRemove)
            {
                textValue = textValue.Replace(c.ToString(), "");
            }
            
            return (true, textValue);
        }
    }
}