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
            StartCoroutine(GetAPiData(baseUrl + "Gordon"));
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
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                JSONNode node = JSON.Parse(webRequest.downloadHandler.text);
                Dictionary<string, float> countryInfo = new Dictionary<string, float>();
                
                for (int i = 0; i < node[1].Count; i++)
                {
                    JSONNode countryParent = node[1][i];
                    for (int x = 0; x < countryParent[x]; x++)
                    {
                        countryInfo.Add(node[i][x][0], node[i][x][1]);
                    }
                }

                ShowContent.text = node[0] + "\n" + "Country: " + node[1][0][0].ToString() + "\nPossiblity: " + node[1][0][1];

                foreach (KeyValuePair<string, float> info in countryInfo)
                {
                    Debug.Log(info.Key + "\n" + info.Value);
                }
            }
        }
    }
}
