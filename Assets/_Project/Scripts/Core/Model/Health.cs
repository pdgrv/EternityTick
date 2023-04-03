using System;

namespace Eternity.Core.Model
{
    public class Health
    {
        public event Action<int> HealthChanged;
        public event Action Die;

        private int _value;

        public Health(int maxHealth)
        {
            _value = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage == 0)
                return;

            _value = Math.Max(_value - damage, 0);
            HealthChanged?.Invoke(_value);

            if (_value == 0)
                Die?.Invoke();
        }
    }
}