using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using SimpleSpace.NonECS;
using UnityEngine.SceneManagement;
using SimpleSpace.UI;
using SimpleSpace.Data;

namespace SimpleSpace.Core
{
    public enum GameStates { Idle, Running, Ended}

    public class GameManager : Singleton<GameManager>
    {
        public GameStates gameState;
        public AssetReference spaceShipData;

        public UIWindow gameOverWindow = null;

        public Action<string> UpdateScore;
        public Action<string> UpdateHealth;
        public Action PlayerIsDead;

        private int _score = 0;

        private GameObject _cacheShip;
        private PawnData _loadedData = null;

        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            
            spaceShipData.LoadAssetAsync<PawnData>().Completed += op =>
            {
                _loadedData = op.Result;
                SetHealth(_loadedData.Life);
            };
        }
        private void Start()
        {
            SetHealth(0);
            SetScore(0);
        }
        public void StartGame()
        {
            //_loadedData.Pawn.InstantiateAsync().Completed += op =>
            
            {
                var damageableData = Instantiate(_loadedData.Pawn).GetComponent<DamageableComponent>();
                damageableData.Data = _loadedData;
                damageableData.Initialize();
            };

            gameState = GameStates.Running;
        }

        public void SetScore(int score)
        {
            if(gameState.Equals(GameStates.Ended))
            {
                return;
            }

            _score += score;
            UpdateScore?.Invoke(_score.ToString());
        }

        public void SetHealth(float health)
        {
            if(health <= 0 && gameState.Equals(GameStates.Running))
            {
                PlayerDead();
                health = 0;
            }

            UpdateHealth?.Invoke(health.ToString());
        }

        public void PlayerDead()
        {
            gameState = GameStates.Ended;
            gameOverWindow?.Open();

            PlayerIsDead?.Invoke();
        }

        public void Reset()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            spaceShipData.ReleaseAsset();
        }
    }
}

