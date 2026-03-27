using TMPro;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public class PopUpDamage : MonoBehaviour
    {
        [SerializeField] private Vector2 _initialVelocity = new Vector2(10, 2);
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void DrawDamage<T>(T damage, Vector2 direction, float lifetime = 0.5f, bool destroy = true)
        {
            _rigidbody2D.linearVelocity = _initialVelocity * direction;

            GetComponentInChildren<TMP_Text>().text = damage.ToString();

            if(destroy)
                Destroy(gameObject, lifetime);
        }
    }
}
