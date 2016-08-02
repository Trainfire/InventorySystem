using UnityEngine;
using UnityEngine.Events;

namespace Framework
{
    public abstract class Tween<T> : MonoBehaviour
    {
        public UnityAction<T> OnTweenValue;
        public float Duration;
        public bool DoTween = false;
        public T Value;

        bool Tweening { get; set; }
        float CurrentTime { get; set; }
        UnityAction OnDone { get; set; }

        public void Play(UnityAction onDone = null)
        {
            CurrentTime = 0f;
            DoTween = true;
            OnDone = onDone;
        }

        void Update()
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
                    Tweening = false;
                    if (OnDone != null)
                        OnDone();
                }
            }
        }

        protected abstract T OnTween(float delta);
    }
}
