using UnityEditor;
using UnityEngine;
using UserSystemFramework.Scripts.System.Config;

namespace UserSystemFramework.Editor.CustomEditors
{
    [CustomEditor(typeof(CustomFieldsConfiguration))]
    public class CustomFieldsConfigurationEditor : UnityEditor.Editor
    {
        public CustomFieldsConfiguration config;
        public bool enableCustomEdit = false;
        private readonly GUIStyle _simpleWrappedLabel = new GUIStyle();
        public void OnEnable()
        {
            _simpleWrappedLabel.wordWrap = true;
            _simpleWrappedLabel.normal.textColor = EditorStyles.label.normal.textColor;
            config = (CustomFieldsConfiguration)target;
        }

        public override void OnInspectorGUI()
        {
            enableCustomEdit = GUILayout.Toggle(enableCustomEdit, "Enable editing");
            if (!enableCustomEdit)
            {
                EditorGUI.BeginDisabledGroup(true);
                base.OnInspectorGUI();
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                base.OnInspectorGUI();
            }
        }
    }
}