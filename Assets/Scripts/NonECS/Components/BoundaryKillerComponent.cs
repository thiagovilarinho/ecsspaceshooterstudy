using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.NonECS
{
    public class BoundaryKillerComponent : MonoBehaviour
    {
        private Transform _transform = default(Transform);

        private float _screenBound = 0;

        private ScreenBoundary _boundaries = new ScreenBoundary();
        // Start is called before the first frame update
        void Start()
        {
            _transform = transform;

            _boundaries.Initialize();

            _screenBound = _boundaries.GetBoundaries();
        }

        // Update is called once per frame
        void Update()
        {
            if (_boundaries.TouchBound(_transform.position.x))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
