using System;
using System.Collections;
using Assets.Scripts.InventoryObject.Abstract;
using Assets.Scripts.InventoryObject.Data;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Abstract;
using UnityEngine;

namespace Assets.Scripts.Weapon {
    // Реализация оружия
    public class WeaponController : MonoBehaviour, IWeaponController {
        public event Action<int, int> OnFiredChangedEnvent;

        public IInventory Inventory => _c.Inventory;

        IInventoryItemInfo _currentInf;

        IPlayerController _c;

        SpriteRenderer _spriteRenderer;

        Muzzle _muzzle;

        //bool _isInit;

        bool _isCooldowning;

        bool _isReloading;


        private void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _muzzle = GetComponentInChildren<Muzzle>();
            
        }

        public void Construct(IPlayerController playerController) {
            _c = playerController;
            _muzzle.Construct(this);
            _c.Inventory.OnAmmoChangedEvent += UpdateWeapon;
            UpdateWeapon();
            //_isInit = true;
        }

        public void UpdateWeapon() {
            if (_currentInf==null || _currentInf != Inventory.WeaponSlot.Item.Info) {
                if (Inventory.WeaponSlot.Item != null) {
                    _currentInf = Inventory.WeaponSlot.Item.Info;
                    _spriteRenderer.sprite =  Inventory.WeaponSlot.Item.Info.SpriteIcon;
                    // OnFiredChangedEnvent?.Invoke(Inventory.ClipSlot.Item?.Amount ?? 0, _c.Inventory.GetTotalAmmoByType
                    //     (_c.Inventory.ClipSlot.Item.Info.AmmoInfo.AmmoType));

                }

            }

            if (Inventory.ClipSlot.Item != null) {

                // OnFiredChangedEnvent?.Invoke(Inventory.ClipSlot.Item.Amount, _c.Inventory.GetTotalAmmoByType
                //     (_c.Inventory.ClipSlot.Item.Info.AmmoInfo.AmmoType));
            }
            OnFiredChangedEnvent?.Invoke(Inventory?.ClipSlot?.Item?.Amount ?? 0, _c.Inventory?.GetTotalAmmoByType
                (_c.Inventory.ClipSlot?.Item?.Info?.AmmoInfo?.AmmoType ??  ItemAmmoType.None) ?? 0);
        }

        public Transform GetMuzzleTransform() {
            return _muzzle.transform;
        }

        public void StartFire() {

            if (CheckReadyForAttack()) {
                TryFireWeapon();
            }
        }

        private bool CheckReadyForAttack() {

            if ((Inventory.WeaponSlot == null || Inventory.WeaponSlot.IsEmpty)) return false;
            if (Inventory.WeaponSlot.Item.Info.WeaponInfo == null) return false;
            if (_isCooldowning) return false;
            if (_isReloading) return false;
            if (Inventory.ClipSlot.Item.Amount <= 0) {
                if (!CheckAmmo()) {
                    return false;
                }
                else {
                    StartCoroutine(Reload());
                }
                
            }
            if (_c.TargetEnemy == null) return false;
            return true;
        }


        private IEnumerator Reload() {
            
            
            _c.Reload();
            _isReloading = true;
            yield return new WaitForSeconds(Inventory.WeaponSlot.Item.Info.WeaponInfo.ReloadTime);
            Inventory.ReloadClipSlot();
            //OutsideAmountAmmo = _c.Inventory.GetTotalAmmoByType(_c.Inventory.WeaponSlot.Item.Info.WeaponInfo.AmmoType);// - ClipAmountAmmo;
            OnFiredChangedEnvent?.Invoke(Inventory.ClipSlot.Item.Amount, _c.Inventory.GetTotalAmmoByType
                (_c.Inventory.ClipSlot.Item.Info.AmmoInfo.AmmoType));
            _isReloading = false;
        }

        private bool CheckAmmo() {
            if (_c.Inventory.GetTotalAmmoByType(_c.Inventory.WeaponSlot.Item.Info.WeaponInfo.AmmoType) > 0) {
                return true;
            }
            _c.NoAmmo();
            return false;
        }

        private void TryFireWeapon() {
            if (Inventory.ClipSlot.Item.Amount <= 0) {
                
                StartCoroutine(Reload());
                return;
            }
            var isFire = Inventory.ShootingAndRemoveAmmo(this);

            if (isFire) {
                StartCoroutine(Firing());
                OnFiredChangedEnvent?.Invoke(Inventory.ClipSlot.Item.Amount, _c.Inventory.GetTotalAmmoByType
                    (_c.Inventory.ClipSlot.Item.Info.AmmoInfo.AmmoType));

            }
        }

        private IEnumerator Firing() {

            Debug.Log("Fire Continue!");
            _c.GameController.CreateBullet(_c, GetMuzzleTransform(), Inventory.WeaponSlot.Item.Info, 1);
            _muzzle.OnFired();
            StartCoroutine(AttackCooldown());
            UpdateWeapon();
            yield break;
        }

        private IEnumerator AttackCooldown() {
            _isCooldowning = true;
            yield return new WaitForSeconds(Inventory.WeaponSlot.Item.Info.WeaponInfo.FireRate);
            _isCooldowning = false;
        }

        private void OnDestroy() {
            //_c.Inventory.OnAmmoChangedEvent -= UpdateWeapon;
            //_c.Inventory.OnOneItemAmmoRemovedEvent -= OnOneItemAmmoRemovedEvent;
        }

        public float TimeReloading() {
            return Inventory.WeaponSlot.Item.Info.WeaponInfo.ReloadTime;
        }
    }
}