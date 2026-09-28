using System;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Holders.MainHeroHolder
{
    public interface IMainHeroProvider
    {
        // Delegates
        event Action<MainHeroCharacter> MainHeroCreated;

        // Runtime
        MainHeroCharacter MainHero { get; }
    }
}