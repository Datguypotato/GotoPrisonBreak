using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform playerTransform;
    public Transform playerCamTrans;

    public Transform otherPortal;
    public Transform portalCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform.position = otherPortal.position;
            playerCamTrans.position = portalCam.transform.position;
        }
    }
}
