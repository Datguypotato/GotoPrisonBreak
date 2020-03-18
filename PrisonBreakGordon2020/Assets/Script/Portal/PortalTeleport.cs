using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// teleport player from portal to portal
/// </summary>

public class PortalTeleport : MonoBehaviour
{
    public Transform playerTransform;
    public Transform playerCamTrans;

    public Transform teleportPoint;
    public Transform portalCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform.position = teleportPoint.position;
            playerCamTrans.position = portalCam.transform.position;
        }
    }
}
