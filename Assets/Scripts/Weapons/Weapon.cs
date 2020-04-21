using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public bool isAttack { get; set; }
    public Vector3 Direction { get; set; }
    public int AttackPower { get; set; }
    public int Damage => Random.Range(1, AttackPower * 2);
    public float DelaySec { get; set; }

    private void Start() => StartCoroutine(AttackActive());

    IEnumerator AttackActive()
    {
        while (true)
        {
            if (isAttack)
                Attack();
            yield return new WaitForSeconds(DelaySec);
        }
    }

    protected abstract void Attack();
}