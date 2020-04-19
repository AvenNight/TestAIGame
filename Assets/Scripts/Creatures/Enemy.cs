using System;
using UnityEngine;

public class Enemy : Creature
{
    public float UpdateTime;// { get; set; }
    public float AggroDistance;// { get; set; }
    public float AttackRange;

    [SerializeField]
    private MeleeWeapon weapon;

    private ObjectsFinder playersFinder;

    void Start()
    {
        weapon.DelaySec = attackSpeed;
        weapon.AttackPower = attack;

        playersFinder = new ObjectsFinder(this.gameObject, "Player");
        foreach (var e in playersFinder.Objects)
            e.GetComponent<Creature>().DeathNotify += () => playersFinder.Objects.Remove(e);
        InvokeRepeating("UpdateEnemy", UpdateTime, UpdateTime);
    }

    void UpdateEnemy()
    {
        if (playersFinder.Objects.Count == 0)
        {
            agent.isStopped = true;
            weapon.isAttack = false;
            return;
        }
        var direction = playersFinder.Direction;
        AttackAI(direction);
        if (playersFinder.DistanceToNearest < AggroDistance)
        {
            agent.SetDestination(playersFinder.NearestObject.transform.position);
            agent.isStopped = false;
        }
        else
            agent.isStopped = true;
    }


    private void AttackAI(Vector3 direction)
    {
        var ray = new Ray(this.transform.position, direction);
        bool hitted = Physics.Raycast(ray, out RaycastHit hit, AttackRange);
        if (hitted && hit.collider.tag == "Player")
        {
            weapon.isAttack = true;
            weapon.AttackedCreature = hit.collider.GetComponent<Creature>();
            Debug.DrawRay(this.transform.position, direction, new Color(0, 1, 0, 0.1f));
        }
        else
        {
            weapon.isAttack = false;
            Debug.DrawRay(this.transform.position, direction, new Color(1, 0, 0, 0.3f));
        }
    }
}