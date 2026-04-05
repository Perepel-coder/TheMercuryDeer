using System;
using UnityEngine;

namespace Assets.Scripts.Services.VisualEffects
{
    public class BaseDestructibleObjectService : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _destructionEffectVFX;

        public ParticleSystem DestructionEffectVFX => _destructionEffectVFX;

        public event EventHandler OnObjectTakeDamage;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out WeaponService _))
            {
                OnObjectTakeDamage?.Invoke(this, EventArgs.Empty);
                Destroy(gameObject);
            }
        }
    }
}
