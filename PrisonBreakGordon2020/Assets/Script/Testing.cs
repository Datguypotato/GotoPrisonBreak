using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Item ball = new BonusItem("Ball", 10, 5);
        Inventory.instance.AddItem(ball);

        Debug.Log("You have " + Inventory.instance.Count() + " item in your inventory");

        //Inventory.instance.RemoveItem(ball);

        Debug.Log("You have " + Inventory.instance.Count() + " item in your inventory");

        Item superAwesomeKeyOfAwesomeness = new AccessItem("superAwesomeKeyOfAwesomeness", 1, 0);

        Inventory.instance.AddItem(superAwesomeKeyOfAwesomeness);

        Debug.Log("You have " + Inventory.instance.Count() + " item in your inventory");
    }
}
