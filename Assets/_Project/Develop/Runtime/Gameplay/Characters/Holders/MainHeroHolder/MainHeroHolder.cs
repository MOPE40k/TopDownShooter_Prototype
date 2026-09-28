using System;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Holders.MainHeroHolder
{
    public class MainHeroHolder : IMainHeroProvider, IMainHeroRegisterer
    {
        // Delegates
        public event Action<MainHeroCharacter> MainHeroCreated = default;

        // Runtime
        public MainHeroCharacter MainHero { get; private set; } = default;

        public void Register(MainHeroCharacter mainHero)
        {
            MainHero = mainHero;

            MainHeroCreated?.Invoke(MainHero);
        }

        public void Unregister()
            => MainHero = null;
    }
}