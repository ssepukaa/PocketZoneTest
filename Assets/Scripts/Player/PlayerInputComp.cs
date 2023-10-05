using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player {
    public class PlayerInputComp : MonoBehaviour {
        [SerializeField]private float _speed;
        private Vector2 _movement;
        private Rigidbody2D _rb;
        private PlayerController _player;
        private PlayerInput _input;

        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _input = GetComponent<PlayerInput>();
        }

        public void Construct(PlayerController player) {
            _player = player;
        }

        private void OnMovement(InputValue value) {
            _movement = value.Get<Vector2>();
        }

        // private void OnFire() {
        //     Debug.Log("Fire!");
        //     Player.StartFire();
        // }

        // private void OnInventory() {
        //     Player.OpenInventory();
        // }

        private void FixedUpdate() {
            _rb.MovePosition(_rb.position + _movement*Time.fixedDeltaTime*_speed);
        }
    }
}
