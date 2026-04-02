using System;
using UnityEngine;
using static Assets.Scripts.Paths.ResourcePaths;

namespace Assets.Scripts.VisualEffects
{
    public class DestructionHandler : MonoBehaviour
    {
        private ParticleSystem _destructionEffectVFX;
        private BaseDestructibleObject _destructibleObject;

        [SerializeField] private float _destructionEffectDuration = 0.5f;

        private void Awake()
        {
            _destructibleObject = GetComponent<BaseDestructibleObject>();

            _destructionEffectVFX = _destructibleObject.DestructionEffectVFX == null ?
                Resources.Load<ParticleSystem>(VFX.DESTRUCTION_EFFECT) : 
                _destructibleObject.DestructionEffectVFX;
        }

        private void Start()
        {
            _destructibleObject.OnObjectTakeDamage += _destructibleObject_OnObjectTakeDamage;
        }

        private void OnDestroy()
        {
            _destructibleObject.OnObjectTakeDamage -= _destructibleObject_OnObjectTakeDamage;
        }

        private void _destructibleObject_OnObjectTakeDamage(object sender, EventArgs e)
        {
            var destructionEffect = Instantiate(_destructionEffectVFX, _destructibleObject.transform.position, Quaternion.identity);

            Destroy(destructionEffect.gameObject, _destructionEffectDuration);
        }
    }
}
