using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{

    public Transform target;

    Vector3 posOffset;


    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = target.rotation;
        transform.position = target.position + posOffset;
    }
}
