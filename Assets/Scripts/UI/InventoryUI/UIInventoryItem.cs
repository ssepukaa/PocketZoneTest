using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.InventoryUI {
    // Отображение предметов в Инвентаре
    public class UIInventoryItem : MonoBehaviour {
        [SerializeField] Image _imageIcon;
        [SerializeField] TMP_Text _textAmount;
        IInventoryItem _item;

        public void Refresh(IInventorySlot slot) {
            if (slot.IsEmpty) {
                Cleanup();
                return;
            }

            _item = slot.Item;
            _imageIcon.sprite = _item.Info.SpriteIcon;
            _imageIcon.gameObject.SetActive(true);

            var textAmountEnabled = slot.GetItemAmount > 1;
            _textAmount.gameObject.SetActive(textAmountEnabled);
            if (textAmountEnabled) {
                _textAmount.text = $"x{slot.GetItemAmount.ToString()}";
            }
        }

        private void Cleanup() {
            _textAmount.gameObject.SetActive(false);
            _imageIcon.gameObject.SetActive(false);
        }
    }
}

