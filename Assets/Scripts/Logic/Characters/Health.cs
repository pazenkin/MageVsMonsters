using System;
using UnityEngine;

namespace Logic.Characters
{
    /// <summary>
    /// Информация о здоровье персонажа
    /// </summary>
    public class Health
    {
        private Health() { }

        public Health(float value)
        {
            MaxValue = value;
            CurrentValue = value;
        }

        public Action<float> OnPercentChange;
        public Action OnDied;
        
        public float MaxValue { get; private set; }
        public float CurrentValue
        {
            get => _value;
            private set
            {
                _value = Mathf.Clamp(value, 0f, MaxValue);
                OnPercentChange?.Invoke(PercentValue);
                if (_value <= float.Epsilon)
                {
                    OnDied?.Invoke();
                }
            }
        }
        public float PercentValue => MaxValue == 0f ? 0f : CurrentValue / MaxValue;

        private float _value;

        internal void Add(float value)
        {
            CurrentValue += value;
        }
        
        internal void Take(float value)
        {
            CurrentValue -= value;
        }
    }
}