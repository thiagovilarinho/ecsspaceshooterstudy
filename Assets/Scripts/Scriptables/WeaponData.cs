using SimpleSpace.NonECS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.Data
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "SimpleSpace/WeaponData", order = 0)]
    public class WeaponData : SimpleWeaponData
    {
        public GameObject Cannon;

        public GameObject Bullet;

        public int ShootInterval = 15;

        private readonly GameObject[] _objects = new GameObject[2];


        /// <summary>
        /// Get the cannon or bullet
        /// </summary>
        /// <param name="i">0 for cannon or 1 for bullet </param>
        /// <returns></returns>
        public override GameObject GetObject(int i)
        {
            int length = _objects.Length;

            if(i > length)
            {
                return _objects[length];
            }

            if(i< length)
            {
                return _objects[0];
            }

            return _objects[i];
        }

        public override void Initialize()
        {
           _objects[0] = Cannon;
            _objects[1] = Bullet;
        }


    }
}
