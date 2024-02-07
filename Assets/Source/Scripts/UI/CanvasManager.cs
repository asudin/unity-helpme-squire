using NSUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NSUI
{
    public class CanvasManager : PersistentSingleton<CanvasManager>
    {
        [SerializeField] private Canvas[] _setCanvases;
        public Canvas[] SetCanvases { get => _setCanvases; set => _setCanvases = value; }

        [Button]
        private void FillCanvases()
        {
            SetCanvases = GetComponentsInChildren<Canvas>();
        }
    }
}