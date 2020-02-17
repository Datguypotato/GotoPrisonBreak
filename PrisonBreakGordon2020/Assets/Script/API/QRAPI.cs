using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class QRAPI : JSONImageRequest
{

    public Image QrImage;

    private void Start()
    {
        baseUrl = "https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=";

        StartCoroutine(RequestImage(baseUrl + "https://youtu.be/PsBPPkt0Rbg"));
    }

    protected override void ShowData(JSONNode node)
    {
        throw new System.NotImplementedException();
    }

    protected override void ShowTexture(Texture2D texture)
    {
        QrImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        QrImage.preserveAspect = true;
    }
}
