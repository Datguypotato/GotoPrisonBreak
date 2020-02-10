using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine;
using TMPro;

public class NameAPI : MonoBehaviour
{

    public TMP_InputField nameInput;
    public TMP_Text ShowContent;

    private string baseUrl = "https://api.nationalize.io?name=";


    public void ButtonClick()
    {
        //if (nameInput.text != "")
        {
            StartCoroutine(GetAPiData(baseUrl + nameInput.text));
        }
    }

    IEnumerator GetAPiData(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                // download text from website
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                JSONNode node = JSON.Parse(webRequest.downloadHandler.text);
                Dictionary<string, float> countryInfo = new Dictionary<string, float>();
                
                // Get relevant data
                for (int i = 0; i < node[1].Count; i++)
                {
                    JSONNode parentNode = node[1][i];
                    Debug.Log(node[1].Count);
                    countryInfo.Add(parentNode[0], parentNode[1]);
                }

                // show relevant data

                // name
                ShowContent.text = node[0];

                foreach (KeyValuePair<string, float> info in countryInfo)
                {
                    ShowContent.text += "\nCountry ID: " + info.Key + "\nPossiblity: " + Math.Round(info.Value, 2) + "%";
                }


            }
        }
    }
}
