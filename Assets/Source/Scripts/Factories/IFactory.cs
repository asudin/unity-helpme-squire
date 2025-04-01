using UnityEngine;

namespace NSFactories
{
    public interface IFactory<T> where T : MonoBehaviour
    {
        public ObjectPool<T> Pool { get; }

        void ActivateItems();
        
        void ActivateItem(T item);

        void ActivateItem(T item, Vector3 position);

        void DeactivateItem(T item);
    }
}
