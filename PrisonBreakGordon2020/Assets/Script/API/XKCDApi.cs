using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;
using UnityEngine;

public class XKCDApi : JSONImageRequest
{
    string endUrl = "/info.0.json";

    public Image xkcdImage;
    public TMP_Text imageTitle;

    // Start is called before the first frame update
    void Start()
    {
        baseUrl = "https://xkcd.com/";
        Request();
    }

    public override void Request()
    {
        // getting a random comic
        int randomComic = Random.Range(0, 666);
        StartCoroutine(RequestJson(baseUrl + randomComic + endUrl));
    }

    // old IEnumerator
    //IEnumerator GetRequest(string uri)
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
    //            //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

    //            JSONNode node = JSON.Parse(webRequest.downloadHandler.text);
    //            Debug.Log(node[8]);
    //            StartCoroutine(GetText(node[8]));
    //            imageTitle.text = node[5];
    //        }
    //    }
    //}

    protected override void ShowData(JSONNode node)
    {
        StartCoroutine(RequestImage(node[8]));
        imageTitle.text = node[5];
    }

    protected override void ShowTexture(Texture2D texture)
    {
        xkcdImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        xkcdImage.preserveAspect = true;
    }

    //IEnumerator GetImage(string url)
    //{
    //    using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
    //    {
    //        yield return uwr.SendWebRequest();

    //        if (uwr.isNetworkError || uwr.isHttpError)
    //        {
    //            Debug.Log(uwr.error);
    //        }
    //        else
    //        {
    //            // Get downloaded asset bundle
    //            Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
    //            //xkcdImage.rectTransform.sizeDelta = new Vector2(texture.width, texture.height);
    //            xkcdImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    //            xkcdImage.preserveAspect = true;
    //        }
    //    }
    //}


}
