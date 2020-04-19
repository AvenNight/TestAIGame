using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private List<GameObject> players;

    private float activeteDistance => 1f;
    public float distanseToPlayer => player.GetDistanceTo(this.gameObject);
    private bool allPlayersOnExit
    {
        get
        {
            var result = players.All(p => p.GetDistanceTo(this.gameObject) <= activeteDistance);
            return result;
        }
    }

    private bool levelComplite = false;
    public bool LevelComplite => levelComplite;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
        foreach (var e in players)
            e.GetComponent<Creature>().DeathNotify += () => players.Remove(e);
    }

    void Update()
    {
        if (allPlayersOnExit && !levelComplite)
        {
            levelComplite = true;
            Debug.Log("FINISH");
        }
    }
}
