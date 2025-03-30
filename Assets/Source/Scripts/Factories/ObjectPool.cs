using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NSFactories
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private List<T> _pool = new List<T>();
        private uint _amount;

        public readonly Transform Container;
        public List<T> Pool => _pool;

        public ObjectPool(List<T> prefabs, Transform container, uint amount = 10)
        {
            Container = container;
            _amount = amount;

            Initialize(prefabs);
        }

        private void Initialize(List<T> prefabs)
        {
            if (prefabs == null)
                return;

            foreach (var prefab in prefabs)
            {
                for (int i = 0; i < _amount; i++)
                {
                    var instance = GameObject.Instantiate(prefab, Container);

                    instance.gameObject.SetActive(false);
                    _pool.Add(instance);
                }
            }
        }

        public bool TryGetPooledObject(out T result)
        {
            result = _pool.FirstOrDefault(pooledObject => !pooledObject.gameObject.activeSelf);

            return result != null;
        }

        public void ResetPool()
        {
            foreach (var item in _pool)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
