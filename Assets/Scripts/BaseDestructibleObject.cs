using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class BaseDestructibleObject : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _destructionEffectVFX;

        public ParticleSystem DestructionEffectVFX => _destructionEffectVFX;

        public event EventHandler OnObjectTakeDamage;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out Weapon _))
            {
                OnObjectTakeDamage?.Invoke(this, EventArgs.Empty);
                Destroy(gameObject);
            }
        }
    }
}
