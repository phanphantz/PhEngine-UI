using UnityEngine;
using PhEngine.Core;
using PhEngine.Core.AssetBox;

namespace PhEngine.UI
{
    [RequireComponent(typeof(UIPanelHolder))]
    public class UIPanelManager : Singleton<UIPanelManager>
    {
        [SerializeField] AssetBox panelAssetBox;
        [SerializeField] UIPanelLayerPriority layerPriority;
        [SerializeField] bool isListenToBackInput = true; 
        [SerializeField] bool isRemoveHistoryFromHolderOnClose = true;

        UIPanelHolder unsafePanelHolder;
        public UIPanelHolder PanelHolder
        {
            get
            {
                if (unsafePanelHolder == null)
                    unsafePanelHolder = GetComponent<UIPanelHolder>();

                return unsafePanelHolder;
            }
        }
        
        void Update()
        {
            if (isListenToBackInput)
                ListenForBackInput();
        }

        void ListenForBackInput()
        {
            if (IsBackInputDetected())
                PanelHolder.CloseTopMostPanelIfBackInputIsAllow(isRemoveHistoryFromHolderOnClose);
        }

        private static bool IsBackInputDetected()
        {
            return IsPressingEscapeOrBackOnMobile();
        }

        private static bool IsPressingEscapeOrBackOnMobile()
        {
            return Input.GetKeyDown(KeyCode.Escape);
        }

        protected override void InitAfterAwake() 
        {
        }

        public UIPanel Spawn(string loadId, string historyTag = null)
        {
            var panel = CreatePanel(loadId);
            if (panel == null) 
                return null;
            
            PanelHolder.AddPanel(panel, loadId, historyTag);
            return panel;
        }
        
        UIPanel CreatePanel(string loadId)
        {
            var panel = LoadPanelFromPoolOrCreateNewIfNeeded(loadId);
            if (panel != null) 
                return panel;
            
            PhDebug.LogError<UIPanelManager>("Cannot load panel. Panel with loadId : " + loadId + " is not found");
            return null;
        }

        UIPanel LoadPanelFromPoolOrCreateNewIfNeeded(string loadId)
        {
            var prefabToLoad = Find(loadId);
            return prefabToLoad == null ? null : PrefabSpawner<UIPanel>.Spawn(prefabToLoad);
        }

        UIPanel Find(string loadId)
        {
            return panelAssetBox.LoadUI<UIPanel>(loadId);
        }
        
        public void Sort(UIPanel uiPanel, UIPanelLayerConfig layer)
        {
            if (layer)
                uiPanel.SetCanvasSortingOrder(layerPriority.GetPriority(layer));
        }
    }
}