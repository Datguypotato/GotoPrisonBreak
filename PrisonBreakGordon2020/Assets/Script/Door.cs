using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : BaseInteractable, IInteractable
{
    public int id;
    public bool open = false;
    public bool locked;
    
    private float initialRotation;

    AudioSource doorEffect;
    public AudioClip doorOpen, doorWrong;

    protected override void Start()
    {
        base.Start();
        initialRotation = transform.rotation.eulerAngles.y;
        doorEffect = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!locked)
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
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            MalApi mal = FindObjectOfType<MalApi>();
            if (mal.currState == MalApi.ApiState.MyAnimeListUser)
                mal.Request();
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.O))
        {
            if (!locked)
            {
                if (Inventory.instance.HasKey(id))
                {
                    Action();
                }
                else
                {
                    doorEffect.PlayOneShot(doorWrong);
                    Debug.Log("Bro you don't have the key you need " + id + " key");
                }
            }
            
        }
    }

    public override void Action()
    {
        if (Input.GetKeyDown(KeyCode.O) && Inventory.instance.HasKey(id))
        {
            open = !open;
            doorEffect.PlayOneShot(doorOpen);
        }
    }
}
