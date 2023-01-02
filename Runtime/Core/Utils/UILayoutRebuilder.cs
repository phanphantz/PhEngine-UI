using UnityEngine;
using UnityEngine.UI;

namespace PhEngine.UI
{
    public static class UILayoutRebuilder
    {
        public static void Rebuild(RectTransform rectTransform)
        {
            if (rectTransform == null || !rectTransform.gameObject.activeSelf)
                return;
     
            RebuildAllChildRectTransform(rectTransform);
            RebuildRectTransform(rectTransform);
        }

        static void RebuildRectTransform(RectTransform rectTransform)
        {
            var layoutGroup = rectTransform.GetComponent<LayoutGroup>();
            var contentSizeFitter = rectTransform.GetComponent<ContentSizeFitter>();
            
            RefreshLayoutGroup(layoutGroup);
            RebuildContentSizeFitter(rectTransform, contentSizeFitter);
        }

        static void RebuildContentSizeFitter(RectTransform rectTransform, ContentSizeFitter contentSizeFitter)
        {
            if (contentSizeFitter != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }

        static void RefreshLayoutGroup(LayoutGroup layoutGroup)
        {
            if (layoutGroup == null) 
                return;
            
            layoutGroup.SetLayoutHorizontal();
            layoutGroup.SetLayoutVertical();
        }

        static void RebuildAllChildRectTransform(RectTransform rectTransform)
        {
            foreach (RectTransform child in rectTransform)
                Rebuild(child);
        }
    }
}