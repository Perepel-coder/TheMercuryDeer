using System;
using UnityEngine;
using static Assets.Scripts.Constans.Paths.ResourcePaths;

namespace Assets.Scripts.Services.VisualEffects
{
    public class DestructionHandlerService : MonoBehaviour
    {
        private ParticleSystem _destructionEffectVFX;
        private BaseDestructibleObjectService _destructibleObject;

        private void Awake()
        {
            _destructibleObject = GetComponent<BaseDestructibleObjectService>();

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
        }
    }
}
