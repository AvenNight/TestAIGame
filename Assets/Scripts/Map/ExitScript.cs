using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private List<GameObject> players;

    private float activateDistance => 1f;
    public float distanceToPlayer => player.GetDistanceTo(this.gameObject);
    private bool allPlayersOnExit
    {
        get
        {
            var result = players.All(p => p.GetDistanceTo(this.gameObject) <= activateDistance);
            return result;
        }
    }

    private bool levelComplete = false;
    public bool LevelComplite => levelComplete;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
        foreach (var e in players)
            e.GetComponent<Creature>().DeathNotify += () => players.Remove(e);
    }

    void Update()
    {
        if (allPlayersOnExit && !levelComplete)
        {
            levelComplete = true;
            Debug.Log("FINISH");
        }
    }
}
