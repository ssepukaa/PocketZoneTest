using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player {
    public class PlayerInput : MonoBehaviour {
        [SerializeField]private float _speed;
        private Vector2 _movement;
        private Rigidbody2D _rb;

        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnMovement(InputValue value) {
            _movement = value.Get<Vector2>();
        }

        private void OnFire() {
            Debug.Log("Fire!");
        }
        private void FixedUpdate() {
            _rb.MovePosition(_rb.position + _movement*Time.fixedDeltaTime*_speed);
        }
    }
}
