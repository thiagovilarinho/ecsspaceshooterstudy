﻿using UnityEngine;
using SimpleSpace.Core;
using SimpleSpace.Data;

namespace SimpleSpace.NonECS
{
    public class ShootingComponent : MonoBehaviour
    {
        private WeaponData _bulletData = null;

        private Transform _cannonPivot = default(Transform);

        private GameObject _bullet = null;

        private bool _initialized = false;

        public WeaponData Data
        {
            get { return _bulletData; }
            set { _bulletData = value; }
        }

        public void Initialize()
        {
            _cannonPivot = transform;

            if (_bulletData.Cannon)
            {
                var tempcannon = Instantiate(_bulletData.Cannon);
                tempcannon.transform.position = _cannonPivot.position;
                tempcannon.transform.SetParent(_cannonPivot);
            }

            _bullet = _bulletData.Bullet;

            _initialized = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(!_initialized || !GameManager.instance.gameState.Equals(GameStates.Running))
            {
                return;
            }

            if (Input.GetKey(KeyCode.F) || Input.GetMouseButton(0))
            {
                if (Time.frameCount % _bulletData.ShootInterval == 0)
                {
                    Shoot();
                }
            }
        }

        private void Shoot()
        {
            var tempBullet = PoolManager.instance.TryGetPool<IDamageable>(_bullet);
            tempBullet.GetTransform.position = _cannonPivot.position;
            tempBullet.Data = _bulletData;
        }
    }
}
