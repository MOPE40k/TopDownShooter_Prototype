using UnityEngine;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Shootable;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalRotation;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public class ControllersFactory
    {
        private readonly InputSystemActions _inputActions = default;

        public ControllersFactory(InputSystemActions inputActions)
            => _inputActions = inputActions;

        public KeyboardInputManagerCharacterMovableController GetKeyboardInputManagerCharacterMovableController(
            IDirectionalMovable movable)
        {
            return new KeyboardInputManagerCharacterMovableController(movable);
        }

        public KeyboardInputSystemCharacterMovableController GetKeyboardInputSystemCharacterMovableController(
            IDirectionalMovable movable)
        {
            return new KeyboardInputSystemCharacterMovableController(_inputActions, movable);
        }

        public KeyboardInputManagerShooterController GetKeyboardInputManagerShooterController(
            IShootable shootable)
        {
            return new KeyboardInputManagerShooterController(shootable);
        }

        public KeyboardInputSystemCharacterShooterController GetKeyboardInputSystemCharacterShooterController(
            IShootable shootable)
        {
            return new KeyboardInputSystemCharacterShooterController(_inputActions, shootable);
        }

        public AlongDirectionRotatableController GetAlongDirectionRotatableController(IDirectionalMovable movable, IDirectionalRotatable rotatable)
            => new AlongDirectionRotatableController(movable, rotatable);

        public RandomDirectionMovableController GetRandomDirectionMovableController(IDirectionalMovable movable, float changeDirectionInterval)
            => new RandomDirectionMovableController(movable, changeDirectionInterval);

        public ToTargetDirectionMovableController GetToTargetDirectionMovableController(IDirectionalMovable movable, Transform target)
            => new ToTargetDirectionMovableController(movable, target);

        public CompositeController GetKeyboardInputSystemMoveShootController(
            IDirectionalMovable movable,
            IDirectionalRotatable rotatable,
            IShootable shootable)
        {
            return new CompositeController(
                new Controller[]
                {
                    GetKeyboardInputSystemCharacterMovableController(movable),
                    GetAlongDirectionRotatableController(movable, rotatable),
                    GetKeyboardInputSystemCharacterShooterController(shootable)
                });
        }

        public CompositeController GetKeyboardInputManagerMoveShootController(
            IDirectionalMovable movable,
            IDirectionalRotatable rotatable,
            IShootable shootable)
        {
            return new CompositeController(
                new Controller[]
                {
                    GetKeyboardInputManagerCharacterMovableController(movable),
                    GetAlongDirectionRotatableController(movable, rotatable),
                    GetKeyboardInputManagerShooterController(shootable)
                });
        }

        public CompositeController GetRandomDirectionMoveRotateController(
            IDirectionalMovable movable,
            IDirectionalRotatable rotatable,
            float changeDirectionInterval)
        {
            return new CompositeController(new Controller[]
            {
                GetRandomDirectionMovableController(movable, changeDirectionInterval),
                GetAlongDirectionRotatableController(movable, rotatable)
            });
        }

        public CompositeController GetToTargetDirectionMoveRotateController(
            IDirectionalMovable movable,
            IDirectionalRotatable rotatable,
            Transform target)
        {
            return new CompositeController(new Controller[]
            {
                GetToTargetDirectionMovableController(movable, target),
                GetAlongDirectionRotatableController(movable, rotatable)
            });
        }

        public void Release(Controller controller)
            => controller.Dispose();
    }
}