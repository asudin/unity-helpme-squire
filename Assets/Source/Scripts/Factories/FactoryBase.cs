using System;
using UnityEngine;

namespace NSFactories
{
    public abstract class FactoryBase<T> : IFactory<T> where T : MonoBehaviour
    {
        private readonly ObjectPool<T> _pool;

        public ObjectPool<T> Pool => _pool; 

        protected FactoryBase(ObjectPool<T> pool)
        {
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));
        }

        public virtual void ActivateItem(T item)
        {
            item.gameObject.SetActive(true);
        }

        public virtual void ActivateItem(T item, Vector3 position, Quaternion rotation)
        {
            item.gameObject.SetActive(true);
            item.transform.position = position;
            item.transform.rotation = rotation;
        }

        public virtual void DeactivateItem(T item)
        {
            item.gameObject.SetActive(false);
        }

        public void SetItemPosition(T item, Vector3 position)
        {
            item.transform.position = position;
        }
    }
}
