using SimpleSpace.Core;
using SimpleSpace.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleSpace.NonECS
{
    public class DamageableComponent : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private int _id = 0;

        [SerializeField]
        private bool _isPlayer = false;

        [SerializeField]
        private GameObject _deathFx = null;

        private IPawnData _pawnData = null;

        private float _life = 0;


        private IDamageable _myTarget = null;

        public float Life { get { return _life; } }

        public int ID { get { return _id; } set { _id = value; } }

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
        private void OnDisable()
        {
            ID = 0;
        }
        public void Initialize()
        {
            if(_pawnData == null)
            {
                return;
            }

            _life = _pawnData.GetLife();
        }

        public void ApplyDamage(float ammount,int hash = 0)
        {
            if(hash.Equals(ID))
            {
                return;
            }

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

        public void Death(bool countScore = false, int hash = -1)
        {
            if(hash.Equals(ID))
            {
                return;
            }

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

        private void GetTargetFromCollision(Collider target)
        {
            _myTarget = target.GetComponent<IDamageable>();
            _myTarget.ApplyDamage(_pawnData.GetDamageAmmount(), ID);
            Death(false, _myTarget.ID);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(ConstantValues.playerTag))
            {
                GetTargetFromCollision(other);
            }

            if (other.CompareTag(ConstantValues.enemyTag) && !_isPlayer)
            {
                GetTargetFromCollision(other);
            }
        }


    }
}
