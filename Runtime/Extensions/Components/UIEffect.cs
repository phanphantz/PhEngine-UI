using System;
using PhEngine.Motion;
using UnityEngine;

namespace PhEngine.UI
{
    [RequireComponent((typeof(TransitionPlayer)))]
    public class UIEffect : MonoBehaviour
    {
        public bool IsPlaying => TransitionPlayer && TransitionPlayer.IsPlaying;

        public TransitionPlayer TransitionPlayer
        {
            get
            {
                if (transitionPlayer == null)
                    transitionPlayer = GetComponent<TransitionPlayer>();

                return transitionPlayer;
            }
        }

        TransitionPlayer transitionPlayer;

        public UIElement UIElement
        {
            get
            {
                if (uiElement == null)
                    uiElement = GetComponent<UIElement>();

                return uiElement;
            }
        }

        UIElement uiElement;

        public Transition effectTransition;

        [Header("Settings")] public bool isPlayOnEnable = false;
        public bool isWaitUntilFinishShowingBeforePlay = true;
        public bool isForceHideAfterEffectFinish = true;
        public bool isDestroyAfterFinishHiding = true;

        public Action onFinishHiding;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetScreenPosition(Vector3 screenPosition)
        {
            UIElement.RectTransform.anchoredPosition = screenPosition;
        }

        void OnEnable()
        {
            if (isPlayOnEnable)
                Play();
        }

        public void Play()
        {
            if (IsShouldPlayEffectInstantly())
            {
                ActivateGameObjectAndPlayNow();
            }
            else
                HandlePlayEffectAfterUIElementFinishShowing();
        }

        void ActivateGameObjectAndPlayNow()
        {
            gameObject.SetActive(true);
            PlayEffectTransition();
        }

        bool IsShouldPlayEffectInstantly()
        {
            return !UIElement || !isWaitUntilFinishShowingBeforePlay;
        }
        
        void PlayEffectTransition()
        {
            if (!Transition.IsCanPlay(effectTransition))
                return;

            ScheduleOnEffectTransitionFinishActions();
            TransitionPlayer.Play(effectTransition);
        }

        void HandlePlayEffectAfterUIElementFinishShowing()
        {
            if (!UIElement.IsPlayingTransition)
                HandlePlayEffectInCaseTransitionIsNotPlaying();
        }

        void NotifyPlayEffect()
        {
           PlayEffectTransition();
        }
        
        void HandlePlayEffectInCaseTransitionIsNotPlaying()
        {
            if (!UIElement.IsShowing)
                ForceShowAndThenPlayEffectLater();
            else
                PlayEffectTransition();
        }

        void ForceShowAndThenPlayEffectLater()
        {
            UIElement.ForceShow();
        }
        
        void ScheduleOnEffectTransitionFinishActions()
        {
            effectTransition.onFinish += NotifyOnEffectFinish;
        }

        void NotifyOnEffectFinish()
        {
            if (isForceHideAfterEffectFinish)
                TryForceHide();

            effectTransition.onFinish -= NotifyOnEffectFinish;
        }

        void TryForceHide()
        {
            if (UIElement)
            {
                UIElement.Hide();
            }
            else
                NotifyHideFinish();
        }

        void NotifyHideFinish()
        {
            onFinishHiding?.Invoke();
            TryDestroySelfIfNeeded();
        }

        void SelfDestruct()
        {
            Destroy(gameObject);
        }

        void TryDestroySelfIfNeeded()
        {
            if (isDestroyAfterFinishHiding)
                SelfDestruct();
        }

        public void Pause()
        {
            TransitionPlayer.Pause();
        }

        public void Resume()
        {
            TransitionPlayer.Resume();
        }

        public void Kill()
        {
            TransitionPlayer.Kill();
        }

        public void Complete()
        {
            TransitionPlayer.Complete();
        }
    }
}