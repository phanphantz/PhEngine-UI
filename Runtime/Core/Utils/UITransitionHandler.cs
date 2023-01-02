using System;
using PhEngine.Motion;

namespace PhEngine.UI
{
    public class UITransitionHandler
    {
        Transition Transition { get; }
        TransitionPlayer Player { get; }
        Action OnFinish { get; set; }
        Action OnFail { get; }
        
        public UITransitionHandler(Transition transition, TransitionPlayer player, Action onFinish, Action onFail)
        {
            Transition = transition;
            Player = player;
            OnFinish = onFinish;
            OnFail = onFail;
        }

        public void Execute()
        {
            if (Transition.IsCanPlay(Transition))
            {
                ScheduleOnFinishAndPlay();
            }
            else
            {
                InvokeOnFinish();
                InvokeOnFail();
            }
        }

        void ScheduleOnFinishAndPlay()
        {
            Transition.onFinish += InvokeOnFinishAndUnbind;
            void InvokeOnFinishAndUnbind()
            {
                InvokeOnFinish();
                Transition.onFinish -= InvokeOnFinishAndUnbind;
            }

            Player.Play(Transition);
        }

        public void Kill()
        {
            OnFinish = null;
        }

        void InvokeOnFinish()
        {
            OnFinish?.Invoke();
        }
        
        void InvokeOnFail()
        {
            OnFail?.Invoke();
        }
    }
}