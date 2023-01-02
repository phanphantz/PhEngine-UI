using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PhEngine.UI
{
    public class UIPanelHolder : MonoBehaviour
    {
        [SerializeField] List<UIPanelHistory> panelHistoryList = new List<UIPanelHistory>();
        
        public void AddPanel(UIPanel mainPanel , string loadId , string historyTag)
        {
            var panelData = new UIPanelHistory(loadId, historyTag, mainPanel.DimmerPanel, mainPanel);
        }
        
        public void CloseTopMostPanelIfBackInputIsAllow(bool isRemoveHistory)
        {
            var topMostPanel = panelHistoryList.LastOrDefault();
            if (topMostPanel == null)
                return;

            if (topMostPanel.mainPanel == null)
                return;

            if (topMostPanel.mainPanel.IsTryCloseOnBackInput)
                CloseTopMostPanel(isRemoveHistory);
        }

        public void CloseTopMostPanel(bool isRemoveHistory)
        {
            var topMostPanel = panelHistoryList.LastOrDefault();
            if (topMostPanel == null)
                return;

            ClosePanelAtHistory(topMostPanel , isRemoveHistory);
        }

        public void ClosePanelAtHistory(UIPanelHistory panelHistory, bool isRemoveHistory)
        {
            if (isRemoveHistory)
                Remove(panelHistory);
            
            panelHistory.Close();
        }

        public void RemovePanel(UIPanel panelToRemove)
        {
            foreach (var panelHistory in panelHistoryList)
            {
                if (panelHistory.mainPanel == panelToRemove)
                {
                    Remove(panelHistory);
                    return;
                }
            }
        }

        void Remove(UIPanelHistory history)
        {
            panelHistoryList.Remove(history);
        }

        public void CloseAllPanelsWithId(string historyId, bool isRemoveHistory)
        {
            var targets = GetAllHistoriesWithId(historyId);
            if (targets == null)
                return;
            
            CloseAllPanelOnHistory(isRemoveHistory, targets);
        }

        public void CloseAllPanelOnHistory(bool isRemoveHistory, params UIPanelHistory[] targets)
        {
            foreach (var target in targets)
            {
                ClosePanelAtHistory(target, isRemoveHistory);
            }
        }

        public UIPanel[] GetAllPanelsWithHistoryId(string historyId)
        {
            var targets = GetAllHistoriesWithId(historyId);
            var result = GetPanelsFromHistoriesById(targets , historyId);
            return result.ToArray();
        }

        public static UIPanel[] GetPanelsFromHistoriesById(UIPanelHistory[] targets, string historyId)
        {
            List<UIPanel> result = new List<UIPanel>();
            foreach (var history in targets)
            {
                if (history.id  == historyId && history.mainPanel)
                    result.Add(history.mainPanel);
            }

            return result.ToArray();
        }

        public UIPanelHistory[] GetAllHistoriesWithId(string historyId)
        {
            return panelHistoryList.Where(o => o.id == historyId).ToArray();
        }

        public void CloseAllPanelsWithTag(string historyTag, bool isRemoveHistory)
        {
            var targets = GetAllHistoriesWithTag(historyTag);
            if (targets == null)
                return;

            CloseAllPanelOnHistory(isRemoveHistory, targets);
        }

        public UIPanel[] GetAllPanelsWithTag(string historyTag)
        {
            var targets = GetAllHistoriesWithTag(historyTag);
            var result = GetPanelsFromHistoriesByTag(targets, historyTag);
            return result;
        }

        public static UIPanel[] GetPanelsFromHistoriesByTag(UIPanelHistory[] targets, string historyTag)
        {
            List<UIPanel> result = new List<UIPanel>();
            foreach (var history in targets)
            {
                if (history.tag == historyTag && history.mainPanel)
                    result.Add(history.mainPanel);
            }

            return result.ToArray();
        }

        public UIPanelHistory[] GetAllHistoriesWithTag(string historyTag)
        {
            return panelHistoryList.Where(o => o.tag == historyTag).ToArray();
        }
    }

}