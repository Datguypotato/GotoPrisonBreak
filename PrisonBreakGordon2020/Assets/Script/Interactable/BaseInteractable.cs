using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour, IInteractable
{
    public Material OutlineMat;
    protected Material startMat;

    protected MeshRenderer rend;

    protected virtual void Start()
    {
        if(GetComponent<MeshRenderer>() != null)
        {
            rend = GetComponent<MeshRenderer>();
            
        }
        else
        {
            rend = GetComponentInChildren<MeshRenderer>();
        }

        startMat = rend.material;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && OutlineMat != null)
        {
            rend.material = OutlineMat;
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            Action();
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        rend.material = startMat;
    }

    public abstract void Action();
}
