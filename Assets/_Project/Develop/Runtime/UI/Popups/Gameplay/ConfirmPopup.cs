using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.UI.Popups.Gameplay
{
    public class ConfirmPopup : PopupBase
    {
        [Header("References:")]
        [SerializeField] private TMP_Text _messageText = default;

        public void ShowMessage(string text)
            => _messageText.SetText(text);

        public IEnumerator WaitConfirm(InputAction inputAction)
        {
            yield return new WaitWhile(
                () => !inputAction.triggered);
        }
        // public IEnumerator WaitConfirm(KeyCode confirmKey)
        // {
        //     yield return new WaitWhile(
        //         () => !Input.GetKeyDown(confirmKey));
        // }
    }
}