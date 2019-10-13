namespace SimpleSpace.NonECS
{
    public interface IDamageable
    {
        void Initialize();
        void ApplyDamage(float ammount);
        void Death();
        void InstaKill();
    }

}
