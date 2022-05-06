//using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class buy : MonoBehaviour
{
   

    public Text sk1;
    public Text sk2;
    public Text sk3;
    public Text textPLAYER;
    public bool buyNow;
    public int numSkin;
    public int contSKIN;

    public Text skins;
    public Text skins1;
    public Text skins2;
    public Text NicknameText;
    public Text cambiado;

    public Text namefirst;
    public Text lastname;
    public Text emailt;
    //   public int idPlayer;
    [SerializeField]
    public InputField nicknameNew;
    void Start()
    {
      //  StartCoroutine(GetRequestUser1("http://localhost:8242/api/Users1/1"));

        StartCoroutine(GetRequestSkins("http://localhost:8242/api/Skins1"));

        buyNow = false;
    }

    void Update()
    {

        if (buyNow)
        {
           
            StartCoroutine(Postskin1("http://localhost:8242/api/PlayerSkins1/"));
            buyNow=false;
        }
      
    }
    public void mostrarDatos()
    {
      StartCoroutine(GetRequestUser("http://localhost:8242/api/Users1"));

    }
    public void CambioName()
    {
        StartCoroutine(PutRequest("http://localhost:8242/api/Players/1"));
    }
    public void sk1Bbt()
    {
        Debug.Log(GameManager.instance.idPlayer);
        numSkin = 1;
        contSKIN= Random.Range(3,300);
      
        buyNow = true;
        Debug.Log(contSKIN);
    }
    public void sk2Bbt()
    {
        numSkin = 2;
        contSKIN = Random.Range(600, 1000);
      
        buyNow = true;
    }
    public void sk3Bbt()
    {
        numSkin = 3;
        contSKIN = Random.Range(300, 600);
        buyNow = true;
    

    }
    IEnumerator GetRequestSkins(string url)
    {
        using (UnityWebRequest webrequest = UnityWebRequest.Get(url))
        {
            yield return webrequest.SendWebRequest();

            switch (webrequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    print("error");
                    break;
                case UnityWebRequest.Result.Success:
                    // print(webrequest.downloadHandler.text);
                    SkinsPlayer skinsPlayer=JsonUtility.FromJson<SkinsPlayer>("{\"skinsL\":" + webrequest.downloadHandler.text+"}");
                    for (int i = 0; i < skinsPlayer.skinsL.Length; i++)
                    {                
                        sk1.text = skinsPlayer.skinsL[0].name.ToString();
                        sk2.text = skinsPlayer.skinsL[1].name.ToString();
                        sk3.text = skinsPlayer.skinsL[2].name.ToString();
                    }
                    break;

            };
        }
    }
    IEnumerator PutRequest(string url)
    {
        string json = "{\"Id\":  \""+ GameManager.instance.idPlayer+"\", \"NickName\":\"" + nicknameNew.text + "\" }";
        byte[] body = Encoding.UTF8.GetBytes(json);
        using (UnityWebRequest webrequest = UnityWebRequest.Put(url, body))
        {
            webrequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(body);
            webrequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webrequest.SetRequestHeader("Content-Type", "application/json");
            yield return webrequest.SendWebRequest();

            switch (webrequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    cambiado.text = "YA EXISTE UN USUARIO CON ESTE NOMBRE";
                    print("error");
                    break;
                case UnityWebRequest.Result.Success:
                    cambiado.text = "CAMBIADO CON EXITO";
                    print(webrequest.downloadHandler.text);
                    Player player = JsonUtility.FromJson<Player>(webrequest.downloadHandler.text);
                
              
                    break;

            };
        }
    }



    IEnumerator Postskin1(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerId", GameManager.instance.idPlayer);
        form.AddField("skinId", numSkin);
        form.AddField("date", "2022-03-11T10:01:00");
        form.AddField("id", contSKIN);
        using (UnityWebRequest webrequest = UnityWebRequest.Post(url, form))
        {
            yield return webrequest.SendWebRequest();

            switch (webrequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    print("error");
                    break;
                case UnityWebRequest.Result.Success:
                    print(webrequest.downloadHandler.text);
                    //Player player = JsonUtility.FromJson<Player>(webrequest.downloadHandler.text);
                    PlayerSkin playerSkin = JsonUtility.FromJson<PlayerSkin>(webrequest.downloadHandler.text);
                    // print(playerSkin.skinId);
                    buyNow = false;
                    break;

            };
        }
    }
    IEnumerator GetRequestUser(string url)
    {
        using (UnityWebRequest webrequest = UnityWebRequest.Get(url))
        {
            yield return webrequest.SendWebRequest();

            switch (webrequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    print("error");
                    break;
                case UnityWebRequest.Result.Success:
                    UsersD usersD = JsonUtility.FromJson<UsersD>("{\"usersDe\":" + webrequest.downloadHandler.text + "}");
         

                    for (int i = 0; i < usersD.usersDe.Length; i++)
                    {
                     //   print(usersD.usersDe[GameManager.instance.idPlayer].email);
                        namefirst.text = (usersD.usersDe[GameManager.instance.idPlayer-1].firstName);
                        lastname.text = (usersD.usersDe[GameManager.instance.idPlayer-1].lastName);
                        emailt.text = (usersD.usersDe[GameManager.instance.idPlayer-1].email);
                        // NicknameText.text = (playersT.playersC[GameManager.instance.idPlayer].nickName).ToString();

                    }
                    break;

            };
        }
    }



    IEnumerator GetRequestUser1(string url)
    {
        using (UnityWebRequest webrequest = UnityWebRequest.Get(url))
        {
            yield return webrequest.SendWebRequest();

            switch (webrequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    print("error");
                    break;
                case UnityWebRequest.Result.Success:
                    User user = JsonUtility.FromJson<User>(webrequest.downloadHandler.text);


              //   print(user.email);
                    break;

            };
        }
    }
}
