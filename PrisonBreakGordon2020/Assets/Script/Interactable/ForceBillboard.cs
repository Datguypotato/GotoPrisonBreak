using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBillboard : MonoBehaviour
{
    Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        Target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(2 * transform.position - Target.position);
    }
}
