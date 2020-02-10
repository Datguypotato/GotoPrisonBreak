using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Testing : MonoBehaviour
{

    public string jsonFileName = "Gordon.json";

    // Start is called before the first frame update
    void Start()
    {
        string jsonStr = ReadLocalJSONFile(jsonFileName);
        JSONNode jsonObject = JsonParse(jsonStr);

        Debug.Log(jsonObject.Keys);
        for (int i = 0; i < jsonObject.Count; i++)
        {
            Debug.Log(jsonObject[i]);
        }

        //ArrayList veryCoolNumbers = new ArrayList();

        //for (int i = 0; i < 10; i++)
        //{
        //    veryCoolNumbers.Add(i);
        //    veryCoolNumbers.Add("Test");
        //}

        //Debug.Log("veryCoolNumbers has " + veryCoolNumbers.Count + " items in it");

        //for (int i = 0; i < veryCoolNumbers.Count; i++)
        //{
        //    Debug.Log(veryCoolNumbers[i].ToString());
        //}

        //int[] head = new int[] { 0, 2, 2, 5, 10, 6 };
        //HashSet<int> headSets = new HashSet<int>(head);

        //Debug.Log("headSets has " + headSets.Count + " items in it");

        //foreach (int item in headSets)
        //{
        //    Debug.Log(item.ToString());
        //}

        //Dictionary<string, int> nameAge = new Dictionary<string, int>();
        //nameAge.Add("Gordon", 18);
        //nameAge.Add("Paul", 19);
        //nameAge.Add("Mathijs", 500);

        //foreach ( KeyValuePair<string, int> userInfo in nameAge)
        //{
        //    Debug.Log("Hi " + userInfo.Key + " Your age is " + userInfo.Value);
        //}

        //Stack diamond = new Stack();
        //diamond.Push(64);
        //diamond.Push("diamonds bby!");

        //foreach (var stack in diamond)
        //{
        //    Debug.Log(stack);
        //}

        //Debug.Log("diamond has " +  diamond.Count + " items");

        //while(diamond.Count > 0)
        //{
        //    Debug.Log(diamond.Pop());
        //}

        //Debug.Log("diamond has " + diamond.Count + " items");

        //Queue people = new Queue();
        //people.Enqueue(veryCoolNumbers.ToString());

        //foreach (var info in people)
        //{
        //    Debug.Log(info);
        //}

        //Item ball = new BonusItem("Ball", 10, 5);
        //Inventory.instance.AddItem(ball);

        //Debug.Log("You have " + Inventory.instance.Count() + " item in your inventory");

        ////Inventory.instance.RemoveItem(ball);

        //Debug.Log("You have " + Inventory.instance.Count() + " item in your inventory");

        //Item superAwesomeKeyOfAwesomeness = new AccessItem("superAwesomeKeyOfAwesomeness", 1, 0);

        //Inventory.instance.AddItem(superAwesomeKeyOfAwesomeness);

        //Debug.Log("You have " + Inventory.instance.Count() + " item in your inventory");
    }

    string ReadLocalJSONFile(string jsonFile)
    {
        string filePath = "" + jsonFile.Replace(".json", "");
        TextAsset ta = Resources.Load<TextAsset>(filePath);
        return ta.text;
    }

    JSONNode JsonParse(string jsonStr)
    {
        return JSON.Parse(jsonStr);
    }
}
