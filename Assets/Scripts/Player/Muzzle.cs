using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Player {
    // Ствол оружия и эффект выстрела
    public class Muzzle: MonoBehaviour {
        private WeaponController _controller;
        private ParticleSystem _particle;

        private void Start() {
            _particle = GetComponentInChildren<ParticleSystem>();
            
        }
        public void Construct(WeaponController controller) {
            _controller = controller;
            
        }

        public void OnFired() {
            _particle.Play();  
        }

        public Transform GetMuzzleTransform() {
            return transform;
        }

       
    }
}
