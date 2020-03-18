using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform portalTarget;
    public Transform camTarget;

    public Transform portal;

    Vector3 posOffset;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = camTarget.position - portalTarget.position;
        transform.position = portal.position + pos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camTarget.rotation;
        transform.position = (camTarget.position - portalTarget.position) + portal.position;
    }
}
