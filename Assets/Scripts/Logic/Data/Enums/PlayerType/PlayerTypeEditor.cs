using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TriInspector;
using UnityEngine;
using Utilities.DynamicEnumsCreator;

namespace CustomDynamicEnums
{
    [CreateAssetMenu(fileName = "PlayerTypeEditor", menuName = "DynamicEnums/PlayerTypeEditor")]
    public class PlayerTypeEditor : ScriptableObject
    {
        [InfoBox("Элементы могут содержать только латинские символы и цифры и начинаться обязательно с буквы. Запрещены повторяющиеся элементы.")]
        [SerializeField] private List<PlayerTypeEditorElement> dynamicEnum = new List<PlayerTypeEditorElement>();
        
        [InfoBox("Не забудьте сохранить список после изменений, чтобы он был включен в динамический Enum.")]
        [EnableIf("ValidateAll")]
        [Button()]
        private void Save()
        {
#if UNITY_EDITOR
            DynamicEnums.ReplaceAndSave("PlayerTypeEnum", dynamicEnum.Select(e => e.Type).ToList());
#endif
        }
        
#region Validation

        private bool ValidateAll => ValidateStrings(dynamicEnum) && ValidateDuplicate(dynamicEnum);

        private bool ValidateStrings(List<PlayerTypeEditorElement> list)
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

        private bool ValidateDuplicate(List<PlayerTypeEditorElement> list)
        {
            return list.GroupBy(x => x.Type).All(g => g.Count() == 1);
        }
        
#endregion
    }
    
    [Serializable]
    public class PlayerTypeEditorElement
    {
        public string Type = "";

        public PlayerTypeEditorElement() { }

        public PlayerTypeEditorElement(string type)
        {
            Type = type;
        }
    }
}