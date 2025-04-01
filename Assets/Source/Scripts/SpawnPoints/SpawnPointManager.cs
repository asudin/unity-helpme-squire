using NSInteractable;
using System.Collections.Generic;
using UnityEngine;

namespace NSSpawnPoints
{
    public class SpawnPointManager
    {
        private readonly Dictionary<IInteractable, Vector3> _spawnPointsToInteractableMap = new();

        public Dictionary<IInteractable, Vector3> SpawnPointsToInteractableMap => _spawnPointsToInteractableMap;

        public SpawnPointManager() { }

        public void RegisterSpawnPoint(IInteractable item, Vector3 position)
        {
            if (!_spawnPointsToInteractableMap.ContainsKey(item))
            {
                _spawnPointsToInteractableMap[item] = position;
            }
        }

        public Vector3 GetSpawnPosition(IInteractable item)
        {
            return _spawnPointsToInteractableMap.TryGetValue(item, out var position) ? position : Vector3.zero;
        }
    }
}
