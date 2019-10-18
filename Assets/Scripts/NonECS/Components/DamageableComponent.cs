using SimpleSpace.Core;
using SimpleSpace.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleSpace.NonECS
{
    public class DamageableComponent : MonoBehaviour, IDamageable
    { 
        [SerializeField]
        private bool _isPlayer = false;

        [SerializeField]
        private GameObject _deathFx = null;

        private IPawnData _pawnData = null;

        private float _life = 0;

        public float Life { get { return _life; } }

        public IPawnData Data
        {
            set { _pawnData = value; }

            get{ return _pawnData; }
        }

        public Transform GetTransform
        {
            get {return transform;}
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

            _life = _pawnData.GetLife();
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
                Death(true);
            }
        }

        public void Death(bool countScore = false)
        {
            if(_deathFx)
            {
                PoolManager.instance.TryPool(_deathFx).
                                    transform.position = GetTransform.position;
            }

            if(_isPlayer)
            {
                GameManager.instance.PlayerDead();
                Addressables.ReleaseInstance(gameObject);
            }
            else
            {
                if(countScore)
                {
                    GameManager.instance.SetScore(_pawnData.GetScore());
                }

                PoolManager.instance.ReturnToPool(gameObject);
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(ConstantValues.playerTag))
            {
                other.GetComponent<IDamageable>().ApplyDamage(_pawnData.GetDamageAmmount());
                Death();
            }

            if (other.CompareTag(ConstantValues.enemyTag) && !_isPlayer)
            {
                other.GetComponent<IDamageable>().ApplyDamage(_pawnData.GetDamageAmmount());
                Death();
            }
        }


    }
}
