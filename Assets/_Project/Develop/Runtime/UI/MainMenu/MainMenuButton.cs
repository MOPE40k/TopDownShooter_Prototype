using TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils.ScenesManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuButton : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private Button _playButton = null;

        private void OnEnable()
            => _playButton.onClick.AddListener(LoadLogicScene);

        private void OnDisable()
            => _playButton.onClick.RemoveListener(LoadLogicScene);

        private void LoadLogicScene()
            => SceneManager.LoadScene(Scenes.GameCycle);
    }
}