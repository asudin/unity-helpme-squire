using System.Collections.Generic;
using UnityEngine;
using NSInteractable.Fires;
using NSInteractable;

namespace NSFactories
{
    public class FireFactory : FactoryBase<Fire>
    {
        private List<Transform> _spawnPoints;
        private float _spawnDelay;
        private int _instantlySpawnedAmount;
        private readonly uint _pooledAmount;
        private readonly ObjectPool<Fire> _pool;
        private Dictionary<Transform, Fire> _spawnPointToFireMap;

        public FireFactory(FireFactoryData config, float spawnDelay)
        {
            _spawnPoints = config.SpawnPoints;
            _spawnDelay = spawnDelay;
            _pooledAmount = config.PooledAmount;
            _instantlySpawnedAmount = config.InstantlySpawnedAmount;
            _pool = new ObjectPool<Fire>(config.FirePrefab, config.Container, _pooledAmount);
            _spawnPointToFireMap = new Dictionary<Transform, Fire>();
        }

        public void Initialize()
        {
            MapFiresWithSpawnPoints();
            InstantlyActivateFires();
        }

        private void MapFiresWithSpawnPoints()
        {
            for (int i = 0; i < _instantlySpawnedAmount && i < _pool.Pool.Count; i++)
            {
                if (i < _pool.Pool.Count && i < _spawnPoints.Count)
                {
                    Fire fire = _pool.Pool[i];
                    Transform spawnPoint = _spawnPoints[i];

                    _spawnPointToFireMap[spawnPoint] = fire;
                }
            }
        }

        private void InstantlyActivateFires()
        {
            foreach (var (spawnPoint, fire) in _spawnPointToFireMap)
            {
                SetItemPosition(fire, spawnPoint.position);
                ActivateItem(fire);
            }
        }

        private void UpdateFireStatus(Fire fire, InteractableStatus status)
        {
            fire.UpdateStatus(status);
        }

        public override void ActivateItem(Fire item)
        {
            base.ActivateItem(item);

            UpdateFireStatus(item, InteractableStatus.Active);
        }

        public override void DeactivateItem(Fire item)
        {
            base.DeactivateItem(item);

            UpdateFireStatus(item, InteractableStatus.Inactive);
        }
    }

    [System.Serializable]
    public class FireFactoryData
    {
        public Fire FirePrefab;
        public List<Transform> SpawnPoints;
        public Transform Container;
        public int InstantlySpawnedAmount;
        public uint PooledAmount;
    }
}