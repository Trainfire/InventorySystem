using System.Collections;
using System.Collections.Generic;

namespace Framework
{
    public enum State
    {
        Running,
        Paused,
    }

    public interface IStateListener
    {
        void OnStateChanged(State state);
    }

    public class StateManager
    {
        private List<IStateListener> listeners;

        public State State { get; private set; }

        public StateManager()
        {
            listeners = new List<IStateListener>();
        }

        public void RegisterListener(IStateListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(IStateListener listener)
        {
            listeners.Remove(listener);
        }

        public void SetState(State state)
        {
            State = state;
            listeners.ForEach(x => x.OnStateChanged(state));
        }
    }
}
