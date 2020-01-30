using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Key : MonoBehaviour
{
    [Header("hover properties")]
    public float rotateSpeed = 1;
    public float maxHeight = 0.1f;
    public float minHeight = -0.1f;
    public float hoverSpeed = 10.0f;

    float randomizeStartPoint;

    // accessItem properties
    AccessItem AccessItem;

    [Header("accessItem properties")]
    public int keyId;
    public string keyName;
    public float weight;
    public Sprite keySprite;

    Vector3 intialPos;

    private void Start()
    {
        AccessItem = new AccessItem(keyName, weight, keyId);
        intialPos = transform.position;
        randomizeStartPoint = Random.Range(0, 100);
    }

    private void Update()
    {
        // simple animation
        float hoverHeight = (maxHeight + minHeight) / 2.0f;
        float hoverRange = maxHeight - minHeight;

        this.transform.position = Vector3.up + new Vector3(0, hoverHeight + Mathf.Cos((randomizeStartPoint + Time.time) * hoverSpeed) * hoverRange, 0) + intialPos;
        transform.Rotate(Vector3.up * rotateSpeed, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.instance.AddItem(this.AccessItem);
            PlayerController pc = FindObjectOfType<PlayerController>();

            pc.UpdateUI(keySprite);

            //updating UI

            
            Destroy(this.gameObject);
        }
    }
}
