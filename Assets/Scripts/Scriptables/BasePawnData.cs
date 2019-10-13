using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleSpace.Data
{
    [CreateAssetMenu(fileName = "Simple PawnData", menuName = "SimpleSpace/SimplePawnData", order = 0)]
    public class BasePawnData : ScriptableObject
    {
        public GameObject Pawn;
        public float DamageAmmount;
    }
}

