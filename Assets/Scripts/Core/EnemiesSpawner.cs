using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.Core
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField]
        private int _spawnInterval = 30;

        [SerializeField]
        private GameObject _enemy = default(GameObject);

        [SerializeField]
        private Transform[] _spawnPoints = default(Transform[]);

        // Update is called once per frame
        void Update()
        {
            if(Time.frameCount % _spawnInterval == 0)
            {
                var tempEnemy =  PoolManager.instance.TryPool(_enemy);
                tempEnemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
            }
        }
    }
}
