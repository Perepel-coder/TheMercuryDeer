using Assets.Scripts.Interfaces.Npc;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    class FlashBlink : MonoBehaviour
    {
        private Renderer _renderer;
        private IDamageable _damageableObject;
        private MaterialPropertyBlock _propBlock;

        private float _blinkDuration = 0.2f;
        private float _blinkTimer = 0f;
        private bool _isBlinking = false;

        private static readonly int FlashAmountId = Shader.PropertyToID("_FlashAmount");

        private void Awake()
        {
            _damageableObject = GetComponentInParent<IDamageable>();
            _renderer = GetComponent<Renderer>();
            _propBlock = new MaterialPropertyBlock();
        }

        private void Start()
        {
            _damageableObject.OnTakedDamage += (sender, args) => StartBlinking();
        }

        private void OnDestroy() => _damageableObject.OnTakedDamage -= (sender, args) => StartBlinking();

        private void Update()
        {
            if (_isBlinking)
            {
                if (_blinkTimer <= 0) StopBlinking();
                _blinkTimer -= Time.deltaTime;
            }
        }        

        private void SetFlash(float amount)
        {
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetFloat(FlashAmountId, amount);
            _renderer.SetPropertyBlock(_propBlock);
        }

        public void StartBlinking()
        {
            _isBlinking = true;
            _blinkTimer = _blinkDuration;
            SetFlash(1f);
        }

        public void StopBlinking()
        {
            SetFlash(0f);
            _isBlinking = false;
        }
    }
}