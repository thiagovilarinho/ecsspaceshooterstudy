using SimpleSpace.NonECS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.Data
{
    [CreateAssetMenu(fileName = "SimpleWeaponData", menuName = "SimpleSpace/SimpleWeaponData", order = 0)]
    public class SimpleWeaponData : ScriptableObject, IPawnData
    {
        public float DamageAmmount = 100;

        public float GetDamageAmmount()
        {
           return DamageAmmount;
        }

        public float GetLife()
        {
           return 0;
        }

        public virtual GameObject GetObject(int index)
        {
            return null;
        }

        public int GetScore()
        {
            return 0;
        }

        public virtual void Initialize()
        { }
    }

}
