using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public bool isAttack { get; set; }
    public Vector3 Direction { get; set; }
    public int AttackPower { get; set; }
    public int Damage => Random.Range(1, AttackPower * 2);
    public float DelaySec { get; set; }
    private bool coroutineActive;
    private Coroutine coroutine;

    private void FixedUpdate()
    {
        if (isAttack && !coroutineActive)
        {
            coroutineActive = true;
            coroutine = StartCoroutine(AttackActive());
        }
        else if (!isAttack && coroutineActive)
        {
            coroutineActive = false;
            StopCoroutine(coroutine);
        }
    }

    IEnumerator AttackActive()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(DelaySec);
        }
    }

    protected abstract void Attack();
}
