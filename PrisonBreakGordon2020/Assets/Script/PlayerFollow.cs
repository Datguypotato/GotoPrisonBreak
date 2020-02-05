using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    [Header("Player cam settings")]
    public Transform PlayerTransform;
    public GameObject player;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = true;
    public bool RotateMiddleMouseButton = true;

    public float RotationsSpeed = 5.0f;
    public float CameraPitchMin = 1.5f;

    public float CameraPitchMax = 6.5f;

    LevelEditorCameraControler controller;
    public float cameraSpeed = 5;


    [Header("Editor cam setting")]

    public Transform editorTransform;
    public Vector3 clampRadius;

    public bool isPlayerMode = true;
    public float editorCameraSpeed = 5f;
    public float lerpSpeed = 2;

    Vector3 lastPlayerPos;
    Quaternion lastPlayerRot;

    Vector2 boundx;
    Vector2 boundz;

    public GameObject testObject;
    // Use this for initialization
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
        controller = FindObjectOfType<LevelEditorCameraControler>();
    }

    private bool IsRotateActive
    {
        get
        {
            if (!RotateAroundPlayer)
                return false;

            if (!RotateMiddleMouseButton)
                return true;

            if (RotateMiddleMouseButton && Input.GetMouseButton(2))
                return true;

            return false;
        }
    }

    // LateUpdate is called after Update methods
    void LateUpdate()
    {
        if(isPlayerMode)
        {
            if (IsRotateActive)
            {

                float h = Input.GetAxis("Mouse X") * RotationsSpeed;
                float v = Input.GetAxis("Mouse Y") * RotationsSpeed;

                Quaternion camTurnAngle = Quaternion.AngleAxis(h, Vector3.up);

                Quaternion camTurnAngleY = Quaternion.AngleAxis(v, -transform.right);

                Vector3 newCameraOffset = camTurnAngle * camTurnAngleY * _cameraOffset;

                // Limit camera pitch
                if (newCameraOffset.y < CameraPitchMin || newCameraOffset.y > CameraPitchMax)
                {
                    newCameraOffset = camTurnAngle * _cameraOffset;
                }

                _cameraOffset = newCameraOffset;

            }

            Vector3 newPos = PlayerTransform.position + _cameraOffset;

            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

            if (LookAtPlayer || RotateAroundPlayer)
                transform.LookAt(PlayerTransform);
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    GameObject tempGhost = Instantiate(testObject);
                    tempGhost.transform.position = hit.point;
                }
            }
        }
    }

    public void SetupSwitchmode()
    {
        player.SetActive(isPlayerMode);

        if (isPlayerMode)
        {
            StartCoroutine(MoveToPosition(lastPlayerPos, lastPlayerRot));
        }
        else if(!isPlayerMode && editorTransform != null)
        {
            lastPlayerPos = transform.position;
            lastPlayerRot = transform.rotation;
            StartCoroutine(MoveToPosition(editorTransform.position, editorTransform.rotation));
        }
    }

    public void SetupNewLevel(Transform editTrans, Vector2 x, Vector2 z)
    {
        editorTransform = editTrans;
        boundx = x;
        boundz = z;
    }

    IEnumerator MoveToPosition(Vector3 endpos, Quaternion endRot)
    {
        float elapsedTime = 0;

        Vector3 startingPos = transform.position;
        Quaternion startingRot = transform.rotation;

        while (elapsedTime < 1)
        {
            transform.position = Vector3.Lerp(startingPos, endpos, elapsedTime);
            transform.rotation = Quaternion.Lerp(startingRot, endRot, elapsedTime);
            elapsedTime += Time.deltaTime * lerpSpeed;
            yield return new WaitForEndOfFrame();
        }
    }
}
