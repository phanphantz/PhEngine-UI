using UnityEngine;

namespace PhEngine.UI
{
    public class UITextAndIconData
    {
        public Sprite Icon;
        public string Text;

        public UITextAndIconData(string text= null, Sprite icon = null)
        {
            Icon = icon;
            Text = text;
        }
        
    }
}