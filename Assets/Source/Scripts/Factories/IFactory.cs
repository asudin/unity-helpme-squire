using UnityEngine;

namespace NSFactories
{
    public interface IFactory<T>
    {
        void ActivateItem(T item);

        void SetItemPosition(T item, Vector3 position);
    }
}
