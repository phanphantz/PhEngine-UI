using System;
using System.Collections;
using PhEngine.Motion;
using UnityEngine;

namespace PhEngine.UI
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(TransitionPlayer))]
    [RequireComponent(typeof(UITransition))]
    public abstract class UIElement : MonoBehaviour
    {
        #region Required Components
        
        RectTransform unsafeRectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (unsafeRectTransform == null)
                    unsafeRectTransform = GetComponent<RectTransform>();

                return unsafeRectTransform;
            }
        }
        
        CanvasGroup unsafeCanvasGroup;
        public CanvasGroup CanvasGroup
        {
            get
            {
                if (unsafeCanvasGroup == null)
                    unsafeCanvasGroup = GetComponent<CanvasGroup>();

                return unsafeCanvasGroup;
            }
        }

        TransitionPlayer unsafeTransitionPlayer;
        public TransitionPlayer TransitionPlayer
        {
            get
            {
                if (unsafeTransitionPlayer == null)
                    unsafeTransitionPlayer = GetComponent<TransitionPlayer>();

                return unsafeTransitionPlayer;
            }
        }
        
        UITransition unsafeUITransition;
        public UITransition UITransition
        {
            get
            {
                if (unsafeUITransition == null)
                    unsafeUITransition = GetComponent<UITransition>();

                return unsafeUITransition;
            }
        }
        
        #endregion

        UIElementActionLogger logger;
        
        [SerializeField] bool isLogging;
        public void SetIsLogging(bool value) => isLogging = value;
        public bool IsLogging => isLogging;

        public event Action OnHideBegin;
        public event Action OnHideFinish;
        public event Action OnShowBegin;
        public event Action OnShowFinish;

        public event Action OnSelectBegin;
        public event Action OnSelectFinish;
        public event Action OnDeselectBegin;
        public event Action OnDeselectFinish;
        
        public event Action OnCloseBegin;
        public event Action OnCloseFinish;

        public bool IsShowing => isShowing;
        [SerializeField] bool isShowing;
        
        public bool IsSelected => isSelected;
        [SerializeField] bool isSelected;
        
        public bool IsClosed { get; private set; }
        public bool IsPlayingTransition => TransitionPlayer.IsPlaying;

        UITransitionHandler transitionHandler;
        
        protected virtual void Awake()
        {
            logger = new UIElementActionLogger(this);
            Show();
        }

        #region Show / Hide / Close
        
        [ContextMenu(nameof(Show))]
        public void Show()
        {
            if (isShowing)
                return;

            ForceShow();
        }

        internal void ForceShow()
        {
            isShowing = true;
            Activate();
            InvokeOnShowBegin();
            HandleTransition(UITransition.Show, InvokeOnShowFinish);
        }

        public void Activate() => gameObject.SetActive(true);

        protected void HandleTransition(Transition transition, Action onFinish, Action onFail = null)
        {
            transitionHandler?.Kill();
            transitionHandler = new UITransitionHandler(transition, TransitionPlayer, onFinish, onFail);
            transitionHandler.Execute();
        }
        
        [ContextMenu(nameof(Hide))]
        public void Hide()
        {
            if (!isShowing)
                return;

            isShowing = false;
            InvokeOnHideBegin();
            HandleTransition(UITransition.Hide, NotifyHideFinish, Deactivate);
        }

        void NotifyHideFinish()
        {
            InvokeOnHideFinish();
            Deactivate();
        }

        public void Deactivate()
            => gameObject.SetActive(false);
        
        [ContextMenu(nameof(Close))]
        public virtual void Close()
        {
            if (IsClosed)
                return;

            ForceClose();
        }

        protected virtual void ForceClose()
        {
            UISelectionHelper.DeselectAllUI();
            IsClosed = true;
            
            if (!isShowing)
                CloseFromHiddenState();
            else
                CloseFromShowingState();
        }
        
        void CloseFromHiddenState()
        {
            InvokeOnCloseBegin();
            NotifyCloseFinish();
        }
        
        void NotifyCloseFinish()
        {
            InvokeOnCloseFinish();
            Destroy(gameObject);
        }

        void CloseFromShowingState()
        {
            isShowing = false;
            InvokeOnCloseBegin();
            HandleTransition(UITransition.Hide, NotifyCloseFinish);
        }
        
        #endregion
        
        #region Select / Deselect
        
        [ContextMenu(nameof(Deselect))]
        public void Deselect()
        {
            if (!isSelected)
                return;
            
            isSelected = false;
            InvokeOnDeselectBegin();
            HandleTransition(UITransition.Deselect, InvokeOnDeselectFinish);
        }

        [ContextMenu(nameof(Select))]
        public void Select()
        {
            if (isSelected)
                return;

            isSelected = true;
            InvokeOnSelectBegin();
            HandleTransition(UITransition.Select, InvokeOnSelectFinish);
        }
        
        public void ToggleSelection()
        {
            if (isSelected)
                Deselect();
            else
                Select();
        }
        
        #endregion
        
        #region Invoke
        
        public void InvokeOnShowBegin()
            => OnShowBegin?.Invoke();
        
        public void InvokeOnShowFinish()
            => OnShowFinish?.Invoke();
        
        public void InvokeOnHideBegin()
            => OnHideBegin?.Invoke();
        
        public void InvokeOnHideFinish()
            => OnHideFinish?.Invoke();
        
        public void InvokeOnSelectBegin()
            => OnSelectBegin?.Invoke();
        
        public void InvokeOnSelectFinish()
            => OnSelectFinish?.Invoke();
        
        public void InvokeOnDeselectBegin()
            => OnDeselectBegin?.Invoke();
        
        public void InvokeOnDeselectFinish()
            => OnDeselectFinish?.Invoke();
        
        public void InvokeOnCloseBegin()
            => OnCloseBegin?.Invoke();
        
        public void InvokeOnCloseFinish()
            => OnCloseFinish?.Invoke();
        
        #endregion

        public void PreventInputUntil(Func<bool> endCondition)
        {
            if (gameObject.activeSelf)
                StartCoroutine(PreventInputUntilRoutine(endCondition));
        }

        IEnumerator PreventInputUntilRoutine(Func<bool> endCondition)
        {
            while (true)
            {
                SetCanvasGroupInteractable(false);
                if (endCondition.Invoke())
                    break;
                
                yield return null;
            }
            
            SetCanvasGroupInteractable(true);
        }
        
        public WaitUntil WaitUntilTransitionEnd()
            => new WaitUntil(() => !IsPlayingTransition);
        
        public void Rebuild()
            => UILayoutRebuilder.Rebuild(RectTransform);

        public void SetSortingOrderBySiblingIndex(int order)
            => transform.SetSiblingIndex(order);

        public bool IsCanvasGroupInteractable 
            => CanvasGroup.blocksRaycasts;
        
        public void SetCanvasGroupInteractable(bool on) 
            => CanvasGroup.blocksRaycasts = on;

        public void SetAlpha(float value)
            => CanvasGroup.alpha = value;

        [ContextMenu(nameof(ToggleLog))]
        public void ToggleLog() => isLogging = !isLogging;
    }
}