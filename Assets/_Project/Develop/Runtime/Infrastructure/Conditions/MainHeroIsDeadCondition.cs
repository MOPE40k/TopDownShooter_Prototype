using System;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Infrastructure.Conditions
{
    public class MainHeroIsDeadCondition : IGameModeCondition
    {
        // Delegates
        public event Action Completed;

        // References
        private readonly MainHeroCharacter _mainHeroCharacter = null;

        public MainHeroIsDeadCondition(MainHeroCharacter mainHeroCharacter)
            => _mainHeroCharacter = mainHeroCharacter;

        public void UpdateTick(float deltaTime)
        {
            if (_mainHeroCharacter.IsDead)
                Completed?.Invoke();
        }
    }
}