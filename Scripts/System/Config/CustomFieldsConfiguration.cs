using System;
using System.Collections.Generic;
using UnityEngine;
using UserSystemFramework.Scripts.System.Data.Enums;

namespace UserSystemFramework.Scripts.System.Config
{
    
    [Serializable]
    public class CustomFieldList
    {
        public Field field;
        public string fieldName;
        public string FieldValueString { get; private set; }
        public void Set(int val) => FieldValueString = val.ToString();
        public void Set(string val) => FieldValueString = val;
        public void Set(DateTime val) => FieldValueString = val.ToString("G");
        public void Set(bool val) => FieldValueString = val.ToString();
    }
    [CreateAssetMenu(fileName = "UserSystemFramework_CustomFieldsConfiguration", menuName = "USF/UserSystemFramework_CustomFieldsConfiguration", order = 0)]
    public class CustomFieldsConfiguration : ScriptableObject
    {
        public List<CustomFieldList> customUserFields;
    }
}