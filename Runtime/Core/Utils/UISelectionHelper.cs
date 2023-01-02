using UnityEngine.EventSystems;

namespace PhEngine.UI
{
    public static class UISelectionHelper
    {
        public static void DeselectAllUI()
        {
            if (EventSystem.current)
                EventSystem.current.SetSelectedGameObject(null);
        }
    }
}