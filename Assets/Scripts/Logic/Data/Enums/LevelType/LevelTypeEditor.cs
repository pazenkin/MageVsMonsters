using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TriInspector;
using UnityEngine;
using Utilities.DynamicEnumsCreator;

namespace CustomDynamicEnums
{
    [CreateAssetMenu(fileName = "LevelTypeEditor", menuName = "DynamicEnums/LevelTypeEditor")]
    public class LevelTypeEditor : ScriptableObject
    {
        [InfoBox("Элементы могут содержать только латинские символы и цифры и начинаться обязательно с буквы. Запрещены повторяющиеся элементы.")]
        [SerializeField] private List<LevelTypeEditorElement> dynamicEnum = new List<LevelTypeEditorElement>();
        
        [InfoBox("Не забудьте сохранить список после изменений, чтобы он был включен в динамический Enum.")]
        [EnableIf("ValidateAll")]
        [Button()]
        private void Save()
        {
#if UNITY_EDITOR
            DynamicEnums.ReplaceAndSave("LevelTypeEnum", dynamicEnum.Select(e => e.Type).ToList());
#endif
        }
        
#region Validation

        private bool ValidateAll => ValidateStrings(dynamicEnum) && ValidateDuplicate(dynamicEnum);

        private bool ValidateStrings(List<LevelTypeEditorElement> list)
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

        private bool ValidateDuplicate(List<LevelTypeEditorElement> list)
        {
            return list.GroupBy(x => x.Type).All(g => g.Count() == 1);
        }
        
#endregion
    }
    
    [Serializable]
    public class LevelTypeEditorElement
    {
        public string Type = "";

        public LevelTypeEditorElement() { }

        public LevelTypeEditorElement(string type)
        {
            Type = type;
        }
    }
}