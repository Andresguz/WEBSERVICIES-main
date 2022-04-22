using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class request : MonoBehaviour
{
    public Text a1;
    public Text a2;
    public Text a3;
    public Text a4;
    public Text a5;
    public Text a6;
    public Text a7;
    public Text a8;
    public Text a9;
    public Text a10;

    public Text b1;
    public Text b2;
    public Text b3;
    public Text b4;
    public Text b5;
    public Text b6;
    public Text b7;
    public Text b8;
    public Text b9;
    public Text b10;
    public Image sk1;
    public Sprite[] listaSkin;
    public Text Name;
    public Text SkinName;
    public int numS=0;
    public Button levelBtnPref;
    public Button levelBtnParent;
    public Button LevelButtonItem;
    void Start()
    {
       // StartCoroutine(GetRequest("http://localhost:8242/api/players/1"));
        StartCoroutine(GetRequestPlayers("http://localhost:8242/api/players"));
        sk1 = GameObject.Find("skinImagen").GetComponent<Image>();
     //   sk1.sprite = listaSkin[numS];
        listaSkin[0] = Resources.Load<Sprite>("Sprites/i2");
        listaSkin[1] = Resources.Load<Sprite>("Sprites/i3");
        listaSkin[2] = Resources.Load<Sprite>("Sprites/i4");
    }
   
    public void next()
    {
       
        StartCoroutine(GetRequest("http://localhost:8242/api/players/1"));
        sk1.sprite = listaSkin[numS];
      
        numS--;
    }
    public void last()
    {
        StartCoroutine(GetRequest("http://localhost:8242/api/players/1"));

        numS++;
        sk1.sprite = listaSkin[numS];
    }
    void Update()
    {
        sk1.sprite = listaSkin[numS];

    }
    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webrequest = UnityWebRequest.Get(url))
        {
            yield return webrequest.SendWebRequest();

            switch (webrequest.result) {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    print("error");
                    break;
                case UnityWebRequest.Result.Success:
                   // print(webrequest.downloadHandler.text);
                    Player player = JsonUtility.FromJson<Player>(webrequest.downloadHandler.text);
                    Skin[] aux= new Skin[player.playerSkins.Length];
//                 print(player.playerSkins[0].skin.name);
                    //if (player.playerSkins[0].skin.name == "Delta")
                    //{
                    //    Debug.Log("kkk");
                    //    sk1.sprite = Resources.Load<Sprite>("Sprites/i2");
                    //}
                 for (int i = 0; i < player.playerSkins.Length; i++)
                  
                    {
                        aux[i] = player.playerSkins[i].skin;
                    //  SkinName.text = player.playerSkins[i].skin.name;
                    }
                
                    Name.text = player.nickName;
                  //  LoadLevelButtons(aux);
                    break;

            };
        }
    }

    private void LoadLevelButtons(Player[] players)
    {
        for (int i = 0; i < players.Length; i++)
        {
          //  GameObject levelBtnObj = Instantiate(levelBtnPref, levelBtnParent) as GameObject;
          //  levelBtnObj.GetComponent<LevelButtonItem>().player = players[i].nickName;
         //   levelBtnObj.GetComponent<LevelButtonItem>().levelScrollViewController =this ;
        }
        

    }
    IEnumerator GetRequestPlayers(string url)
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


                  //  print(webrequest.downloadHandler.text);
                   // PlayersT players = JsonUtility.FromJson<PlayersT>("{\"players\":" + webrequest.downloadHandler.text + "}");
                    PlayersT playersT = JsonUtility.FromJson<PlayersT>("{\"playersC\":" + webrequest.downloadHandler.text + "}");
                    //   print(players.players.);

                    for (int i = 0; i < playersT.playersC.Length; i++)
                    {
                        //print("Players " + playersT.playersC[i].nickName + "skins " + playersT.playersC[i].playerSkins[i].skin.name);
                        print("Players " + playersT.playersC[0].id + (" ") + playersT.playersC[0].nickName + playersT.playersC[0].playerSkins[0].skin.name + (" ") + playersT.playersC[0].playerSkins[1].skin.name);

                        a1.text = (playersT.playersC[0].id + (" ") + playersT.playersC[0].nickName + playersT.playersC[0].playerSkins[0].skin.name+ playersT.playersC[0].playerSkins[1].skin.name).ToString();
                        a2.text = (playersT.playersC[1].id + (" ") + playersT.playersC[1].nickName + playersT.playersC[1].playerSkins[0].skin.name).ToString();
                        a3.text = (playersT.playersC[2].id + (" ") + playersT.playersC[2].nickName + playersT.playersC[2].playerSkins[0].skin.name).ToString();
                        a4.text = (playersT.playersC[3].nickName + (" ") + playersT.playersC[3].playerSkins[0].skin.name).ToString();
                        a5.text = (playersT.playersC[4].nickName + (" ") + playersT.playersC[4].playerSkins[0].skin.name).ToString();
                        a6.text = (playersT.playersC[5].nickName + (" ") + playersT.playersC[5].playerSkins[0].skin.name).ToString();
                        a7.text = (playersT.playersC[6].nickName + (" ") + playersT.playersC[6].playerSkins[0].skin.name).ToString();
                        a8.text = (playersT.playersC[7].nickName + (" ") + playersT.playersC[7].playerSkins[0].skin.name).ToString();
                        a9.text = (playersT.playersC[8].nickName + (" ") + playersT.playersC[8].playerSkins[0].skin.name).ToString();
                        a10.text = (playersT.playersC[9].nickName + (" ") + playersT.playersC[9].playerSkins[0].skin.name).ToString();



                        b1.text = (playersT.playersC[1].id + (" ") + playersT.playersC[1].playerSkins[0].skin.name).ToString();
                      //  b2.text = (playersT.playersC[1].id + (" ") + playersT.playersC[1].playerSkins[1].skin.name).ToString();
                       // b2.text = (playersT.playersC[1].id + (" ") + playersT.playersC[1].playerSkins[0].skin.name).ToString();
                        b3.text = (playersT.playersC[1].nickName).ToString();
                       // b4.text = (playersT.playersC[0].id + (" ") + playersT.playersC[0].playerSkins[2].skin.name).ToString();
                        b5.text = (playersT.playersC[4].id + (" ") + playersT.playersC[4].playerSkins[0].skin.name).ToString();
                        b6.text = (playersT.playersC[5].id + (" ") + playersT.playersC[5].playerSkins[0].skin.name).ToString();
                        b7.text = (playersT.playersC[6].id + (" ") + playersT.playersC[6].playerSkins[0].skin.name).ToString();
                        b8.text = (playersT.playersC[7].id + (" ") + playersT.playersC[7].playerSkins[0].skin.name).ToString();
                        b9.text = (playersT.playersC[8].id + (" ") + playersT.playersC[8].playerSkins[0].skin.name).ToString();
                        b10.text = (playersT.playersC[9].id + (" ") + playersT.playersC[9].playerSkins[0].skin.name).ToString();
                   
                     

                    }
                    break;

            };
        }
    }

  
}
