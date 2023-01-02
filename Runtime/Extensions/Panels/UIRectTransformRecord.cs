using System;
using UnityEngine;

namespace PhEngine.UI
{
    [Serializable]
    public class UIRectTransformRecord
    {
        public RectTransform targetRectTransform;
        public UIPanel panel;

        public UIRectTransformRecord(RectTransform targetRectTransform, UIPanel panel)
        {
            this.targetRectTransform = targetRectTransform;
            this.panel = panel;
        }
    }
}