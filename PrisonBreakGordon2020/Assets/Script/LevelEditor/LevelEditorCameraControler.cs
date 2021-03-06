﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this was for testing purposes
/// </summary>
public class LevelEditorCameraControler : MonoBehaviour
{
    public Transform editorTransform;
    Transform activeCameraTransform;

    [Tooltip("percentage speed of time.delta time")]
    public float lerpSpeed;
    public bool startLerping = false;

    public Vector3 initialCameraPos;
    public Quaternion initialCameraRot;

    bool editActive;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera"))
        {
            activeCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
        initialCameraPos = activeCameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && editorTransform != null)
        {
            editActive = !editActive;
            SwitchMode(editActive);
        }
    }

    void SwitchMode(bool turnOnEditor)
    {
        // getting variables
        //PlayerFollow playerCamera = FindObjectOfType<PlayerFollow>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (turnOnEditor)
        {
            initialCameraPos = activeCameraTransform.position;
            initialCameraRot = activeCameraTransform.rotation;
            StartCoroutine(MoveToPosition(editorTransform.position, editorTransform.rotation, 2));
            //playerCamera.enabled = false;
        }
        else
        {
            StartCoroutine(MoveToPosition(initialCameraPos, initialCameraRot, 2));
            //playerCamera.enabled = true;
        }
    }

    IEnumerator MoveToPosition(Vector3 endpos, Quaternion endRot, float time)
    {
        float elapsedTime = 0;
        
        while(elapsedTime < time)
        {
            activeCameraTransform.position = Vector3.Lerp(activeCameraTransform.position, endpos, elapsedTime / time);
            activeCameraTransform.rotation = Quaternion.Lerp(activeCameraTransform.rotation, endRot, elapsedTime / time);
            elapsedTime += Time.deltaTime * lerpSpeed;
            yield return new WaitForEndOfFrame();
        }
    }
}
