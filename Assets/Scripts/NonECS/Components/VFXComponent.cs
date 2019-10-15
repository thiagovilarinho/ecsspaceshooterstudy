using SimpleSpace.Core;
using UnityEngine;

namespace SimpleSpace.NonECS
{
    public class VFXComponent : MonoBehaviour
    {
        [SerializeField]
        private ParticleListener _particleListener = null;

        private void Start()
        {
            GetComponents();
        }

        private void OnEnable()
        {
            _particleListener.ParticleStopped += RecycleParticle;
        }

        [ContextMenu("Grab Components")]
        private void GetComponents()
        {
            if (_particleListener == null)
            {
                _particleListener = GetComponentInChildren<ParticleListener>();
            }
        }

        private void RecycleParticle()
        {
            PoolManager.instance.ReturnToPool(gameObject);
        }

        private void OnDisable()
        {
            _particleListener.ParticleStopped -= RecycleParticle;
        }
    }
}

