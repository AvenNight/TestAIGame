using UnityEngine;

public class RangeWeapon : Weapon
{
    public GameObject Bullet { get; set; }
    protected override void Attack()
    {
        var b = Instantiate(Bullet, this.gameObject.transform.position + Direction.normalized * 0.7f, Quaternion.Euler(Direction));
        b.GetComponent<Bullet>().Shoot(Direction, Damage);
    }
}
