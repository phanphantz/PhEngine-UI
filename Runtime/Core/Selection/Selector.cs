using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace PhEngine.UI
{
    public class Selector : MonoBehaviour
    {
        [Header("Settings")]
        public bool isAllowSelectExistingElement;
        public List<SelectionCategory> categoryList = new List<SelectionCategory>();

        [SerializeField] List<SelectableComponent> selectionList = new List<SelectableComponent>();

        public Action<string> onCategoryChange;
        
        public void SelectOnlyOne(SelectableComponent target)
        {
            if (!IsSelectionRequestValid(target))
                return;
            
            DeselectAndRemoveAllElements();
            SelectAndAdd(target);
        }

        bool IsSelectionRequestValid(SelectableComponent target)
        {
            return !target.IsSelected || isAllowSelectExistingElement;
        }
        
        public void ReplaceWith(SelectableComponent element) =>  ReplaceWith(new SelectableComponent[] {element});
        public void ReplaceWith(params SelectableComponent[] elementsToReplace)
        {
            if (!IsListNullOrEmpty())
                DeselectAndRemove(selectionList.Where(element=> !elementsToReplace.Contains(element)).ToArray());
            
            SelectAndAdd(elementsToReplace);
        }

        bool IsListNullOrEmpty()
        {
            return selectionList == null || selectionList.Count == 0;
        }
        
        public void ToggleSelection(SelectableComponent target)
        {
            if (target.IsSelected)
                DeselectAndRemove(target);
            else
                SelectAndAdd(target);
        }
        
        public void SelectAndAdd(SelectableComponent element) => SelectAndAdd(new SelectableComponent[] {element});
        public void SelectAndAdd(params SelectableComponent[] elementsToSelect)
        {
            Select(elementsToSelect);

            var confirmedSelectedElements = GetValidatedElementsToAdd();
            selectionList.AddRange(confirmedSelectedElements);
            TryNotifySelectCategory(confirmedSelectedElements);

            SelectableComponent[] GetValidatedElementsToAdd()
            {
                return elementsToSelect.Where(e=> isAllowSelectExistingElement || e.IsSelected ).ToArray();
            }
        }
        
        public void TryNotifySelectCategory(params SelectableComponent[] targets)
        {
            if (categoryList == null)
                return;
            
            foreach (var target in targets)
                TryNotifySelectCategory(target);
        }

        public void TryNotifySelectCategory(SelectableComponent target)
        {
            if (categoryList == null)
                return;
            
            foreach (var reference in categoryList)
            {
                if (reference.linkedTarget != target)
                    continue;
                
                onCategoryChange?.Invoke(reference.categoryId);
            }
        }
        
        void Select(params SelectableComponent[] elementsToSelect)
        {
            foreach (var obj in elementsToSelect)
            {
                if (!IsSelectionRequestValid(obj))
                    continue;
                
                obj.Select();
            }
        }

        public void DeselectAndRemove(SelectableComponent element) => DeselectAndRemove(new SelectableComponent[] {element});
        public void DeselectAndRemove(params SelectableComponent[] elementsToRemove)
        {
            Deselect(elementsToRemove);
        }

        void Deselect(params SelectableComponent[] elementsToDeselect)
        {
            foreach (var obj in elementsToDeselect)
                obj.Deselect();
        }

        public void DeselectAndRemoveAllElements()
        {
            Deselect(selectionList.ToArray());
            selectionList.Clear();
        }
    }
}