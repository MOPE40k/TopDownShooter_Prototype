namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Holders.MainHeroHolder
{
    public interface IMainHeroRegisterer
    {
        void Register(MainHeroCharacter mainHero);
        void Unregister();
    }
}