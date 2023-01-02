using PhEngine.Core;

namespace PhEngine.UI
{
    public static class UIPanelInitializer
    {
        static UIPanel panel;
        static UIPanelConfig config;

        public static void Init(UIPanel target, UIPanelConfig config, UIPanelLayerConfig layer = null)
        {
            panel = target;
            UIPanelInitializer.config = config;
            Init(layer);
        }

        static void Init(UIPanelLayerConfig layer = null)
        {
            layer = TryGetDefaultLayer(layer);
            TryInitDimmerPanel(layer);
            TryInitSelfSorting(layer);
        }

        static UIPanelLayerConfig TryGetDefaultLayer(UIPanelLayerConfig layer)
        {
            if (layer == null && config)
                layer = config.defaultSortingLayer;
            
            return layer;
        }

        static void TryInitDimmerPanel(UIPanelLayerConfig layer)
        {
            if (config == null)
                return;
            
            var dimmerPrefab = config.dimmerPanelPrefab;
            if (!dimmerPrefab) 
                return;
            
            var loadedDimmer = SpawnAndInitDimmer(dimmerPrefab , layer);
            panel.SetDimmerPanel(loadedDimmer);
        }

        static void TryInitSelfSorting(UIPanelLayerConfig layer)
        {
            if (config == null)
            {
                panel.BringToFront();
                return;
            }
            
            switch (config.sortCanvasOnInitMethod)
            {
                case UIPanelSortingMethod.Off:
                    return;
                case UIPanelSortingMethod.UseDefaultSortingLayer:
                    TrySetCanvasSortOrderFromPanelSorter(layer);
                    break;
                case UIPanelSortingMethod.BringToFront:
                    panel.BringToFront();
                    break;
            }

            SortCanvasToTopMostInTheAssignedSortingOrder();
            panel.transform.SetAsLastSibling();
        }

        public static void SortCanvasToTopMostInTheAssignedSortingOrder()
        {
            if (panel.Canvas == null)
                return;
            
            //Doing these 2 lines make the canvas goes up to top most sorting order 
            panel.Canvas.enabled = false;
            panel.Canvas.enabled = true;
        }

        static void TrySetCanvasSortOrderFromPanelSorter(UIPanelLayerConfig layer)
        {
            if (UIPanelManager.Instance)
                UIPanelManager.Instance.Sort(panel, layer);
            else
                PhDebug.LogWarning<UIPanel>("Cannot sort panel : " + panel.gameObject.name + " by config. UIPanelSorter is missing.");
        }

        static UIPanel SpawnAndInitDimmer(UIPanel dimmerPrefab , UIPanelLayerConfig layer)
        {
            return PrefabSpawner<UIPanel>.Spawn(dimmerPrefab);
        }
    }
}