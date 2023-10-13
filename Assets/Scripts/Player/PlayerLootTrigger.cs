using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Player.Abstract;
using UnityEngine;

namespace Assets.Scripts.Player {
    class PlayerLootTrigger : MonoBehaviour, IPlayerLootTrigger {
        IPlayerController _c;
       
        public bool Construct(IPlayerController player) {
            _c = player;
            
            return true;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("OnTrigger Enter!");
            ILootContainer loot = other.GetComponent<ILootContainer>();
            if (loot != null) {
                Debug.Log($"PlayerController not null init = {_c!= null}");
                _c.CollectLoot(other, loot.CollectLoot()) ;
            }
        }
    }
}