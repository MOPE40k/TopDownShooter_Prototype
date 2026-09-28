using UnityEngine;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.SpawnFeature;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters
{
    public class SpawnView : MonoBehaviour, IInitializable
    {
        // Consts
        private const string MaterialEdgeKey = "_Edge";

        // References
        private ICanSpawn _spawnEntity = null;
        private Renderer[] _renderers = null;

        // Runtime
        private bool _isInit = false;

        public void Init()
        {
            _spawnEntity = GetComponentInParent<ICanSpawn>();

            _renderers = GetComponentsInChildren<Renderer>();

            UpdateRenderers();

            _isInit = true;
        }

        private void Update()
        {
            if (!_isInit)
                return;

            UpdateRenderers();
        }

        private void UpdateRenderers()
        {
            if (_spawnEntity.InSpawnProcess(out float elapsedTime))
                Spawn(elapsedTime);
            else
                Reset();
        }

        private void Reset()
            => SetFloatFor(_renderers, MaterialEdgeKey, 0f);

        private void Spawn(float elapsedTime)
            => SetFloatFor(_renderers, MaterialEdgeKey, 1f - elapsedTime / _spawnEntity.TimeToSpawn);

        private void Despawn(float elapsedTime)
            => SetFloatFor(_renderers, MaterialEdgeKey, elapsedTime / _spawnEntity.TimeToSpawn);

        private void SetFloatFor(Renderer[] renderers, string key, float value)
        {
            foreach (Renderer renderer in renderers)
                renderer.material.SetFloat(key, value);
        }
    }
}