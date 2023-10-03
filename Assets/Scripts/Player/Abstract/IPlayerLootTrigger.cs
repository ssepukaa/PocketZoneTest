using UnityEngine;

namespace Assets.Scripts.Player.Abstract {
    public interface IPlayerLootTrigger {
        bool Construct(IPlayerController player);
    }
}
