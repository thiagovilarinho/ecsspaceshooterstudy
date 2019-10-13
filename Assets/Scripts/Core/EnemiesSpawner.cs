using SimpleSpace.Data;
using SimpleSpace.NonECS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleSpace.Core
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField]
        private int _spawnInterval = 30;

        [SerializeField]
        private AssetReference _enemyData = null;

        [SerializeField]
        private Transform[] _spawnPoints = default(Transform[]);

        private PawnData _loadedData = null;

        private bool _dataLoaded = false;



        private void Awake()
        {
            _enemyData.LoadAssetAsync<PawnData>().Completed += op =>
            {
                _loadedData = op.Result;

                _dataLoaded = op.IsDone;
                
            };

        }
        void Update()
        {
            if(GameManager.instance.gameState == GameStates.Ended && !_dataLoaded)
            {
                return;
            }

            if(Time.frameCount % _spawnInterval == 0)
            {
                var tempEnemy =  PoolManager.instance.TryGetPool<DamageableComponent>(_loadedData.Pawn);
                tempEnemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
                tempEnemy.Data = _loadedData;
                tempEnemy.Initialize();
            }
        }

        private void OnDestroy()
        {
            _enemyData.ReleaseAsset();
        }
    }
}
