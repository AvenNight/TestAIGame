using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Creature : MonoBehaviour, IDamaged
{
    [SerializeField]
    protected NavMeshAgent agent;
    [SerializeField]
    protected int attack;
    public int Attack => attack;
    [SerializeField]
    protected int defence;
    public int Defence => defence;
    [SerializeField]
    protected float speed;
    public float Speed => speed;
    [SerializeField]
    protected float attackSpeed;
    public float AttackSpeed => attackSpeed;
    [SerializeField]
    protected int maxHp;
    public int MaxHp => maxHp;

    public event Action DeathNotify;

    [SerializeField]
    protected int hp;
    public int Hp
    {
        get => hp;
        set
        {
            hp = value > maxHp ? maxHp : value;
        }
    }

    private void Awake()
    {
        Hp = maxHp;
        agent.speed = speed;
    }

    public void TakeDamage(int damage)
    {
        var inputDamage = damage;
        var reducePercent = (0.05 * Defence) / (1 + 0.05 * Defence);
        damage = (int)Math.Round(damage - damage * reducePercent, 0);
        Hp -= damage;
        Debug.Log($"{this.tag} taken {damage} damage (reduced {inputDamage - damage})");
        if (Hp <= 0) Destroy(this.gameObject, 0.1f);
    }

    private void OnDestroy() => DeathNotify?.Invoke();
}
