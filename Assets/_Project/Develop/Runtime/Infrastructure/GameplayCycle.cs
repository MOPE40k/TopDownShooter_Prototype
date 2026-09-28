using System;
using System.Collections;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Holders.MainHeroHolder;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Shootable;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.SpawnFeature;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Infrastructure.Conditions;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Characters;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using UnityEngine;
using UnityEngine.SceneManagement;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils.ScenesManagement;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.UI.Popups.Gameplay;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Infrastructure
{
    public class GameplayCycle : IDisposable
    {
        // Consts
        private const KeyCode LaunchKeyCode = KeyCode.F;

        // References
        private readonly InputSystemActions _inputActions = default;
        private readonly MainHeroFactory _mainHeroFactory = default;
        private readonly CondititionsFactory _condititionsFactory = default;
        private readonly ProjectilesFactory _bulletsFactory = default;
        private readonly MainHeroConfig _mainHeroConfig = default;
        private readonly BulletConfig _bulletConfig = default;
        private readonly IMainHeroRegisterer _mainHeroRegister = default;
        private readonly LevelConfig _levelConfig = default;
        private readonly ConfirmPopup _confirmPopup = default;
        private readonly MobileControlScreen _mobileControlScreen = default;
        private GameMode _gameMode = default;
        private readonly EnemiesSpawnService _enemiesSpawner = default;
        private readonly MonoBehaviour _coroutineRunner = default;

        private MainHeroCharacter _mainHero = default;

        public GameplayCycle(
            InputSystemActions inputActions,
            MainHeroFactory mainHeroFactory,
            CondititionsFactory condititionsFactory,
            ProjectilesFactory bulletsFactory,
            MainHeroConfig mainHeroConfig,
            BulletConfig bulletConfig,
            LevelConfig levelConfig,
            IMainHeroRegisterer mainHeroRegister,
            ConfirmPopup confirmPopup,
            MobileControlScreen mobileControlScreen,
            EnemiesSpawnService enemiesSpawner,
            MonoBehaviour coroutineRunner)
        {
            _inputActions = inputActions;
            _mainHeroFactory = mainHeroFactory;
            _condititionsFactory = condititionsFactory;
            _bulletsFactory = bulletsFactory;
            _mainHeroConfig = mainHeroConfig;
            _levelConfig = levelConfig;
            _bulletConfig = bulletConfig;
            _mainHeroRegister = mainHeroRegister;
            _confirmPopup = confirmPopup;
            _mobileControlScreen = mobileControlScreen;
            _enemiesSpawner = enemiesSpawner;
            _coroutineRunner = coroutineRunner;
        }

        public IEnumerator Prepare()
        {
            _confirmPopup.Hide();
            _mobileControlScreen.Hide();

            yield return SceneManager.LoadSceneAsync(
                _levelConfig.EnviromentSceneName,
                LoadSceneMode.Additive);
        }

        public IEnumerator Launch()
        {
            _inputActions.Preparing.Enable();
            _inputActions.Player.Disable();

            CreateMainHero();

            var levelDescription = ConditionDescriptionHandler
                .GetConditionsDescription(_levelConfig);

            _confirmPopup.Show();
            _confirmPopup.ShowMessage(
                $"{levelDescription}\nPress {LaunchKeyCode.ToString()} or FIRE button on screen for begin!");

            _mobileControlScreen.Show();

            yield return _confirmPopup.WaitConfirm(_inputActions.Preparing.StartGame);

            _inputActions.Preparing.Disable();
            _inputActions.Player.Enable();

            _confirmPopup.Hide();

            _gameMode = new GameMode(
                _levelConfig,
                _condititionsFactory,
                _mainHeroRegister as IMainHeroProvider,
                _enemiesSpawner);

            _gameMode.Win += OnGameModeWin;
            _gameMode.Defeat += OnGameModeDefeat;

            _gameMode.Start();
        }

        public void UpdateTick(float deltaTime)
            => _gameMode?.UpdateTick(deltaTime);

        public void Dispose()
            => OnGameModeEnded();

        private void CreateMainHero()
        {
            _mainHero = _mainHeroFactory.GetMainHero(
                _mainHeroConfig,
                _bulletsFactory,
                _bulletConfig,
                _levelConfig.MainHeroSpawnPoint);

            _mainHeroRegister.Register(_mainHero);
        }

        private void OnGameModeWin()
        {
            Debug.Log("Win");

            OnGameModeEnded();

            SceneManager.LoadScene(Scenes.Menu);
        }

        private void OnGameModeDefeat()
        {
            Debug.Log("Defeat");

            OnGameModeEnded();

            _coroutineRunner.StartCoroutine(Launch());
        }

        private void OnGameModeEnded()
        {
            if (_gameMode == null)
                return;

            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;
            _gameMode.Dispose();

            ReleaseMainHero();
        }

        private void ReleaseMainHero()
        {
            _mainHeroFactory.ReleaseCharacter(_mainHero);

            _mainHeroRegister.Unregister();
        }
    }
}