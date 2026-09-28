using System.Collections;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Holders.MainHeroHolder;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Shootable;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.SpawnFeature;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Infrastructure.Conditions;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.UI;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Characters;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using UnityEngine;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.UI.Popups.Gameplay;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.CameraFeatures;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        // Consts
        private const string MainHeroConfigPath = "Configs/Gameplay/Entities/MainHeroConfig";
        private const string LevelsConfigPath = "Configs/Gameplay/Levels/LevelsListConfig";
        private const string BulletConfigPath = "Configs/Gameplay/Entities/BulletConfig";
        private const string LoadingMessage = "LOADING ...";

        [Header("References:")]
        [SerializeField] private LoadingScreen _loadingScreen = default;
        [SerializeField] private ConfirmPopup _confirmPopup = default;
        [SerializeField] private MobileControlScreen _mobileControlScreen = default;

        // References
        private InputSystemActions _inputActions = default;
        private ControllersUpdateService _controllersUpdateService = default;
        private GameplayCycle _gameplayCycle = default;

        private void Awake()
            => StartCoroutine(LoadingProcess());

        private IEnumerator LoadingProcess()
        {
            _loadingScreen.Show();
            _loadingScreen.ShowMessage(LoadingMessage);

            MainHeroConfig mainHeroConfig = Resources
                .Load<MainHeroConfig>(MainHeroConfigPath);

            LevelsListConfig levelsListConfig = Resources
                .Load<LevelsListConfig>(LevelsConfigPath);

            BulletConfig bulletConfig = Resources
                .Load<BulletConfig>(BulletConfigPath);

            _inputActions = new();
            // _inputActions.Enable();

            _controllersUpdateService = new ControllersUpdateService();

            CharactersFactory charactersFactory = new CharactersFactory();
            CamerasFactory camerasFactory = new CamerasFactory();
            ControllersFactory controllersFactory = new ControllersFactory(_inputActions);
            ProjectilesFactory bulletsFactory = new ProjectilesFactory();

            MainHeroFactory mainHeroFactory = new MainHeroFactory(
                charactersFactory,
                camerasFactory,
                controllersFactory,
                _controllersUpdateService);

            EnemiesFactory enemiesFactory = new EnemiesFactory(
                charactersFactory,
                controllersFactory,
                _controllersUpdateService);

            LevelConfig levelConfig = levelsListConfig.GetRandom();

            EnemiesSpawnService enemiesSpawner = new EnemiesSpawnService(
                enemiesFactory,
                this,
                levelConfig.EnemiesSpawnInterval);

            MainHeroHolder mainHeroHolder = new MainHeroHolder();

            CondititionsFactory condititionsFactory = new CondititionsFactory(
                levelConfig,
                enemiesSpawner,
                mainHeroHolder);

            _gameplayCycle = new GameplayCycle(
                _inputActions,
                mainHeroFactory,
                condititionsFactory,
                bulletsFactory,
                mainHeroConfig,
                bulletConfig,
                levelConfig,
                mainHeroHolder,
                _confirmPopup,
                _mobileControlScreen,
                enemiesSpawner,
                this);

            yield return new WaitForSeconds(1.5f); // TEMP LOAD PROGRESS DEMO

            yield return _gameplayCycle.Prepare();

            _loadingScreen.Hide();

            yield return _gameplayCycle.Launch();
        }

        private void Update()
        {
            float dt = Time.deltaTime;

            _controllersUpdateService?.UpdateTick(dt);

            _gameplayCycle?.UpdateTick(dt);
        }

        private void OnDestroy()
        {
            _inputActions.Disable();
            _gameplayCycle?.Dispose();
        }
    }
}