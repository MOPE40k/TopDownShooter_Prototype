using TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils.Reactive;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Entities
{
    public class HealthService : IHealthService
    {
        public ReactiveVeriable<float> _max = default;
        public ReactiveVeriable<float> _current = default;

        public HealthService(float max)
        {
            _max = new ReactiveVeriable<float>(max);
            _current = new ReactiveVeriable<float>(max);
        }

        public IReadOnlyReactiveVeriable<float> Max => _max;
        public IReadOnlyReactiveVeriable<float> Current => _current;

        public void Add(float value)
        {
            ZeroCheck(value);

            _current.Value = Mathf.Max(_current.Value + value, _max.Value);
        }

        public void Reduce(float value)
        {
            ZeroCheck(value);

            _current.Value = Mathf.Max(_current.Value - value, 0f);
        }

        private void ZeroCheck(float value)
        {
            if (value < 0)
                throw new System.ArgumentException(nameof(value), "Less than or equal to zero!");
        }
    }
}