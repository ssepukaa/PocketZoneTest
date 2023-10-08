using System.Collections;
using Assets.Scripts.Enemy.Abstract;
using Assets.Scripts.Player.Abstract;
using UnityEngine;

namespace Assets.Scripts.Enemy.DataRes {
    [CreateAssetMenu(fileName = "EnemyResourceData", menuName = "PocketZoneTest/EnemyResourceData")]

    public class EnemyResourceData : ScriptableObject {

        public Sprite _spriteEnemy;
        public IPlayerController Target;
    }
}