using UnityEngine;

namespace SimpleSpace.NonECS
{
    public interface IDamageable
    {
        Transform GetTransform { get;}
        IPawnData Data { get; set; }
        int ID { get; set; }
        void Initialize();
        void ApplyDamage(float ammount,int hash = 0);
        void Death(bool countScore = false, int hash = 0);
    }

}
