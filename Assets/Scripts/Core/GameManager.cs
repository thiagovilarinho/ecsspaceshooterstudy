using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleSpace.Core
{
    public class GameManager : Singleton<GameManager>
    {
        public AssetReference _spaceShip;

        public Action<string> UpdateScore;
        public Action<string> UpdateHealth;
        public Action PlayerIsDead;

        private int _score = 0;

        private GameObject _cacheShip;

        // Start is called before the first frame update
        void Start()
        {
            //_spaceShip.LoadAssetAsync<GameObject>().Completed += LoadAsset;
            _spaceShip.InstantiateAsync();
            //handle.
            //_cacheShip = handle.Result;
        }

        public void SetScore(int score)
        {
            _score += score;
            UpdateScore?.Invoke(_score.ToString());
        }

        public void SendHealth(float health)
        {
            UpdateHealth?.Invoke(health.ToString());
        }

        public void PlayerDead()
        {
            PlayerIsDead?.Invoke();
        }
    }
}

