using System.Linq;
using UnityEngine;

namespace PhEngine.UI
{
    [CreateAssetMenu(menuName = "PhEngine/UI/TextValidation/SpaceRemover", fileName = "UITextValidation_SpaceRemover",
        order = 0)]
    public class UITextValidation_SpaceRemover : UITextValidation
    {
        public override (bool isValid, string resultText) GetValidatedText(string textValue)
        {
            return (true, string.Concat(textValue.Where(c => !char.IsWhiteSpace(c))));
        }
    }
}