using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TriInspector;
using UnityEngine;
using Utilities.DynamicEnumsCreator;

namespace CustomDynamicEnums
{
    [CreateAssetMenu(fileName = "EnemyTypeEditor", menuName = "DynamicEnums/EnemyTypeEditor")]
    public class EnemyTypeEditor : ScriptableObject
    {
        [InfoBox("Элементы могут содержать только латинские символы и цифры и начинаться обязательно с буквы. Запрещены повторяющиеся элементы.")]
        [SerializeField] private List<EnemyTypeEditorElement> dynamicEnum = new List<EnemyTypeEditorElement>();
        
        [InfoBox("Не забудьте сохранить список после изменений, чтобы он был включен в динамический Enum.")]
        [EnableIf("ValidateAll")]
        [Button()]
        private void Save()
        {
#if UNITY_EDITOR
            DynamicEnums.ReplaceAndSave("EnemyTypeEnum", dynamicEnum.Select(e => e.Type).ToList());
#endif
        }
        
#region Validation

        private bool ValidateAll => ValidateStrings(dynamicEnum) && ValidateDuplicate(dynamicEnum);

        private bool ValidateStrings(List<EnemyTypeEditorElement> list)
        {
            var result = true;
            var pattern = @"^[A-Za-z][A-Za-z0-9]*$";
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (string.IsNullOrWhiteSpace(item.Type) || !Regex.IsMatch(item.Type, pattern))
                        result = false;
                }
            }

            return result;
        }

        private bool ValidateDuplicate(List<EnemyTypeEditorElement> list)
        {
            return list.GroupBy(x => x.Type).All(g => g.Count() == 1);
        }
        
#endregion
    }
    
    [Serializable]
    public class EnemyTypeEditorElement
    {
        public string Type = "";

        public EnemyTypeEditorElement() { }

        public EnemyTypeEditorElement(string type)
        {
            Type = type;
        }
    }
}