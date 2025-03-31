using NSInteractable;
using NSInteractable.Consumables;
using System.Collections.Generic;
using UnityEngine;

namespace NSFactories
{
    public class PotionFactory : FactoryBase<Potion>
    {
        private List<Transform> _spawnPoints;
        private float _spawnDelay;
        private readonly uint _pooledAmount;
        private readonly ObjectPool<Potion> _pool;
        private Dictionary<Transform, Potion> _spawnPointToPotionMap;

        public PotionFactory(PotionFactoryData config, float spawnDelay)
        {
            _spawnPoints = config.SpawnPoints;
            _spawnDelay = spawnDelay;
            _pooledAmount = config.PooledAmount;
            _pool = new ObjectPool<Potion>(config.PotionPrefab, config.Container, _pooledAmount);
            _spawnPointToPotionMap = new Dictionary<Transform, Potion>();
        }

        public void Initialize()
        {
            MapPotionsWithSpawnPoints();
            InstantlyActivatePotions();
        }

        private void MapPotionsWithSpawnPoints()
        {
            for (int i = 0; i < _pool.Pool.Count; i++)
            {
                if (i < _pool.Pool.Count && i < _spawnPoints.Count)
                {
                    Potion potion = _pool.Pool[i];
                    Transform spawnPoint = _spawnPoints[i];

                    _spawnPointToPotionMap[spawnPoint] = potion;
                }
            }
        }

        private void InstantlyActivatePotions()
        {
            foreach (var (spawnPoint, potion) in _spawnPointToPotionMap)
            {
                SetItemPosition(potion, spawnPoint.position);
                ActivateItem(potion);
            }
        }

        private void UpdatePotionStatus(Potion fire, InteractableStatus status)
        {
            fire.UpdateStatus(status);
        }

        public override void ActivateItem(Potion item)
        {
            base.ActivateItem(item);

            UpdatePotionStatus(item, InteractableStatus.Active);
        }

        public override void DeactivateItem(Potion item)
        {
            base.DeactivateItem(item);

            UpdatePotionStatus(item, InteractableStatus.Inactive);
        }
    }

    [System.Serializable]
    public class PotionFactoryData
    {
        public Potion PotionPrefab;
        public List<Transform> SpawnPoints;
        public Transform Container;
        public uint PooledAmount;
    }
}
