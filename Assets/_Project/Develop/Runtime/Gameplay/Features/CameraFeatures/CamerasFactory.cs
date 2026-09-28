using Cinemachine;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.CameraFeatures
{
    public class CamerasFactory
    {
        // Consts
        private const string FollowVCameraPrefabPath = "Prefabs/FollowVCamera";
        private const string VCameraBoundsPrefabPath = "Prefabs/CameraBounds";

        private CinemachineVirtualCamera _camera = default;
        private BoxCollider _cameraBounds = default;

        public CinemachineVirtualCamera GetFollowCameraWithBoundsFor(MainHeroCharacter character)
        {
            ReleaseFollowCameraWithBounds();

            CinemachineVirtualCamera followCameraPrefab = Resources
                .Load<CinemachineVirtualCamera>(FollowVCameraPrefabPath);

            _camera = GameObject.Instantiate(followCameraPrefab);
            _camera.Follow = character.CameraTarget;

            if (_camera.TryGetComponent(out CinemachineConfiner confiner))
            {
                BoxCollider cameraBoundsPrefab = Resources
                    .Load<BoxCollider>(VCameraBoundsPrefabPath);

                _cameraBounds = GameObject.Instantiate(cameraBoundsPrefab);

                confiner.m_BoundingVolume = _cameraBounds;
            }

            return _camera;
        }

        private void ReleaseFollowCameraWithBounds()
        {
            if (_camera != null)
                GameObject.Destroy(_camera.gameObject);

            if (_cameraBounds != null)
                GameObject.Destroy(_cameraBounds.gameObject);
        }
    }
}