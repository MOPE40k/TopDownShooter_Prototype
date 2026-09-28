using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.UI.Popups.Gameplay
{
    public abstract class PopupBase : MonoBehaviour
    {
        public virtual void Show()
            => gameObject.SetActive(true);

        public virtual void Hide()
            => gameObject.SetActive(false);
    }
}