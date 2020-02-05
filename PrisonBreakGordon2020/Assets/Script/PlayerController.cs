using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode interactButton;
    public KeyCode openInventory;
    public KeyCode SwitchPlayerMode;

    public bool disableOnStart = true;

    public Canvas canvas;

    PlayerFollow cameraController;

    void Start()
    {
        if (disableOnStart)
        {
            canvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        cameraController = FindObjectOfType<PlayerFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(openInventory))
        {
            if (canvas.enabled)
            {
                canvas.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                canvas.enabled = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if (Input.GetKeyDown(SwitchPlayerMode) && cameraController.editorTransform != null)
        {
            cameraController.isPlayerMode = !cameraController.isPlayerMode;
            cameraController.SetupSwitchmode();

            if(!cameraController.isPlayerMode)
                Cursor.lockState = CursorLockMode.None;
        }
    }
}
