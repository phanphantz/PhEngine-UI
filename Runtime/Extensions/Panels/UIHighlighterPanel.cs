using PhEngine.UI;
using UnityEngine;

namespace PhEngine.UI
{
    public class UIHighlighterPanel : UIPanel
    {
        [SerializeField]  UIElement highlighterElement;
        public UIElement HighlighterElement => highlighterElement;
        
        public void Highlight(Vector3 position, Vector2 rectTransformSize)
        {
            SetHighlighterRect(position, rectTransformSize);
            ShowHighlighter();
        }
        
        public void SetHighlighterRect(Vector3 position, Vector2 rectTransformSize)
        {
            highlighterElement.RectTransform.transform.position = position;
            highlighterElement.RectTransform.sizeDelta = rectTransformSize;
        }

        public void SetScreenPosition(Vector2 position)
        {
            highlighterElement.RectTransform.anchoredPosition = position;
        }
        
        public void ShowHighlighter()
        {
            highlighterElement.Show();
        }
        
        public void HideHighlighter()
        {
            highlighterElement.Hide();
        }
    }
    
}