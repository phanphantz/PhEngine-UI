using System;
using UnityEngine;

namespace PhEngine.UI
{
    public class UIPointerInputListenerData : UITextAndIconData
    {
        public Action OnClick { get; }
        
        public UIPointerInputListenerData(string text= null, Sprite icon = null, Action onClick = null)  : base(text , icon)
        {
            OnClick = onClick;
        }
    }
}