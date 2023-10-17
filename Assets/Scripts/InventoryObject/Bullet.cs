using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.Main.Game.Abstract;
using UnityEngine;

namespace Assets.Scripts.InventoryObject {
    public class Bullet : MonoBehaviour, IBullet {
        public float speed = 10f; // Скорость пули
        IInventoryItem _item;
        SpriteRenderer _renderer;

        private void Awake() {
            _renderer = GetComponentInChildren<SpriteRenderer>();

        }

        public bool Construct(IDamageSystem listener, object sender, IInventoryItemInfo info, int amount) {

            _item = new InventoryItem(info);
            _renderer.sprite = info.AmmoInfo.BulletSprite;
            _item.Amount = amount;
            return true;
        }


        private void Update() {
            // Перемещение пули вперед по оси Y
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            IDamageable damageableObject = collision.gameObject.GetComponent<IDamageable>();
            if (damageableObject != null) {
                // Теперь вы можете вызывать методы интерфейса IDamageable
                damageableObject.TakeDamage(_item.Info.AmmoInfo.Damage);
                Debug.Log($"Apply damage {_item.Info.AmmoInfo.Damage}");
            }
            // Если пуля сталкивается с объектом (например, врагом), уничтожаем ее
            Destroy(gameObject);
        }
    }
}