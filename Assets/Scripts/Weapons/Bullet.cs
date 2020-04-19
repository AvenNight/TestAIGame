using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float speed;
    private int damage;

    public void Shoot(Vector3 direction, int damage)
    {
        this.GetComponent<Rigidbody>().velocity = direction.normalized * speed;
        this.damage = damage;
        Destroy(this.gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Enemy")
        {
            var enemy = other.GetComponent<IDamaged>();
            enemy.TakeDamage(damage);
            //Debug.Log("Enemy damaged TR");
        }
        Destroy(this.gameObject);
    }
}
