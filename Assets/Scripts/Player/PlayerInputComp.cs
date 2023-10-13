using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player {
    public class PlayerInputComp : MonoBehaviour {
        [SerializeField] float _speed;
        Vector2 _movement;
        Rigidbody2D _rb;
        PlayerController _player;
        PlayerInput _input;

        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _input = GetComponent<PlayerInput>();
        }

        public bool Construct(PlayerController player) {
            _player = player;
            return true;
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
