using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;
using SimpleJSON;

public class MALEnterUsername : BaseJSONRequest
{
    public TMP_InputField inputUsername;
    public TMP_Text worldspaceUsername;

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
        if (mal.AssignUsername(inputUsername.text))
        {
            // check if user exist

            // activate userMovement
            character.enabled = true;

            // close UI
            inputUsername.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            Debug.Log(mal.AssignUsername(inputUsername.text));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // update inputfield to worldspace 
            inputUsername.gameObject.SetActive(true);
            inputUsername.text = worldspaceUsername.text;

            // open UI
            inputUsername.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

            character = other.gameObject.GetComponent<ThirdPersonUserControl>();
            StartCoroutine(StopPlayerMovement());
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
        throw new System.NotImplementedException();
    }
}
