using UnityEngine;

namespace Assets.Scripts.Player.Abstract {
    // Триггер обнаружения лута
    public interface IPlayerLootTrigger {
        bool Construct(IPlayerController player);
    }
}
