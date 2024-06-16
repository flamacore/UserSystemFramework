/*using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UserSystemFramework.Scripts.API.ObjectUpdaters;
using UserSystemFramework.Scripts.System.Data.Interfaces;

namespace UserSystemFramework.Editor.CustomEditors
{
    [CustomEditor(typeof(TextMeshProUpdater))]
    public class TextMeshProUpdaterEditor : BaseComponentUpdaterEditor
    {
        private TextMeshProUpdater _textMeshProUpdater;
        private int _selectedData;
        private int SelectedData
        {
            get => _selectedData;
            set => _textMeshProUpdater.SelectedData = value;
        }
        private int _selectedSubData;
        private int SelectedSubData
        {
            get => _selectedSubData;
            set => _textMeshProUpdater.SelectedSubData = value;
        }
        private int _selectedSource;
        private int SelectedSource
        {
            get => _selectedSource;
            set => _textMeshProUpdater.SelectedSource = value;
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _textMeshProUpdater = (TextMeshProUpdater) target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Select Data");
            EditorGUILayout.BeginHorizontal();
            SelectedSource =
                EditorGUILayout.Popup(SelectedSource, pusherSources.Select(x => x.GetType().Name).ToArray());
            int clamp = Mathf.Clamp(SelectedSource, 0, pusherSources.Count);
            fieldNames = pusherSources[clamp].GetType().GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.Name).ToList();
            SelectedData = EditorGUILayout.Popup(SelectedData, fieldNames.ToArray());
            if (SelectedData >= 0 && SelectedData < fieldNames.Count && fieldNames.Count > 0)
            {
                PropertyInfo propertyType = pusherSources[clamp].GetType().GetProperty(fieldNames[SelectedData]);
                if (propertyType != null && propertyType.PropertyType.GetInterface(nameof(IDataPusher)) != null)
                {
                    SelectedSubData = EditorGUILayout.Popup(SelectedSubData, propertyType.PropertyType.GetProperties().Select(x => x.Name).ToArray());
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}*/