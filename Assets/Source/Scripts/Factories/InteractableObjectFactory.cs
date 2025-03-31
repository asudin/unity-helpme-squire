using System.Collections.Generic;
using UnityEngine;
using NSInteractable;

namespace NSFactories
{
    public class InteractableObjectFactory<T> : FactoryBase<T> where T : MonoBehaviour, IInteractable
    {
        protected readonly uint PooledAmount;

        public InteractableObjectFactory(InteractableObjectFactoryData config) : base(new ObjectPool<T>(config.Prefab as T, config.Container, config.PooledAmount))
        {
            PooledAmount = config.PooledAmount;
        }

        private void UpdateInteractableStatus(T item, InteractableStatus status)
        {
            item.UpdateStatus(status);
        }

        public override void ActivateItem(T item)
        {
            base.ActivateItem(item);

            UpdateInteractableStatus(item, InteractableStatus.Active);
        }

        public override void ActivateItem(T item, Vector3 position, Quaternion rotation)
        {
            base.ActivateItem(item, position, rotation);

            UpdateInteractableStatus(item, InteractableStatus.Active);
        }

        public override void DeactivateItem(T item)
        {
            base.DeactivateItem(item);

            UpdateInteractableStatus(item, InteractableStatus.Inactive);
        }
    }

    [System.Serializable]
    public class InteractableObjectFactoryData
    {
        public InteractableBase Prefab;
        public uint PooledAmount;
        public Transform Container;
        public List<Transform> SpawnPoints;
    }
}
