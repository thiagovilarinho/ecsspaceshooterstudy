using SimpleSpace.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.NonECS
{
    public class BoundaryKillerComponent : MonoBehaviour
    {

        [SerializeField]
        private float _limitMultiplier = 1;

        private Transform _transform = default(Transform);

        private ScreenBoundary _boundaries = new ScreenBoundary();
        // Start is called before the first frame update
        void Start()
        {
            _transform = transform;

            _boundaries.Initialize();

        }

        // Update is called once per frame
        void Update()
        {
            if (_boundaries.TouchBound(_transform.position.x * _limitMultiplier))
            {
                //gameObject.SetActive(false);
                PoolManager.instance.ReturnToPool(gameObject);
            }
        }
    }
}
