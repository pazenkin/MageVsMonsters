namespace Logic.Characters
{
    /// <summary>
    /// Калькулятор нанесенного персонажу урона
    /// </summary>
    public class DamageBuffer
    {
        private DamageBuffer() { }
        
        public DamageBuffer(Health health, Armor armor)
        {
            _health = health;
            _armor = armor;
        }
        
        private readonly Health _health;
        private readonly Armor _armor;
        
        public void SetDamage(float damageValue)
        {
            _health.Take(damageValue * (1f - _armor.Value));
        }
    }
}