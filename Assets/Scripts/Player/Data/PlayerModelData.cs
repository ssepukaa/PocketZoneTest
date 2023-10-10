using System;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Infra.Game;
using UnityEngine;

namespace Assets.Scripts.Player.Data {
    [Serializable]
    public class PlayerModelData : MonoBehaviour {
        [SerializeField] private float _currentHealth;

        public float CurrentHealth {
            get => _currentHealth;
            set => _currentHealth = value;
        }
    }
}
