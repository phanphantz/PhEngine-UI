using System;
using UnityEngine;

namespace PhEngine.UI
{
    [Serializable]
    public class HighlighterPanelRecord
    {
        public RectTransform targetRectTransform;
        public UIHighlighterPanel highlighterPanel;

        public HighlighterPanelRecord(RectTransform targetRectTransform, UIHighlighterPanel highlighterPanel)
        {
            this.targetRectTransform = targetRectTransform;
            this.highlighterPanel = highlighterPanel;
        }
    }
}