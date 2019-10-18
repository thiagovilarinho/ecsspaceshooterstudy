using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.Data
{
    [CreateAssetMenu(fileName = "ShipData", menuName = "SimpleSpace/ShipData", order = 1)]
    public class ShipData : BasePawnData
    {
        public WeaponData Weapon;
    }
}
