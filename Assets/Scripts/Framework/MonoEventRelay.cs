using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Framework
{
    public interface IMonoUpdateReceiver
    {
        void OnUpdate();
    }

    public interface IMonoLateUpdateReceiver
    {
        void OnLateUpdate();
    }

    public class MonoEventRelay : MonoBehaviour
    {
        static List<IMonoUpdateReceiver> updateReceivers;
        static List<IMonoLateUpdateReceiver> lateUpdateReceivers;

        public void Awake()
        {
            updateReceivers = new List<IMonoUpdateReceiver>();
            lateUpdateReceivers = new List<IMonoLateUpdateReceiver>();
        }

        public void Update()
        {
            updateReceivers.ForEach(x => x.OnUpdate());
        }

        public void LateUpdate()
        {
            lateUpdateReceivers.ForEach(x => x.OnLateUpdate());
        }

        public static void RegisterForUpdate(IMonoUpdateReceiver listener)
        {
            updateReceivers.Add(listener);
        }

        public static void RegisterForLateUpdate(IMonoLateUpdateReceiver listener)
        {
            lateUpdateReceivers.Add(listener);
        }
    }

}