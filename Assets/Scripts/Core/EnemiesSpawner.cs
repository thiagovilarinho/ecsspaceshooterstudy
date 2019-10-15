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
        private string _enemieLabel = "Meteors";

        [SerializeField]
        private Transform[] _spawnPoints = default(Transform[]);

        private bool _dataLoaded = false;

        private List<PawnData> _enemiesData = new List<PawnData>();

        private int _randomResult = 0;

        private void Awake()
        {
            Addressables.LoadAssetsAsync<PawnData>(_enemieLabel, op =>
             {
                 _enemiesData.Add(op);
                 _dataLoaded = op;
             });

            //_enemyData.LoadAssetAsync<PawnData>().Completed += op =>
            //{
            //    _loadedData = op.Result;
            //    _dataLoaded = op.IsDone;
            //};

        }
        void Update()
        {
            if(GameManager.instance.gameState == GameStates.Ended && !_dataLoaded)
            {
                return;
            }

            if(Time.frameCount % _spawnInterval == 0)
            {
                var tempEnemy =  PoolManager.instance.TryGetPool<DamageableComponent>(_enemiesData[_randomResult].Pawn);
                tempEnemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
                tempEnemy.Data = _enemiesData[_randomResult];

                _randomResult = Random.Range(0, _enemiesData.Count);

                tempEnemy.Initialize();
            }
        }
    }
}
