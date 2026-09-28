namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.SpawnFeature
{
    public interface ICanSpawn
    {
        // Runtime
        float TimeToSpawn { get; }

        bool InSpawnProcess(out float elapsedTime);
    }
}