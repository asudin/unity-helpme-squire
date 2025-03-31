using UnityEngine;

namespace NSFactories
{
    public class FactoryBase<T> : IFactory<T> where T : MonoBehaviour
    {
        public virtual void ActivateItem(T item)
        {
            item.gameObject.SetActive(true);
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
