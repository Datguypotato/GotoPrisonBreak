using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
using UnityEngine;
using SimpleJSON;

public class MALEnterUsername : BaseJSONRequest
{
    public GameObject inputObject;
    public TMP_InputField inputUsername;

    public Sprite redX;
    public Image marker;

    MalApi mal;
    ThirdPersonUserControl character;

    private void Start()
    {
        mal = FindObjectOfType<MalApi>();

        inputObject.gameObject.SetActive(false);
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

    public void SetupLogin()
    {
        inputObject.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        character = FindObjectOfType<ThirdPersonUserControl>();
        StartCoroutine(StopPlayerMovement());
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
            inputObject.gameObject.SetActive(false);
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
