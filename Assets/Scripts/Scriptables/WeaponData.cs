using SimpleSpace.NonECS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.Data
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "SimpleSpace/WeaponData", order = 0)]
    public class WeaponData : ScriptableObject, IPawnData
    {
        public GameObject Cannon;

        public GameObject Bullet;

        public float damageAmmount = 100;

        public int ShootInterval = 15;

        private readonly GameObject[] _objects = new GameObject[2];

        public float GetDamageAmmount()
        {
            return damageAmmount;
        }

        public float GetLife()
        {
            return 0;
        }

        public int GetScore()
        {
           return 0;
        }

        /// <summary>
        /// Get the cannon or bullet
        /// </summary>
        /// <param name="i">0 for cannon or 1 for bullet </param>
        /// <returns></returns>
        public GameObject GetObject(int i)
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

        public void Initialize()
        {
           _objects[0] = Cannon;
            _objects[1] = Bullet;
        }


    }
}
