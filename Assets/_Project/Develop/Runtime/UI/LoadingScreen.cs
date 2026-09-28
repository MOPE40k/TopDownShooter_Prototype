using TMPro;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.UI.Popups.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.UI
{
    public class LoadingScreen : PopupBase
    {
        [Header("References:")]
        [SerializeField] private TMP_Text _text = null;
        [SerializeField] private Image _loadingInProgressIndicator = null;

        [Space]
        [Header("Settings:")]
        [SerializeField] private float _indicatorRotationSpeed = 100f;

        private void Update()
        {
            if (!gameObject.activeSelf)
                return;

            _loadingInProgressIndicator.transform.Rotate(Vector3.forward, _indicatorRotationSpeed * Time.deltaTime, Space.World);
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        public void ShowMessage(string text)
            => _text.SetText(text);
    }
}