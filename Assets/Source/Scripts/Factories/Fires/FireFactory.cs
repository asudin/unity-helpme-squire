using System.Collections.Generic;
using UnityEngine;
using NSInteractable.Fires;

namespace NSFactories
{
    [System.Serializable]
    public class FireFactoryData
    {
        public Fire FirePrefab;
        public List<Transform> SpawnPoints;
        public Transform Container;
        public int InstantlySpawnedAmount;
        public uint PooledAmount;
    }

    public class FireFactory : InteractableFactory<Fire>
    {
        private int _instantlySpawnedAmount;

        public FireFactory(FireFactoryData config, float spawnDelay)
            : base(config.SpawnPoints, spawnDelay, config.PooledAmount, config.FirePrefab, config.Container)
        {
            _instantlySpawnedAmount = config.InstantlySpawnedAmount;
        }

        public void Initialize()
        {
            for (int i = 0; i < _instantlySpawnedAmount && i < Pool.Pool.Count && i < SpawnPoints.Count; i++)
            {
                Fire fire = Pool.Pool[i];
                Transform spawnPoint = SpawnPoints[i];

                SpawnPointToItemMap[spawnPoint] = fire;
            }

            InstantlyActivateItems();
        }
    }
}
