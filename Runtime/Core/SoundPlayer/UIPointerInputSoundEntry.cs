using System;
using UnityEngine.EventSystems;

namespace PhEngine.UI
{
    [Serializable]
    public class UIPointerInputSoundEntry :  UIPointerInputEntry
    {
        public string[] soundIds;
        public UIPointerInputSoundEntry(EventTriggerType inputType, params string[] soundIds) : base(inputType)
        {
            this.soundIds = soundIds;
        }
        
    }
}