using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace PhEngine.UI
{
    [Serializable]
    public class UIPointerInputEventEntry :  UIPointerInputEntry
    {
        public UnityEvent<PointerEventData> onDetect;

        public UIPointerInputEventEntry(EventTriggerType inputType, UnityEvent<PointerEventData> onDetect) : base(inputType)
        {
            this.onDetect = onDetect;
        }

        public EventTrigger.Entry CreateEventTriggerEntry()
        {
            var entry = new EventTrigger.Entry();
            entry.eventID = inputType;
            entry.callback = new EventTrigger.TriggerEvent();
            entry.callback.AddListener((BaseEventData data) => CallOnDetectEvent(data as PointerEventData));
            return entry;
        }
        
        public UnityAction CreateButtonClickedAction()
        {
            if (inputType != EventTriggerType.PointerClick)
                return null;
            
            return ()=> CallOnDetectEvent(null);
        }
        
        private void CallOnDetectEvent(PointerEventData data)
        {
            onDetect?.Invoke(data);
        }
        
    }
}