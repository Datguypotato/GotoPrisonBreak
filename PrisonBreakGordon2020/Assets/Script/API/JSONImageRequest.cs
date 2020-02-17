using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public abstract class JSONImageRequest : BaseJSONRequest
{

    protected IEnumerator RequestImage(string url)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                Texture2D requestedTexture = DownloadHandlerTexture.GetContent(uwr);

                ShowTexture(requestedTexture);
            }
        }
    }

    protected abstract void ShowTexture(Texture2D texture);
}
