using UnityEngine;
using UnityEngine.AI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    SceneCreator sceneCreator;

    [SerializeField]
    NavMeshSurface navMesh;

    void Start()
    {
        sceneCreator.CreateScene(Levels.Level2);
        navMesh.BuildNavMesh();
        //Time.timeScale = 2f;
    }

    public void Refresh()
    {
        sceneCreator.Refresh(Levels.Level2);
    }

    void Update()
    {

    }
}