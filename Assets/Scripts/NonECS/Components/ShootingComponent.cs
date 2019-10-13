using SimpleSpace.Core;
using SimpleSpace.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleSpace.NonECS
{
    public class ShootingComponent : MonoBehaviour
    {
        [SerializeField]
        private int _interval = 3;

        [SerializeField]
        private AssetReference _bulletData = null;

        private Transform _cannonPivot = default(Transform);

        private GameObject _bullet = null;

        private bool _assetLoaded = false;

        private PawnData _bData;

        private void Awake()
        {
            _cannonPivot = transform;

            _bulletData.LoadAssetAsync<BasePawnData>().Completed += op =>
            {
                var tempData = op.Result;
                _bData = ScriptableObject.CreateInstance<PawnData>();
                _bData.Pawn = tempData.Pawn;
                _bData.DamageAmmount = tempData.DamageAmmount;

                _bullet = _bData.Pawn;
                _assetLoaded = op.IsDone;
            };
        }

        // Update is called once per frame
        void Update()
        {
            if(!_assetLoaded)
            {
                return;
            }

            if (Input.GetKey(KeyCode.F) || Input.GetMouseButton(0))
            {
                if (Time.frameCount % _interval == 0)
                {
                    Shoot();
                }
            }
        }
        private void Shoot()
        {
            if(!GameManager.instance.gameState.Equals(GameStates.Running))
            {
                return;
            }

            var tempBullet = PoolManager.instance.TryGetPool<DamageableComponent>(_bullet);
            //tempBullet.SetActive(true);
            tempBullet.transform.position = _cannonPivot.position;
            tempBullet.Data = _bData;
        }

        private void OnDestroy()
        {
            _bulletData.ReleaseAsset();
        }
    }
}
