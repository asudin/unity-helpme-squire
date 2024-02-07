using UnityEngine;

namespace NSUtilities
{
    public abstract class BaseStaticInstance : MonoBehaviour
    {
        [SerializeField] private int _defaultOrder;

        public int DefaultOrder
        {
            get => _defaultOrder;
            set => _defaultOrder = value;
        }

        public abstract void Initialize();
        protected abstract void OnApplicationQuit();
    }

    public abstract class StaticInstance<T> : BaseStaticInstance
        where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        public static T ForceInstance
        {
            get
            {
                if (Instance != null) return Instance;

                T foundObject = FindObjectOfType<T>();
                GameObject singletonObject = foundObject == null ? null : foundObject.gameObject;

                if (singletonObject == null)
                {
                    singletonObject = new GameObject(nameof(T));
                    Instance = singletonObject.AddComponent<T>();
                }
                else
                {
                    Instance = foundObject;
                }

                return Instance;
            }
        }

        public override void Initialize() =>
            Instance = this as T;

        protected override void OnApplicationQuit()
        {
            Instance = null;

            Destroy(gameObject);
        }
    }

    public abstract class Singleton<T> : StaticInstance<T>
        where T : MonoBehaviour
    {
        public override void Initialize()
        {
            if (Instance == null)
                return;

            base.Initialize();
        }
    }

    public abstract class PersistentSingleton<T> : StaticInstance<T>
        where T : MonoBehaviour
    {
        public override void Initialize()
        {
            base.Initialize();

            transform.SetParent(null);

            DontDestroyOnLoad(gameObject);
        }
    }
}
