using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using NSFires;

namespace NSFactories
{
    public class FiresFactory : FactoryBase<Fire>
    {
        private List<Transform> _spawnPoints;
        private float _spawnTimer;
        private int _instantlySpawnedAmount;
        private readonly ObjectPool<Fire> _pool;
        private CancellationTokenSource _cts;
        private Dictionary<Transform, Fire> _spawnPointToFireMap;

        public FiresFactory(FiresFactoryData config, float spawnTimer)
        {
            _spawnPoints = config.SpawnPoints;
            _spawnTimer = spawnTimer;
            _instantlySpawnedAmount = config.InstantlySpawnedAmount;
            _pool = new ObjectPool<Fire>(config.FirePrefabs, config.Container, 20);
            _cts = new CancellationTokenSource();
            _spawnPointToFireMap = new Dictionary<Transform, Fire>();
        }

        public void Initialize()
        {
            InstantlyActivateFires();
            SpawnFiresOverTimeAsync(_cts.Token).Forget();
        }

        private void InstantlyActivateFires()
        {
            for (int i = 0; i < _instantlySpawnedAmount && i < _pool.Pool.Count; i++)
            {
                if (i < _pool.Pool.Count && i < _spawnPoints.Count)
                {
                    Fire fire = _pool.Pool[i];
                    Transform spawnPoint = _spawnPoints[i];
                    SetPosition(fire, spawnPoint.position);
                    ActivateItem(fire);
                    UpdateFireStatus(fire, FireStatus.Active);

                    _spawnPointToFireMap[spawnPoint] = fire;
                }
            }
        }

        private async UniTaskVoid SpawnFiresOverTimeAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnTimer), cancellationToken: cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                    break;

                foreach (var spawnPoint in _spawnPoints)
                {
                    if (!_spawnPointToFireMap.ContainsKey(spawnPoint) || _spawnPointToFireMap[spawnPoint] == null || !_spawnPointToFireMap[spawnPoint].gameObject.activeInHierarchy)
                    {
                        if (_pool.TryGetPooledObject(out Fire fire))
                        {
                            SetPosition(fire, spawnPoint.position);
                            ActivateItem(fire);
                            UpdateFireStatus(fire, FireStatus.Active);

                            _spawnPointToFireMap[spawnPoint] = fire;
                        }
                    }
                }
            }
        }

        private void UpdateFireStatus(Fire fire, FireStatus status)
        {
            fire.UpdateStatus(status);
        }

        public void StopSpawning()
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = new CancellationTokenSource();
            }
        }

        public void Dispose()
        {
            StopSpawning();
        }

        public override void ActivateItem(Fire item)
        {
            base.ActivateItem(item);

            UpdateFireStatus(item, FireStatus.Active);
        }

        public override void DeactivateItem(Fire item)
        {
            base.DeactivateItem(item);

            UpdateFireStatus(item, FireStatus.Inactive);
        }
    }

    [System.Serializable]
    public class FiresFactoryData
    {
        public List<Fire> FirePrefabs;
        public List<Transform> SpawnPoints;
        public Transform Container;
        public int InstantlySpawnedAmount;
    }
}