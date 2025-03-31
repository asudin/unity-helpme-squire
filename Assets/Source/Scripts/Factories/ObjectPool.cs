using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NSFactories
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private List<T> _pooledItemsList = new List<T>();
        private uint _amount;

        public readonly Transform Container;
        public List<T> PooledItemsList => _pooledItemsList;

        public ObjectPool(T prefab, Transform container, uint amount = 10)
        {
            Container = container;
            _amount = amount;

            Initialize(prefab);
        }

        private void Initialize(T prefab)
        {
            if (prefab == null)
                return;

            for (int i = 0; i < _amount; i++)
            {
                var instance = GameObject.Instantiate(prefab, Container);

                instance.gameObject.SetActive(false);
                _pooledItemsList.Add(instance);
            }
        }

        public bool TryGetPooledObject(out T result)
        {
            result = _pooledItemsList.FirstOrDefault(pooledObject => !pooledObject.gameObject.activeSelf);

            return result != null;
        }

        public void ResetPool()
        {
            foreach (var item in _pooledItemsList)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
