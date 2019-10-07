using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.NonECS
{
    public class ShootingComponent : MonoBehaviour
    {
        [SerializeField]
        private int _interval = 3;
        private Transform _cannonPivot = default(Transform);

        private void Start()
        {
            _cannonPivot = transform;
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.F))
            {
                if (Time.frameCount % _interval == 0)
                {
                    Shoot();
                }
            }
        }

        private void Shoot()
        {
            var tempBullet = SimpleBulletPool.instance.GetGameObject();
            tempBullet.SetActive(true);
            tempBullet.transform.position = _cannonPivot.position;
        }
    }
}
