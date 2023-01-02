namespace PhEngine.UI
{
    [System.Serializable]
    public class UIPanelHistory
    {
        public string id;
        public string tag;
        public UIPanel dimmerPanel;
        public UIPanel mainPanel;

        public UIPanelHistory(string id, string tag , UIPanel dimmerPanel, UIPanel mainPanel)
        {
            this.id = id;
            this.tag = tag;
            this.dimmerPanel = dimmerPanel;
            this.mainPanel = mainPanel;
        }

        public void Close()
        {
            CloseSelfAndLoadedDimmerPanel(mainPanel);
        }

        private static void CloseSelfAndLoadedDimmerPanel(UIPanel panel)
        {
            if (panel)
                panel.Close();
        }
    }
}