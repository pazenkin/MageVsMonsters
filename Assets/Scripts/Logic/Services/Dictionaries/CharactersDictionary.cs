using System.Collections.Generic;
using Logic.Characters;
using UnityEngine;

namespace Logic.Services.Dictionaries
{
    /// <summary>
    /// Библиотека игровых персонажей (для быстрого получения компонентов по ссылке на GameObject)
    /// </summary>
    public class CharactersDictionary
    {
        private readonly Dictionary<GameObject, Character> _value = new();

        public void Add(GameObject gameObject, Character character)
        {
            _value[gameObject] = character;
        }
        
        public void Add(Character character)
        {
            _value[character.gameObject] = character;
        }

        public void Add(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out Character character))
            {
                Add(gameObject, character);
            }
        }

        public void Remove(GameObject gameObject)
        {
            if (!_value.ContainsKey(gameObject)) return;
            _value.Remove(gameObject);
        }

        public void Remove(Character character)
        {
            Remove(character.gameObject);
        }

        public void Clear()
        {
            _value.Clear();
        }

        public bool TryGetCharacter(GameObject gameObject, out Character character)
        {
            return _value.TryGetValue(gameObject, out character);
        }
    }
}