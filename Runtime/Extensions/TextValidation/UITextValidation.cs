using UnityEngine;
using UnityEngine.Serialization;

namespace PhEngine.UI
{
    public abstract class UITextValidation : ScriptableObject
    {
        public abstract (bool isValid, string resultText) GetValidatedText(string textValue);
    }
}