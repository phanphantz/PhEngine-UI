using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PhEngine.UI
{
    [RequireComponent(typeof(Button))]
    public class UIButton : UIPointerInputListener
    {
        Button button;
        public Button Button
        {
            get
            {
                if (button == null)
                    button = GetComponent<Button>();

                return button;
            }
        }
        
        protected override void HandleOnInteractableChange()
        {
            Button.interactable = IsInteractable;
        }
        
        protected override void RemoveCallbackInComponent()
        {
            Button.onClick.RemoveAllListeners();
        }

        protected override void AddCallbackToComponent()
        {
            Button.onClick.AddListener(HandleOnClick);
        }

        void HandleOnClick()
        {
            OnClick?.Invoke();
        }

        protected override void AddPlaySoundCallbackToComponent()
        {
            Button.onClick.AddListener
            (
                () => { PlaySoundOfInputType(EventTriggerType.PointerClick); }
            );
        }
    }

}