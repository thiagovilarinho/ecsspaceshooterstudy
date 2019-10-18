using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.NonECS
{
    public interface IPawnData
    {
        void Initialize();

        /// <summary>
        /// Get the cannon or bullet or the pawn object
        /// </summary>
        /// <param name="index">0 for cannon or pawn and 1 for bullet</param>
        /// <returns></returns>
        GameObject GetObject(int index);

        float GetDamageAmmount();
        float GetLife();
        int GetScore();
    }
}
