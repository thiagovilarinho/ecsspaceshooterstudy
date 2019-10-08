using SimpleSpace.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleSpace.NonECS
{
    public class DamageableComponent : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private float _startLife = 150;

        [SerializeField]
        private int _myPoints = 50;

        [SerializeField]
        private float _damageAmmount = 15;

        [SerializeField]
        private bool _isPlayer = false;

        private float _life = 0;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _life = _startLife;
        }

        public void ApplyDamage(float ammount)
        {
            _life -= ammount;

            if(_isPlayer)
            {
                GameManager.instance.SendHealth(_life);
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
            }else
            {
                GameManager.instance.SetScore(_myPoints);
                PoolManager.instance.ReturnToPool(gameObject);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(ConstantValues.playerTag))
            {
                other.GetComponent<IDamageable>().ApplyDamage(_damageAmmount);
                Death();
            }

            if (other.CompareTag(ConstantValues.enemyTag) && !_isPlayer)
            {
                other.GetComponent<IDamageable>().ApplyDamage(_damageAmmount);
                Death();
            }

        }

        private void OnDisable()
        {
            Initialize();
        }

    }
}
