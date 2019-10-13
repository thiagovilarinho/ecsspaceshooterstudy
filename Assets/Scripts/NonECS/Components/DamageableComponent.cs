using SimpleSpace.Core;
using SimpleSpace.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleSpace.NonECS
{
    public class DamageableComponent : MonoBehaviour, IDamageable
    { 
        public PawnData _pawnData = null;

        [SerializeField]
        private bool _isPlayer = false;

        private float _life = 0;

        public float Life { get { return _life; } }

        public PawnData Data
        {
            set { _pawnData = value; }

            get{ return _pawnData; }
        }

        private void OnEnable()
        {
            Initialize();
        }
        public void Initialize()
        {
            if(_pawnData == null)
            {
                return;
            }

            _life = _pawnData.Life;
        }
        public void ApplyDamage(float ammount)
        {
            _life -= ammount;

            if(_isPlayer)
            {
                GameManager.instance.SetHealth(_life);
            }

            if(_life <= 0)
            {
                Death();
            }
        }
        public void Death()
        {
            if(_isPlayer)
            {
                GameManager.instance.PlayerDead();
                Addressables.ReleaseInstance(gameObject);
            }
            else
            {
                GameManager.instance.SetScore(_pawnData.ScorePoint);
                PoolManager.instance.ReturnToPool(gameObject);
            }
        }

        public void InstaKill()
        {
            PoolManager.instance.ReturnToPool(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(ConstantValues.playerTag))
            {
                other.GetComponent<IDamageable>().ApplyDamage(_pawnData.DamageAmmount);
                InstaKill();
            }

            if (other.CompareTag(ConstantValues.enemyTag) && !_isPlayer)
            {
                other.GetComponent<IDamageable>().ApplyDamage(_pawnData.DamageAmmount);
                InstaKill();
            }
        }

        private void OnDisable()
        {
            Initialize();
        }
    }
}
