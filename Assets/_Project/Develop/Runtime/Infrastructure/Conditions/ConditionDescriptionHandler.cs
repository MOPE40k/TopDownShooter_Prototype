using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Infrastructure.Conditions
{
    public static class ConditionDescriptionHandler
    {
        public static string GetConditionsDescription(LevelConfig levelConfig)
        {
            string resultDescription = string.Empty;

            if (levelConfig.WinCondition == GameModeConditionTypes.ByAmountOfTime)
                resultDescription += $"Survive for {levelConfig.TimeToWin} seconds!\n";
            else if (levelConfig.WinCondition == GameModeConditionTypes.ByNumberDestroyed)
                resultDescription += $"Destroy {levelConfig.DestroyedEnemiesCountToWin} enemies.\n";

            if (levelConfig.DefeatCondition == GameModeConditionTypes.NumberOfEnemiesIsTooLarge)
                resultDescription += $"There should be no more than {levelConfig.EnemiesCountToDefeat} enemies!\n";

            resultDescription += $"Don't die and Good luck!";

            return resultDescription;
        }
    }
}