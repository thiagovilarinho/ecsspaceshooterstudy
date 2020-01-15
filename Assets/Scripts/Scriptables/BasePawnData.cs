using SimpleSpace.NonECS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleSpace.Data
{
    [CreateAssetMenu(fileName = "Simple PawnData", menuName = "SimpleSpace/SimplePawnData", order = 0)]
    public class BasePawnData : ScriptableObject, IPawnData
    {
        public GameObject Pawn;
        public float Life = 150;

        protected readonly GameObject[] _objects = new GameObject[1];

        public virtual float GetDamageAmmount()
        {
           return 0;
        }

        public float GetLife()
        {
            return Life;
        }

        public virtual int GetScore()
        {
            return 0;
        }

        /// <summary>
        /// Return the pawn value.
        /// </summary>
        /// <param name="i">only 0 will be returned</param>
        /// <returns></returns>
        public GameObject GetObject(int i)
        {
            int length = _objects.Length;

            if (i > length)
            {
                return _objects[length];
            }

            if (i < length)
            {
                return _objects[0];
            }

            return _objects[i];
        }

        public void Initialize()
        {
           _objects[0] = Pawn;
        }
    }
}

