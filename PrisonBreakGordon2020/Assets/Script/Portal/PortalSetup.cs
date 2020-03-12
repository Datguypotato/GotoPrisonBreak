using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSetup : MonoBehaviour
{
    public Camera portalCam;
    public Material camMat;

    // Start is called before the first frame update
    void Start()
    {
        if(portalCam.targetTexture != null)
        {
            portalCam.targetTexture.Release();
        }

        portalCam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        camMat.mainTexture = portalCam.targetTexture;
    }

}
