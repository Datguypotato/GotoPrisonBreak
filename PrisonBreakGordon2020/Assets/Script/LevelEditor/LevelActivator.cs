using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActivator : MonoBehaviour
{
    public Transform editorCameraTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<LevelEditorCameraControler>().editorTransform = editorCameraTransform;
        }
    }
}
