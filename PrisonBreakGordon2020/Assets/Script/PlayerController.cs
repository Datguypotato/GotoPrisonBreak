using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode interactButton;
    public KeyCode openInventory;

    public GameObject hudHolder;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(openInventory))
        {
            if (hudHolder.activeSelf)
            {
                hudHolder.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                FindObjectOfType<PlayerFollow>().enabled = true;
            }
            else
            {
                hudHolder.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                FindObjectOfType<PlayerFollow>().enabled = false;
            }
        }
    }
}
