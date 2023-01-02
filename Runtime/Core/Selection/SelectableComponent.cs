using System;
using UnityEngine;

namespace PhEngine.UI
{
    public class SelectableComponent : MonoBehaviour
    {
        public bool IsSelected { get; private set; }
        public event Action OnSelect;
        public event Action OnDeselect;

        public void Deselect()
        {
            if (!IsSelected)
                return;
            
            IsSelected = false;
            HandleOnDeselect();
        }

        protected virtual void HandleOnDeselect()
            => OnDeselect?.Invoke();

        public void Select()
        {
            if (IsSelected)
                return;

            IsSelected = true;
            HandleOnSelect();
        }

        protected virtual void HandleOnSelect()
            => OnSelect?.Invoke();

        public void ToggleSelection()
        {
            if (IsSelected)
                Deselect();
            else
                Select();
        }
    }
}