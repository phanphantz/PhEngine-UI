using System.Linq;
using PhEngine.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PhEngine.UI
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class UIPanel : UIElement
    {
        #region Required Components
        
        Canvas unsafeCanvas;
        public Canvas Canvas
        {
            get
            {
                if (unsafeCanvas == null)
                    unsafeCanvas = GetComponent<Canvas>();

                return unsafeCanvas;
            }
        }
        
        CanvasScaler unsafeCanvasScaler;
        public CanvasScaler CanvasScaler
        {
            get
            {
                if (unsafeCanvasScaler == null)
                    unsafeCanvasScaler = GetComponent<CanvasScaler>();

                return unsafeCanvasScaler;
            }
        }
        
        #endregion
        
        [SerializeField] bool isCanBeClosed = true;
        public bool IsCanBeClosed => isCanBeClosed;
        public void SetIsCanBeClosed(bool value) => isCanBeClosed = value;

        [SerializeField] bool isTryCloseOnBackInput;
        public bool IsTryCloseOnBackInput => isTryCloseOnBackInput;
        public void SetIsTryCloseOnBackInput(bool value) => isTryCloseOnBackInput = value;

        public UIPanelConfig Config => config;
        [SerializeField] UIPanelConfig config;
        
        public UIButton CloseButton => closeButton;
        [SerializeField] UIButton closeButton;
        
        public int SortingOrder { get; private set; }
        public UIPanel DimmerPanel { get; private set; }
        
        protected override void Awake()
        {
            UISelectionHelper.DeselectAllUI();
            TryBindCloseButton();
            base.Awake();
        }

        [ContextMenu(nameof(Initialize))]
        public void Initialize()
        {
            CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            CanvasScaler.referenceResolution = new Vector2(1920, 1080);
            CanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            CanvasScaler.matchWidthOrHeight = 0.5f;
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        void TryBindCloseButton()
        {
            if (closeButton)
                closeButton.OnClick += Close;
        }

        protected virtual void Start()
        {
            //Wait for UIPanelSorter to be initialized first
            UIPanelInitializer.Init(this, config);
        }
        
        public override void Close()
        {
            if (!IsCanBeClosed)
            {
                PhDebug.LogWarning<UIPanel>("Cannot close panel. You tried to close the panel : " + gameObject.name + " that marked as cannot be closed!");
                return;
            }
            
            base.Close();
        }

        protected override void ForceClose()
        {
            if (DimmerPanel)
                DimmerPanel.Close();
            
            base.ForceClose();
        }

        #region Sorting
        
        public void BringToFront()
        {
            var allCanvases = FindObjectsOfType<Canvas>();
            var highestOrder = allCanvases.Select(c => c.sortingOrder).Prepend(int.MinValue).Max();
            SetCanvasSortingOrder(highestOrder + 1);
        }
        
        public void SendToBack()
        {
            var allCanvases = FindObjectsOfType<Canvas>();
            var lowestOrder = allCanvases.Select(c => c.sortingOrder).Prepend(int.MaxValue).Min();
            SetCanvasSortingOrder(lowestOrder - 1);
        }

        public void SetCanvasSortingOrder(int order)
        {
            SortingOrder = order;
            Canvas.sortingOrder = order;
        }
        
        #endregion

        public void SetDimmerPanel(UIPanel panel)
            => DimmerPanel = panel;
    }
}