using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode interactButton;
    public KeyCode openInventory;

    public bool disableOnStart = true;

    public Canvas canvas;
    public Image[] imageSlots;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (disableOnStart)
        {
            canvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
            
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
    }

    public void UpdateUI(Sprite sourceImage)
    {
        for (int i = 0; i < imageSlots.Length; i++)
        {
            if(imageSlots[i].sprite == null)
            {
                imageSlots[i].sprite = sourceImage;
                return;
            }
        }

        Debug.Log("iventory is full");
    }
}
