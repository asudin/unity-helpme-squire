using UnityEngine;

namespace NSFactories
{
    public interface IFactory<T> where T : MonoBehaviour
    {
        public ObjectPool<T> Pool { get; }

        void ActivateItem(T item);
        
        void ActivateItem(T item, Vector3 position, Quaternion rotation);

        void DeactivateItem(T item);

        void SetItemPosition(T item, Vector3 position);
    }
}
