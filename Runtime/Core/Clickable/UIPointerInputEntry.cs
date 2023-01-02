using System;
using UnityEngine.EventSystems;

namespace PhEngine.UI
{
    [Serializable]
    public abstract class UIPointerInputEntry
    {
        public EventTriggerType inputType;

        protected UIPointerInputEntry(EventTriggerType inputType)
        {
            this.inputType = inputType;
        }

    }
}