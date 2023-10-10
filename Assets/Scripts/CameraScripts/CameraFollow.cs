using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.CameraScripts {
    public class CameraFollow : MonoBehaviour {
        public Transform target; // Цель, за которой следует камера (ваш персонаж)
        public float smoothSpeed = 0.125f; // Скорость плавного следования камеры
        public Vector3 offset; // Смещение камеры относительно персонажа

        private void FixedUpdate() {
            if (target == null) return;

            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}