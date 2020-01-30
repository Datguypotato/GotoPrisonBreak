using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorCameraControler : MonoBehaviour
{
    public Transform editorTransform;
    Transform cameraTransform;

    [Tooltip("percentage speed of time.delta time")]
    public float lerpSpeed;
    public float lerpTime = 0;
    public bool startLerping = false;

    public Vector3 initialCameraPos;
    public Quaternion initialCameraRot;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera"))
        {
            cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
            Debug.Log("0");
        }
        initialCameraPos = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            startLerping = !startLerping;
            SwitchMode(startLerping);
        }

    }

    void SwitchMode(bool turnOnEditor)
    {
        // getting variables
        PlayerFollow playerCamera = FindObjectOfType<PlayerFollow>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // setting variables
        startLerping = turnOnEditor;
        lerpTime = 0;

        if (turnOnEditor)
        {
            initialCameraPos = cameraTransform.position;
            initialCameraRot = cameraTransform.rotation;
            StartCoroutine(MoveToPosition(editorTransform.position, editorTransform.rotation, 2));
        }
        else
            StartCoroutine(MoveToPosition(initialCameraPos, initialCameraRot, 2));
    }

    IEnumerator MoveToPosition(Vector3 endpos, Quaternion endRot, float time)
    {
        float elapsedTime = 0;
        
        while(elapsedTime < time)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, endpos, elapsedTime / time);
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, endRot, elapsedTime / time);
            elapsedTime += Time.deltaTime * lerpSpeed;
            yield return new WaitForEndOfFrame();
        }
    }
}
