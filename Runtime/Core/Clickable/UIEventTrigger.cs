using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PhEngine.UI
{
    [RequireComponent(typeof(EventTrigger))]
    public class UIEventTrigger : UIPointerInputListener
    {
        EventTrigger eventTrigger;
        public EventTrigger EventTrigger
        {
            get
            {
                if (eventTrigger == null)
                    eventTrigger = GetComponent<EventTrigger>();

                return eventTrigger;
            }
        }
        
        protected override void HandleOnInteractableChange()
        {
            EventTrigger.enabled = IsInteractable;
        }
        
        protected override void RemoveCallbackInComponent()
        {
            EventTrigger.triggers.Clear();
        }

        protected override void AddCallbackToComponent()
        {
            TryAddOnClickEventToEventTrigger();
            AddAllInputEntryToEventTrigger();
        }

        protected override void AddPlaySoundCallbackToComponent()
        {
            if (SoundPlayer == null)
                return;

            var soundEntries = SoundPlayer.soundEntryList;
            if (soundEntries == null || soundEntries.Count == 0)
                return;
            
            var entriesWithDistinctInputType = soundEntries.Select(e => e.inputType).Distinct();
            foreach (var eventTriggerType in entriesWithDistinctInputType)
                AddPlaySoundOfInputTypeCallback(eventTriggerType);
            
            void AddPlaySoundOfInputTypeCallback(EventTriggerType inputType)
            {
                var playSoundEventTriggerEntry = CreateEventTriggerEntry(inputType,
                    () => { PlaySoundOfInputType(inputType); }
                );
                
                EventTrigger.triggers.Add(playSoundEventTriggerEntry);
            }
        }

        void TryAddOnClickEventToEventTrigger()
        {
            if (OnClick == null)
                return;

            var onClickEntry = CreateOnClickEntry();
            EventTrigger.triggers.Add(onClickEntry);
        }

        EventTrigger.Entry CreateOnClickEntry()
        {
            var onClickEntry = CreateEventTriggerEntry(EventTriggerType.PointerClick, OnClick);
            return onClickEntry;
        }

        EventTrigger.Entry CreateEventTriggerEntry(EventTriggerType inputType, Action action)
        {
            var onClickEntry = new EventTrigger.Entry();
            onClickEntry.eventID = inputType;
            onClickEntry.callback = new EventTrigger.TriggerEvent();
            onClickEntry.callback.AddListener
            (
                (data) => { HandleOnExecuteAction(action); }
            );
            return onClickEntry;
        }

        static void HandleOnExecuteAction(Action action)
        {
            action?.Invoke();
        }

        void AddAllInputEntryToEventTrigger()
        {
            foreach (var entry in inputEntryList)
            {
                EventTrigger.triggers.Add(entry.CreateEventTriggerEntry());
            }
        }

        List<UIPointerInputEventEntry> inputEntryList = new List<UIPointerInputEventEntry>();

        public List<UIPointerInputEventEntry> InputEntryList
        {
            get => inputEntryList;
            set
            {
                if (inputEntryList == value)
                    return;

                SetInputEntryList(value);
            }
        }

        void SetInputEntryList(List<UIPointerInputEventEntry> entryList)
        {
            inputEntryList = entryList;
            ValidateCallbacks();
        }
        
    }
}