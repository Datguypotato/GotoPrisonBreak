﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
using UnityEngine;
using SimpleJSON;

public class MALEnterUsername : BaseJSONRequest
{
    public TMP_InputField inputUsername;

    public Sprite redX;
    public Image marker;

    MalApi mal;
    ThirdPersonUserControl character;

    private void Start()
    {
        mal = FindObjectOfType<MalApi>();

        inputUsername.gameObject.SetActive(false);
    }

    private void Update()
    {
        //if (inputUsername.isFocused)
        //{
        //    Debug.Log("focussed");
        //    // assign MalApi username
        //    if (Input.GetKeyDown(KeyCode.Return))
        //    {
        //        Debug.Log("0");
        //        if (mal.AssignUsername(inputUsername.text))
        //        {
        //            // activate userMovement
        //            character.enabled = true;

        //            // close UI
        //            inputUsername.gameObject.SetActive(false);
        //            Cursor.lockState = CursorLockMode.None;
        //        }

        //    }

        //    // update UI with world ui
        //    worldspaceUsername.text = inputUsername.text;
        //}
    }

    public void Enter()
    {
        // check if user exist
        Debug.Log(inputUsername.text);
        StartCoroutine(RequestJson("https://api.jikan.moe/v3/user/" + inputUsername.text));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.O) && !inputUsername.gameObject.activeSelf)
            {
                // update inputfield to worldspace 
                inputUsername.gameObject.SetActive(true);

                // open UI
                inputUsername.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;

                character = other.gameObject.GetComponent<ThirdPersonUserControl>();
                StartCoroutine(StopPlayerMovement());
            }
        }
    }

    IEnumerator StopPlayerMovement()
    {
        while(character.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            character.StopMovement();
            character.enabled = false;
            yield return new WaitForEndOfFrame();
        }
    }

    protected override void ShowData(JSONNode node)
    {
        // show if user exist
        if(node["error"] == null)
        {
            // activate userMovement
            character.enabled = true;

            // UI
            inputUsername.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            Debug.Log(node["username"]);
            mal.AssignUsername(node["username"]);
        }
        else
        {
            marker.sprite = redX;
            Debug.Log("LMAO dude doesn't exist you dip");
        }
    }
}
