public class MeleeWeapon : Weapon
{
    public Creature AttackedCreature { get; set; }

    protected override void Attack()
    {
        AttackedCreature.TakeDamage(this.Damage);
    }
}