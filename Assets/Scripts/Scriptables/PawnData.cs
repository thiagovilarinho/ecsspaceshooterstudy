using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.Data
{
    [CreateAssetMenu(fileName = "PawnData", menuName = "SimpleSpace/PawnData", order = 1)]
    public class PawnData : ShipData
    {
        public int ScorePoint;

        public override int GetScore()
        {
            return ScorePoint;
        }
    }
}
