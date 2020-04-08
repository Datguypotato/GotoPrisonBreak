using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public GameObject tooltip;

    //Transform playerTransform;
    //ThirdPersonUserControl userControl;

    private void Start()
    {
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //userControl = FindObjectOfType<ThirdPersonUserControl>();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tooltip.SetActive(true);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        tooltip.transform.position = playerTransform.position + userControl.GetForward() + Vector3.right + Vector3.up;
    //    }
    //}

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tooltip.SetActive(false);
        }
    }
}