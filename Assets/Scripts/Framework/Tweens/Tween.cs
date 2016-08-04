using System;
using UnityEngine;
using UnityEngine.Events;

namespace Framework
{
    public abstract class Tween<T> : IMonoUpdateReceiver
    {
        public UnityAction<T> OnTweenValue;
        public float Duration;
        public bool DoTween = false;

        public T Value { get; private set; }
        public T From { get; set; }
        public T To { get; set; }

        public bool Tweening { get; set; }
        float CurrentTime { get; set; }
        UnityAction OnDone { get; set; }

        public Tween()
        {
            MonoEventRelay.RegisterForUpdate(this);
        }

        public void Play(UnityAction onDone = null)
        {
            CurrentTime = 0f;
            DoTween = true;
            OnDone = onDone;
        }

        protected abstract T OnTween(float delta);

        void IMonoUpdateReceiver.OnUpdate()
        {
            if (DoTween)
            {
                DoTween = false;
                Tweening = true;
                CurrentTime = 0f;
            }

            if (Tweening)
            {
                Value = OnTween(CurrentTime / Duration);

                if (OnTweenValue != null)
                    OnTweenValue(Value);

                CurrentTime += Time.deltaTime;

                if (CurrentTime > Duration)
                {
                    Value = To;

                    Tweening = false;

                    if (OnDone != null)
                        OnDone();
                }
            }
        }
    }
}
