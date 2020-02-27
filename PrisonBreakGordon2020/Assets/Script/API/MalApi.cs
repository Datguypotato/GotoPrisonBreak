using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MalApi : JSONImageRequest
{
    //public TMP_InputField inputField;
    //public TMP_Text text;

    public Image animeImage;

    string animeImageUrl;
    string MALUsername;

    string randomAnimeName;

    ApiState currState = ApiState.GetRandomAnime;

    enum ApiState
    {
        GetRandomAnime,
        AnimeQRCode,
        MyAnimeListUser
    }

    private void Start()
    {
        //Request();
    }

    public void AssignUsername(string username)
    {
        if(username != "")
        {
            MALUsername = username;
            Request();
        }
        
    }

    public override void Request()
    {
        switch (currState)
        {
            case ApiState.GetRandomAnime:
                baseUrl = "https://api.jikan.moe/v3/user/" + "datguypotato/animelist/completed";
                break;
            case ApiState.AnimeQRCode:
                baseUrl = "https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=" + animeImageUrl;
                break;
            case ApiState.MyAnimeListUser:
                baseUrl = "https://api.jikan.moe/v3/user/" + MALUsername + "/animelist/completed";
                break;
        }

        Debug.Log("Requesting JSON ......\n" + baseUrl);
        if(currState == ApiState.GetRandomAnime || currState == ApiState.MyAnimeListUser)
        {
            StartCoroutine(RequestJson(baseUrl));
        }
        else
        {
            StartCoroutine(RequestImage(baseUrl));
        }
    }

    protected override void ShowData(JSONNode node)
    {
        switch (currState)
        {
            case ApiState.GetRandomAnime:
                // create a random anime node the user has to watch
                JSONNode randomAnimeNode = node["anime"][UnityEngine.Random.Range(0, node["anime"].Count)];

                // assign JSON
                //text.text = randomAnimeNode["title"];
                animeImageUrl = randomAnimeNode["image_url"];
                randomAnimeName = randomAnimeNode["title"];

                currState = ApiState.AnimeQRCode;
                Request();
                break;
            case ApiState.MyAnimeListUser:
                // check if user has completed the serie
                for (int i = 0; i < node["anime"].Count; i++)
                {
                    JSONNode animeNode = node["anime"][i];
                    Debug.Log(animeNode["title"]);
                    if(animeNode["title"] == randomAnimeName)
                    {
                        // check if completed
                        if (animeNode["watching status"] == "2")
                            Debug.Log("anime is completed");
                    }
                }
                break;
        }

    }

    protected override void ShowTexture(Texture2D texture)
    {
        // create QR code to find out which anime user has to watch
        animeImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        animeImage.preserveAspect = true;

        currState = ApiState.MyAnimeListUser;
    }

    private string GetSeason(DateTime date)
    {
        float value = (float)date.Month + date.Day / 100;   // <month>.<day(2 digit)>
        if (value < 3.21 || value >= 12.22) return "winter";
        if (value < 6.21) return "spring";
        if (value < 9.23) return "summer";
        return "autumn";
    }
}
