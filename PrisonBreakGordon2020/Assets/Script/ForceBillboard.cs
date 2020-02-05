using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBillboard : MonoBehaviour
{
    Transform playerTrans;

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(2 * transform.position - playerTrans.position);
    }
}
