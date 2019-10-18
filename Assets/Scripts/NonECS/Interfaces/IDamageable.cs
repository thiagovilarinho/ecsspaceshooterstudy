using UnityEngine;

namespace SimpleSpace.NonECS
{
    public interface IDamageable
    {
        Transform GetTransform { get;}
        IPawnData Data { get; set; }
        void Initialize();
        void ApplyDamage(float ammount);
        void Death(bool countScore = false);
    }

}
