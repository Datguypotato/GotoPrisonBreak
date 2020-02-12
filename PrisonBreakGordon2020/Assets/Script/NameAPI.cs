using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine;
using TMPro;

public class NameAPI : BaseJSONRequest
{

    public TMP_InputField nameInput;
    public TMP_Text ShowContent;

    public double lastPercent;

    public override void RequestJson()
    {
        baseUrl = "https://api.nationalize.io?name=";
        if (nameInput.text != "")
        {
            StartCoroutine(GetRequest(baseUrl + nameInput.text));
        }
    }

    //IEnumerator GetAPiData(string uri)
    //{
    //    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    //    {
    //        // Request and wait for the desired page.
    //        yield return webRequest.SendWebRequest();

    //        string[] pages = uri.Split('/');
    //        int page = pages.Length - 1;

    //        if (webRequest.isNetworkError)
    //        {
    //            Debug.Log(pages[page] + ": Error: " + webRequest.error);
    //        }
    //        else
    //        {
    //            // download text from website
    //            Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
    //            JSONNode node = JSON.Parse(webRequest.downloadHandler.text);
    //            Info[] infos = new Info[3];
    //            //Dictionary<string, string> countryInfo = new Dictionary<string, string>();
                
    //            // Get relevant data
    //            for (int i = 0; i < node[1].Count; i++)
    //            {
    //                JSONNode parentNode = node[1][i];
    //                //Debug.Log(node[1].Count);
    //                infos[i].countryCode = parentNode[0];
    //                infos[i].percent = parentNode[1];
    //                //countryInfo.Add(parentNode[0], parentNode[1]);
    //            }

    //            // show relevant data

    //            // name
    //            ShowContent.text = node[0];

    //            foreach (Info info in infos)
    //            {
    //                //Debug.Log(info.Value);
    //                //lastPercent = info.Value;
    //                ShowContent.text += "\nCountry ID: " + info.countryCode + "\nPossiblity: " + info.percent.Remove(4, info.percent.Length - 4) + "%";
    //            }


    //        }
    //    }
    //}

    protected override void ShowData(JSONNode node)
    {
        Info[] infos = new Info[3];
        //Dictionary<string, string> countryInfo = new Dictionary<string, string>();

        // Get relevant data
        for (int i = 0; i < node[1].Count; i++)
        {
            JSONNode parentNode = node[1][i];
            //Debug.Log(node[1].Count);
            infos[i].countryCode = parentNode[0];
            infos[i].percent = parentNode[1];
            //countryInfo.Add(parentNode[0], parentNode[1]);
        }

        // show relevant data

        // name
        ShowContent.text = node[0];

        foreach (Info info in infos)
        {
            //Debug.Log(info.Value);
            //lastPercent = info.Value;
            ShowContent.text += "\nCountry ID: " + info.countryCode + "\nPossiblity: " + info.percent.Remove(4, info.percent.Length - 4) + "%";
        }
    }

    public struct Info
    {
        public string countryCode;
        public string percent;
    }
}
