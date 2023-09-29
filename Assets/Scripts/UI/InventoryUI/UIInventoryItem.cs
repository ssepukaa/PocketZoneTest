using Assets.Scripts.InventoryObject;
using Assets.Scripts.InventoryObject.Abstract;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.InventoryUI {
    public class UIInventoryItem : MonoBehaviour {
        [SerializeField] private Image _imageIcon;
        [SerializeField] private TMP_Text _textAmount;
        private IInventoryItem _item;
        
        public void Refresh(InventorySlot slot) {
            if (slot.GetIsEmpty()) {
                Cleanup();
                return;
            }

            _item = slot.GetItem();
            _imageIcon.sprite = _item.GetInfo().spriteIcon;
            _imageIcon.gameObject.SetActive(true);

            var textAmountEnabled = slot.GetAmount() > 1;
            _textAmount.gameObject.SetActive(textAmountEnabled);
            if (textAmountEnabled) {
                _textAmount.text = $"x{slot.GetAmount().ToString()}";
            }
        }

        private void Cleanup() {
            _textAmount.gameObject.SetActive(false);
            _imageIcon.gameObject.SetActive(false);
        }
    }
}



// using Assets.Scripts.Inventory.Abstract;
// using TMPro;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;
//
//
// namespace Assets.Scripts.UI.Inventory {
//     public class UIInventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
//         [SerializeField] private Image _imageIcon;
//         [SerializeField] private TMP_Text _textAmount;
//         public IInventoryItem item { get; private set; }
//         private RectTransform _rectTransform;
//         private Canvas _mainCanvas;
//         private CanvasGroup _canvasGroup;
//         private UIInventoryItem _uiInventoryItem;
//         private bool _isDragging = false; // Флаг, указывающий, началось ли перетаскивание
//         private void Start() {
//             _rectTransform = GetComponent<RectTransform>();
//             _mainCanvas = GetComponentInParent<Canvas>();
//             _canvasGroup = GetComponent<CanvasGroup>();
//         }
//         public void OnBeginDrag(PointerEventData eventData) {
//             _uiInventoryItem = GetComponent<UIInventoryItem>();
//             if (_uiInventoryItem == null || _uiInventoryItem.item == null) {
//                 return; // Если предмета нет, просто вернитесь и не начинайте перетаскивание
//             }
//             _isDragging = true; // Устанавливаем флаг перетаскивания
//             var slotTransform = _rectTransform.parent;
//             slotTransform.SetAsLastSibling();
//             _canvasGroup.blocksRaycasts = false;
//         }
//
//         public void OnDrag(PointerEventData eventData) {
//             if (!_isDragging) return; // Если перетаскивание не началось, просто вернитесь
//
//             _rectTransform.anchoredPosition += eventData.delta/_mainCanvas.scaleFactor;
//         }
//
//         public void OnEndDrag(PointerEventData eventData) {
//             if (!_isDragging) return; // Если перетаскивание не началось, просто вернитесь
//             transform.localPosition = Vector3.zero;
//             _canvasGroup.blocksRaycasts = true;
//             _isDragging = false; // Сбрасываем флаг перетаскивания
//         }
//         public void Refresh(IInventorySlot slot) {
//             
//
//             if (slot.isEmpty) {
//                 Cleanup();
//                 return;
//             }
//
//             item = slot.item;
//             _imageIcon.sprite = item.Info.spriteIcon;
//             _imageIcon.gameObject.SetActive(true);
//
//             var textAmountEnabled = slot.amount > 1;
//             _textAmount.gameObject.SetActive(textAmountEnabled);
//             if (textAmountEnabled) {
//                 _textAmount.text = $"x{slot.amount.ToString()}";
//                
//
//             }
//         }
//
//         private void Cleanup() {
//             _textAmount.gameObject.SetActive(false);
//             _imageIcon.gameObject.SetActive(false);
//         }
//     }
// }