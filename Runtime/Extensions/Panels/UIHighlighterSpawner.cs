using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PhEngine.UI
{
    public class UIHighlighterSpawner : MonoBehaviour
    {
        public string uiHighlighterPanelId;
        public List<UIRectTransformRecord> highlightRecordList = new List<UIRectTransformRecord>();

        public void SpawnAndHighlightFromInspector(RectTransform rectTransform)
        {
            SpawnAndHighlightAt(rectTransform.transform.position,rectTransform);
        }
        
        public void SpawnAndHighlightOnWorldCanvasFromInspector(RectTransform rectTransform)
        {
            var panel = SpawnAndHighlightAt(rectTransform.transform.position,rectTransform);
            
            var screenPosition = ScreenPositionFinder.GetScreenPositionOfWorldCanvasRectTransform(rectTransform);
            panel.SetScreenPosition(screenPosition);
        }

        public UIHighlighterPanel SpawnAndHighlightAt(RectTransform rectTransform) =>
            SpawnAndHighlightAt(rectTransform.transform.position, rectTransform);
        
        public UIHighlighterPanel SpawnAndHighlightAt(Vector3 position, RectTransform rectTransform)
        {
            var panel = SpawnAndHighlightAt(position, rectTransform.rect.size);
            var record = new UIRectTransformRecord(rectTransform, panel);
            highlightRecordList.Add(record);
            return panel;
        }
        
        public UIHighlighterPanel SpawnAndHighlightAt(Vector3 position, Vector2 rectTransformSize)
        {
            var panel = SpawnHighlighterPanel();
            panel.Highlight(position , rectTransformSize);
            return panel;
        }
        
        UIHighlighterPanel SpawnHighlighterPanel()
        {
            return UIPanelManager.Instance.Spawn(uiHighlighterPanelId) as UIHighlighterPanel;
        }

        public void CloseHighlighterPanelAt(RectTransform rectTransform)
        {
            Debug.Log("Close!");
            foreach (var highlighterPanelRecord in highlightRecordList.Where(record=> record.targetRectTransform == rectTransform))
                highlighterPanelRecord.panel.Close();
        }
    }
}