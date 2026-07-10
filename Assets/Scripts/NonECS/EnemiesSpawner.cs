using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using SimpleSpace.Core;
using SimpleSpace.Data;
using Unity.Burst;

namespace SimpleSpace.NonECS
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField]
        private float _spawnInterval = 2f;

        [SerializeField]
        private string _enemieLabel = "Meteors";

        [SerializeField]
        private Transform[] _spawnPoints = default(Transform[]);

        private bool _dataLoaded = false;

        private List<PawnData> _enemiesData = new List<PawnData>();

        private int _randomResult = 0;

        private float _spawnTimer = 0f;

        private int _lastSpawner = 0;

        private void Awake()
        {
            Addressables.LoadAssetsAsync<PawnData>(_enemieLabel, op =>
             {
                 _enemiesData.Add(op);
                 _dataLoaded = op;
             });
        }

        void Update()
        {
            if (GameManager.instance.gameState == GameStates.Ended || !_dataLoaded || _enemiesData.Count == 0)
            {
                return;
            }

            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= _spawnInterval)
            {
                _spawnTimer = 0f;

                Spawn();
            }
        }

        [BurstCompile]
        void Spawn()
        {
                var tempEnemy =  PoolManager.instance.TryGetPool<IDamageable>(_enemiesData[_randomResult].Pawn);
                _lastSpawner++;
               /* while (_lastSpawner == _currentSpawner)
                {
                    _currentSpawner = Random.Range(0, _spawnPoints.Length);
                    
                }*/
               
                if (_lastSpawner > _spawnPoints.Length) _lastSpawner = 0;

                tempEnemy.GetTransform.position = _spawnPoints[_lastSpawner%_spawnPoints.Length].position;
                tempEnemy.GetTransform.localScale = tempEnemy.GetTransform.localScale * Random.Range(0.95f,1.12f);
                tempEnemy.Data = _enemiesData[_randomResult];

                _randomResult = Random.Range(0, _enemiesData.Count);

                tempEnemy.Initialize();
        }
    }
    
}
