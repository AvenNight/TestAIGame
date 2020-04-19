using UnityEngine;

public class PlayerController : Creature
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject exit;
    [SerializeField]
    private RangeWeapon rifle;
    [SerializeField]
    private float firingRange;
    private float fearRange => firingRange * 0.5f;

    private WayStatus wayStatus;
    private Vector3 startPoint; //=> new Vector3(-9.9f, 0, -9.9f);
    private Vector3 endPoint; //=> new Vector3(9.9f, 0, 9.9f);

    private ObjectsFinder enemyFinder;

    private void Start()
    {
        startPoint = this.gameObject.transform.position;
        endPoint = exit.transform.position;
        agent.SetDestination(endPoint);

        rifle.DelaySec = attackSpeed;
        rifle.AttackPower = attack;
        rifle.Bullet = bullet;

        enemyFinder = new ObjectsFinder(this.gameObject, "Enemy");
        foreach (var e in enemyFinder.Objects)
            e.GetComponent<Creature>().DeathNotify += () => enemyFinder.Objects.Remove(e);
    }

    private void FixedUpdate()
    {
        SelectorAI();
    }

    private void SelectorAI()
    {
        if (enemyFinder.Objects.Count == 0)
        {
            agent.SetDestination(endPoint);
            rifle.isAttack = false;
            return;
        }
        var direction = enemyFinder.Direction;
        ShootingAI(direction);
        if (!IsAIStatusChanged(direction))
            return;
        wayStatus = GetStatus(direction);
        switch (wayStatus)
        {
            case WayStatus.Walk:
                agent.SetDestination(endPoint);
                break;
            case WayStatus.Fear:
                agent.SetDestination(startPoint);
                break;
        }
    }

    private bool IsAIStatusChanged(Vector3 direction) => wayStatus != GetStatus(direction);

    private WayStatus GetStatus(Vector3 direction) =>
        direction.magnitude > fearRange ? WayStatus.Walk : WayStatus.Fear;

    private void ShootingAI(Vector3 direction)
    {
        var ray = new Ray(this.transform.position, direction);
        bool hitted = Physics.Raycast(ray, out RaycastHit hit, firingRange);
        if (hitted && hit.collider.tag == "Enemy")
        {
            rifle.Direction = direction;
            rifle.isAttack = true;
            Debug.DrawRay(this.transform.position, direction, new Color(0, 1, 0, 0.1f));
        }
        else
        {
            rifle.isAttack = false;
            Debug.DrawRay(this.transform.position, direction, new Color(1, 0, 0, 0.3f));
        }
    }
}