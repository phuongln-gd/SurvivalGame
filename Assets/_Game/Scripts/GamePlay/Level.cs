using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    [SerializeField] Transform minPoint, maxPoint;

    public Vector2 RandomPoint()
    {
        Vector2 randPoint = Random.Range(minPoint.position.x, maxPoint.position.x) * Vector2.right + Random.Range(minPoint.position.y, maxPoint.position.y) * Vector2.up;

        return randPoint;
    }

}
