using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;
using UnityEngine;

public abstract class BaseJSONRequest : MonoBehaviour
{
    protected string baseUrl;

    public abstract void RequestJson();

    protected IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                string rawJson = webRequest.downloadHandler.text;
                JSONNode node = JSON.Parse(rawJson);

                ShowData(node);
            }
        }
    }

    protected abstract void ShowData(JSONNode node);
}
