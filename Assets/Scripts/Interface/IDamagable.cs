namespace Interface
{
    public interface IDamagable
    {
        bool Damage(int damage);
        void Heal(int heal);
    }
}