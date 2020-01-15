using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.Data
{
    [CreateAssetMenu(fileName = "PawnData", menuName = "SimpleSpace/PawnData", order = 1)]
    public class PawnData : ShipData
    {
        public int ScorePoint = 100;

        public float MyDamage = 50;

        public override int GetScore()
        {
            return ScorePoint;
        }

        public override float GetDamageAmmount()
        {
            if(Weapon == null)
            {
                return MyDamage;
            }

            return Weapon.GetDamageAmmount();
        }
    }
}
