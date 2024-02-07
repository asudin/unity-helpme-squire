using System.Linq;
using UnityEngine;

namespace NSUtilities
{
    public class StaticInstanceBootstrapper : MonoBehaviour
    {
        [SerializeField] private BaseStaticInstance[] _staticInstances;

        private void Awake()
        {
            _staticInstances = FindObjectsOfType<BaseStaticInstance>();

            foreach (var instance in _staticInstances)
            {
                instance.Initialize();
            }
        }

        public void InjectInstances()
        {
            _staticInstances = FindObjectsOfType<BaseStaticInstance>()
                .OrderBy(order => order.DefaultOrder)
                .ToArray();
        }
    }
}
