using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform tf;
    [SerializeField] private Transform target;

    public Vector3 offset;

    public float speed;

    private void Start()
    {
        offset = target.position - tf.position;
    }

    private void LateUpdate()
    {
        tf.position = Vector3.Lerp(tf.position, target.position - offset, Time.deltaTime * speed);
    }
}
