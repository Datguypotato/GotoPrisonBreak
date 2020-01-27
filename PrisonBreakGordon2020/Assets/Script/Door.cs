using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int id;
    public bool open = false;
    
    private float initialRotation;

    AudioSource doorEffect;
    public AudioClip doorOpen, doorWrong;

    private void Start()
    {
        initialRotation = transform.rotation.eulerAngles.y;
        doorEffect = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (open && transform.rotation.eulerAngles.y < initialRotation + 80)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, initialRotation + 80, 0), 5);
        }
        else if (!open && transform.rotation.eulerAngles.y > initialRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, initialRotation, 0), 5);
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.O))
        {
            if (Inventory.instance.HasKey(id))
            {
                open = !open;
                doorEffect.PlayOneShot(doorOpen);
            }
            else
            {
                doorEffect.PlayOneShot(doorWrong);
                Debug.Log("Bro you don't have the key you need " + id + " key");
            }
            
        }
    }
}
