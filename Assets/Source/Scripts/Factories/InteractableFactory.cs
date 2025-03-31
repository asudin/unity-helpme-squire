using System.Collections.Generic;
using UnityEngine;
using NSInteractable;

namespace NSFactories
{
    public abstract class InteractableFactory<T> : FactoryBase<T> where T : MonoBehaviour, IInteractable
    {
        protected List<Transform> SpawnPoints;
        protected float SpawnDelay;
        protected readonly uint PooledAmount;
        protected readonly ObjectPool<T> Pool;
        protected Dictionary<Transform, T> SpawnPointToItemMap;

        protected InteractableFactory(List<Transform> spawnPoints, float spawnDelay, uint pooledAmount, T prefab, Transform container)
        {
            SpawnPoints = spawnPoints;
            SpawnDelay = spawnDelay;
            PooledAmount = pooledAmount;
            Pool = new ObjectPool<T>(prefab, container, PooledAmount);
            SpawnPointToItemMap = new Dictionary<Transform, T>();
        }

        protected virtual void MapItemsWithSpawnPoints()
        {
            int count = Mathf.Min(Pool.Pool.Count, SpawnPoints.Count);

            for (int i = 0; i < count; i++)
            {
                T item = Pool.Pool[i];
                Transform spawnPoint = SpawnPoints[i];

                SpawnPointToItemMap[spawnPoint] = item;
            }
        }

        protected virtual void InstantlyActivateItems()
        {
            foreach (var pair in SpawnPointToItemMap)
            {
                Transform spawnPoint = pair.Key;
                T item = pair.Value;

                SetItemPosition(item, spawnPoint.position);
                ActivateItem(item);
            }
        }

        protected virtual void UpdateInteractableStatus(T item, InteractableStatus status)
        {
            item.UpdateStatus(status);
        }

        public override void ActivateItem(T item)
        {
            base.ActivateItem(item);
            UpdateInteractableStatus(item, InteractableStatus.Active);
        }
    }
}
