using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PhEngine.UI
{
    public abstract class UIPointerInputListener : UITextAndIcon
    {
        protected abstract void AddCallbackToComponent();
        protected abstract void RemoveCallbackInComponent();
        protected abstract void AddPlaySoundCallbackToComponent();

        [SerializeField] bool isInteractable = true;
        public bool IsInteractable
        {
            get => isInteractable;
            set
            {
                if (isInteractable == value)
                    return;

                SetInteractable(value);
            }
        }

        void SetInteractable(bool value)
        {
            isInteractable = value;
            HandleOnInteractableChange();
        }

        protected abstract void HandleOnInteractableChange();
        
        [SerializeField] UISoundPlayer soundPlayer;
        public UISoundPlayer SoundPlayer => soundPlayer;

        protected void PlaySoundOfInputType(EventTriggerType inputType)
        {
            if (soundPlayer == null)
                return;

            soundPlayer.Play(inputType);
        }

        Action onClick;

        public Action OnClick
        {
            get => onClick;
            set
            {
                onClick = value;
                ValidateCallbacks();
            }
        }

        protected void ValidateCallbacks()
        {
            RemoveCallbackInComponent();
            AddPlaySoundCallbackToComponent();
            AddCallbackToComponent();
        }

        protected virtual void Awake()
        {
            AddPlaySoundCallbackToComponent();
            ValidateInteractableStatus();
        }

        public void SetData(UIPointerInputListenerData data)
        {
            OnClick += data.OnClick;
            SetData(data as UITextAndIconData);
        }

        void OnValidate()
        {
            ValidateInteractableStatus();
        }

        void ValidateInteractableStatus()
        {
            if (!Application.isPlaying)
                return;

            HandleOnInteractableChange();
        }
    }
}