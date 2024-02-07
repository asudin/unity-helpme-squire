using NSUtilities;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NSEditor
{
    public class BootstrappingOrderWindow : OdinEditorWindow
    {
        [MenuItem("Tools/Custom Project Settings")]
        private static void ShowWindow()
        {
            var window = GetWindow<BootstrappingOrderWindow>();

            window.titleContent = new GUIContent("Custom Project Settings", EditorIcons.SettingsCog.Raw);
            window.Show();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            titleContent = new GUIContent("Custom Project Settings", EditorIcons.SettingsCog.Raw);
            Refresh();
        }

        [VerticalGroup("Group 1", 1)]
        [ListDrawerSettings(ShowIndexLabels = false, ShowPaging = false, ShowItemCount = false, HideRemoveButton = true,
                HideAddButton = true, DefaultExpandedState = true, DraggableItems = false)]
        public List<InstanceOrderPack> _staticInstances;

        [VerticalGroup("Group 0", 0)]
        [Button]
        public void SetOrder()
        {
            foreach (var staticInstance in _staticInstances)
            {
                staticInstance.Instance.DefaultOrder = staticInstance.Order;
                EditorUtility.SetDirty(staticInstance.Instance);
            }

            Refresh();

            StaticInstanceBootstrapper bootstrapperInstance = FindObjectOfType<StaticInstanceBootstrapper>();

            if (bootstrapperInstance != null)
            {
                bootstrapperInstance.InjectInstances();
            }
        }

        [VerticalGroup("Group 0", 0)]
        [Button]
        private void Refresh()
        {
            _staticInstances = new List<InstanceOrderPack>();

            foreach (var staticInstance in FindObjectsOfType<BaseStaticInstance>())
            {
                _staticInstances.Add(new InstanceOrderPack(staticInstance, staticInstance.DefaultOrder));
            }

            _staticInstances = _staticInstances.OrderBy(order => order.Order).ToList();
        }
    }

    [Serializable]
    public class InstanceOrderPack
    {
        [ReadOnly] public BaseStaticInstance Instance;
        public int Order;

        public InstanceOrderPack(BaseStaticInstance instance, int order)
        {
            this.Instance = instance;
            Order = order;
        }
    }
}
