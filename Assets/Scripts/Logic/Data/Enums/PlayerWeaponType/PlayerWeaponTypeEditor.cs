using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TriInspector;
using UnityEngine;
using Utilities.DynamicEnumsCreator;

namespace CustomDynamicEnums
{
    [CreateAssetMenu(fileName = "PlayerWeaponTypeEditor", menuName = "DynamicEnums/PlayerWeaponTypeEditor")]
    public class PlayerWeaponTypeEditor : ScriptableObject
    {
        [InfoBox("Элементы могут содержать только латинские символы и цифры и начинаться обязательно с буквы. Запрещены повторяющиеся элементы.")]
        [SerializeField] private List<PlayerWeaponTypeEditorElement> dynamicEnum = new List<PlayerWeaponTypeEditorElement>();
        
        [InfoBox("Не забудьте сохранить список после изменений, чтобы он был включен в динамический Enum.")]
        [EnableIf("ValidateAll")]
        [Button()]
        private void Save()
        {
#if UNITY_EDITOR
            DynamicEnums.ReplaceAndSave("PlayerWeaponTypeEnum", dynamicEnum.Select(e => e.Type).ToList());
#endif
        }
        
#region Validation

        private bool ValidateAll => ValidateStrings(dynamicEnum) && ValidateDuplicate(dynamicEnum);

        private bool ValidateStrings(List<PlayerWeaponTypeEditorElement> list)
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

        private bool ValidateDuplicate(List<PlayerWeaponTypeEditorElement> list)
        {
            return list.GroupBy(x => x.Type).All(g => g.Count() == 1);
        }
        
#endregion
    }
    
    [Serializable]
    public class PlayerWeaponTypeEditorElement
    {
        public string Type = "";

        public PlayerWeaponTypeEditorElement() { }

        public PlayerWeaponTypeEditorElement(string type)
        {
            Type = type;
        }
    }
}