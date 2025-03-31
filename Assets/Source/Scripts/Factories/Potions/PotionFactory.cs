using System.Collections.Generic;
using UnityEngine;
using NSInteractable.Consumables;

namespace NSFactories
{
    [System.Serializable]
    public class PotionFactoryData
    {
        public Potion PotionPrefab;
        public List<Transform> SpawnPoints;
        public Transform Container;
        public uint PooledAmount;
    }

    public class PotionFactory : InteractableFactory<Potion>
    {
        public PotionFactory(PotionFactoryData config, float spawnDelay)
            : base(config.SpawnPoints, spawnDelay, config.PooledAmount, config.PotionPrefab, config.Container)
        {
        }

        public void Initialize()
        {
            MapItemsWithSpawnPoints();
            InstantlyActivateItems();
        }
    }
}
