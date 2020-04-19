using UnityEngine;

public class SceneCreator : MonoBehaviour
{
    [SerializeField]
    GameObject wall;
    [SerializeField]
    GameObject ground;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    GameObject exit;

    private float wallSize => 2.2f;
    private float groundLevel => -1f;
    private Vector3 startingPoint => new Vector3(-9.9f, 0, -9.9f);
    private GameObject environment;
    private GameObject maps;

    public void CreateScene(char[,] map, bool init = true)
    {
        environment = GameObject.FindGameObjectWithTag("Environment");
        maps = GameObject.FindGameObjectWithTag("MapObjects");
        var curLoc = startingPoint;
        for (int x = 0; x < map.GetLength(0); x++)
        {
            curLoc.z = startingPoint.z;
            for (int y = 0; y < map.GetLength(1); y++)
            {
                // Create Map Object
                CreateObject(GetMapObjectByType(map[map.GetLength(1) - 1 - y, x], curLoc));
                // Create Ground
                if (init)
                    CreateObject(new MapObjectData(ground, new Vector3(curLoc.x, curLoc.y + groundLevel, curLoc.z), environment));
                curLoc.z += wallSize;
            }
            curLoc.x += wallSize;
        }
    }

    public void Refresh(char[,] map)
    {
        int n = maps.transform.childCount;
        for (int i = 0; i < n; i++)
            Destroy(maps.transform.GetChild(i).gameObject);
        CreateScene(map, false);
    }

    private MapObjectData GetMapObjectByType(char type, Vector3 location)
    {
        switch (type)
        {
            case 'w': return new MapObjectData(wall, location, environment);
            case 'p': return new MapObjectData(player, location, maps);
            case 'e': return new MapObjectData(enemy, location, maps);
            case 'x': return new MapObjectData(exit, location, maps);
            default: return null;
        }
    }

    private void CreateObject(MapObjectData mapObj)
    {
        if (mapObj == null) return;
        var curWall = Instantiate(mapObj.Prefab, mapObj.Location, Quaternion.identity);
        curWall.transform.SetParent(mapObj.Parrent.transform, true);
    }
}