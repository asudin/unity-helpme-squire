using System.Collections.Generic;
using UnityEngine;
using NSInteractable;
using NSSpawnPoints;

namespace NSFactories
{
    public class InteractableFactory<T> : FactoryBase<T> where T : MonoBehaviour, IInteractable
    {
        protected readonly uint PooledAmount;

        private SpawnPointManager _spawnPointManager;
        private Dictionary<IInteractable, Vector3> PairedInteractables;

        public InteractableFactory(InteractableFactoryData config) : base(new ObjectPool<T>(config.Prefab as T, config.Container, config.PooledAmount))
        {
            PooledAmount = config.PooledAmount;
            _spawnPointManager = new SpawnPointManager();

            MapInteractableToSpawnPoint(config);
        }

        private void MapInteractableToSpawnPoint(InteractableFactoryData config)
        {
            for (int i = 0; i < config.SpawnPoints.Count; i++)
            {
                _spawnPointManager.RegisterSpawnPoint(Pool.PooledItemsList[i], config.SpawnPoints[i].position);
            }

            PairedInteractables = _spawnPointManager.SpawnPointsToInteractableMap;
        }

        private void UpdateInteractableStatus(T item, InteractableStatus status)
        {
            item.UpdateStatus(status);
        }

        public override void ActivateItems()
        {
            foreach (var pair in _spawnPointManager.SpawnPointsToInteractableMap)
            {
                ActivateItem(pair.Key as T, pair.Value);
            }
        }

        public override void ActivateItem(T item)
        {
            base.ActivateItem(item);

            UpdateInteractableStatus(item, InteractableStatus.Active);
        }

        public override void ActivateItem(T item, Vector3 position)
        {
            base.ActivateItem(item, position);

            UpdateInteractableStatus(item, InteractableStatus.Active);
        }

        public override void DeactivateItem(T item)
        {
            base.DeactivateItem(item);

            var spawnPoint = _spawnPointManager.GetSpawnPosition(item);

            item.transform.position = spawnPoint;
            UpdateInteractableStatus(item, InteractableStatus.Inactive);
        }
    }

    [System.Serializable]
    public class InteractableFactoryData
    {
        public InteractableBase Prefab;
        public uint PooledAmount;
        public Transform Container;
        public List<Transform> SpawnPoints;
    }
}
