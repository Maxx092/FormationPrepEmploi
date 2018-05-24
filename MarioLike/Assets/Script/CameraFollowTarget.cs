using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    private Transform myPlayerTransform;
    private Vector3 offSet;
    [SerializeField]
    private float minY;
    [SerializeField]
    private float maxY;

	void Start ()
    {
        myPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offSet = transform.position - myPlayerTransform.position;
    }

	void Update ()
    {
        transform.position = new Vector3(myPlayerTransform.position.x, Mathf.Clamp(myPlayerTransform.position.y, minY, maxY), myPlayerTransform.position.z) + offSet;
    }
}
