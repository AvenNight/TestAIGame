using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsFinder
{
    private readonly GameObject thisObj;
    public readonly List<GameObject> Objects;

    public GameObject NearestObject => Objects
        .Aggregate((a, b) => thisObj.gameObject.GetDistanceTo(a) < thisObj.gameObject.GetDistanceTo(b) ? a : b);

    public Vector3 Direction => NearestObject.transform.position - thisObj.transform.position;

    public float DistanceToNearest => thisObj.gameObject.GetDistanceTo(NearestObject);

    public ObjectsFinder(GameObject from, string tag)
    {
        thisObj = from;
        Objects = GameObject.FindGameObjectsWithTag(tag).ToList();
    }
}