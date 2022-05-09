using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
  public static GameManager instance;
    [SerializeField]
    private InputField correo;
    [SerializeField]
    private InputField contra;

    public Text NamePlayer;
    public Text NamePlayer1;
    public Text NamePlayer2;
  //  public Text correotext;
  
   // private string loginText;

    public GameObject canvasBuy;
    public GameObject canvasNicjname;
    public GameObject canvasMenu;
    public GameObject canvasSkins;
    public GameObject canvasLogin;

    public Text skins;
    public Text skins1;
    public Text skins2;

 
    public int idPlayer;
    public string nameP;
    public string SkinsAdd;

    public bool activarSkins;
    public bool activarCambio;

    public int IdSkinplayer;
    public int IdSkinplayer1;
    public int IdSkinplayer2;
    public int IdSkinplayer3;

    public Text sskins;
    public Text sskins1;
    public Text sskins2;
    public Text sskins3;
    public Image sk1;
    public Sprite[] listaSkin;
    public int conts=0;
    private string infinite;
    private string infinito="Infinite";
    void Start()
    {
        instance = this; 
        //   StartCoroutine(PostRequestPlayers("http://localhost:8242/api/players/"));
        canvasBuy.SetActive(false);
        canvasMenu.SetActive(false);
        canvasNicjname.SetActive(false);
        canvasSkins.SetActive(false);
        canvasLogin.SetActive(true);
        activarSkins = false;
        activarCambio = false;

        //sk1 = GameObject.Find("skinImagen").GetComponent<Image>();
        //   sk1.sprite = listaSkin[numS];
        listaSkin[0] = Resources.Load<Sprite>("Sprites/Infinite");
        listaSkin[1] = Resources.Load<Sprite>("Sprites/Delta");
        listaSkin[2] = Resources.Load<Sprite>("Sprites/Patito");
        //listaSkin[3] = Resources.Load<Sprite>("Sprites/i2");
    }
    public void boton()
    {

        StartCoroutine(LoginUser("http://localhost:8242/api/Users1/"));
        canvasMenu.SetActive(true);
        canvasBuy.SetActive(false);
        canvasLogin.SetActive(false);
        canvasSkins.SetActive(false);
        canvasNicjname.SetActive(false);
        activarSkins=true;
        activarCambio=true;
    }
    public void backmenu()
    {
        canvasMenu.SetActive(true);
        canvasBuy.SetActive(false);
        canvasLogin.SetActive(false);
        canvasNicjname.SetActive(false);
        canvasSkins.SetActive(false);
    }
    public void NicknameCanvas()
    {
        canvasMenu.SetActive(false);
        canvasBuy.SetActive(false);
        canvasLogin.SetActive(false);
        canvasNicjname.SetActive(true);
        canvasSkins.SetActive(false);
    }

    public void ShopCanvas()
    {
        canvasMenu.SetActive(false);
        canvasBuy.SetActive(true);
        canvasLogin.SetActive(false);
        canvasNicjname.SetActive(false);
        canvasSkins.SetActive(false);
    }

    public void SkinsCanvas()
    {
        StartCoroutine(LoginUser("http://localhost:8242/api/Users1/"));
        canvasMenu.SetActive(false);
        canvasBuy.SetActive(false);
        canvasLogin.SetActive(false);
        canvasNicjname.SetActive(false);
        canvasSkins.SetActive(true);

    }
    public void DeleteSkin1()
    {
        StartCoroutine(Delete("http://localhost:8242/api/PlayerSkins1/"+ IdSkinplayer));
    }
    public void DeleteSkin2()
    {
        StartCoroutine(Delete("http://localhost:8242/api/PlayerSkins1/" + IdSkinplayer1));
    }
    public void DeleteSkin3()
    {
        StartCoroutine(Delete("http://localhost:8242/api/PlayerSkins1/" + IdSkinplayer2));
    }
    public void DeleteSkin4()
    {
        StartCoroutine(Delete("http://localhost:8242/api/PlayerSkins1/" + IdSkinplayer3));
    }

    private void Update()
    {
        if (infinite == infinito)
        {
            Debug.Log(infinite);
            Debug.Log("asaas");
            sk1.sprite = Resources.Load<Sprite>("Sprites/Infinite");
        }
    }
    IEnumerator LoginUser(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("Email", correo.text);
        form.AddField("mnk", contra.text);
       
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
                   // print(webrequest.downloadHandler.text);

                    Player player = JsonUtility.FromJson<Player>(webrequest.downloadHandler.text);
                    idPlayer = player.id;
                    nameP = player.nickName;
                    //print(nameP);

                    NamePlayer.text = nameP.ToString();
                    NamePlayer1.text = nameP.ToString();
                    NamePlayer2.text = nameP.ToString();

                    //================================
                 
                    //name

                 

                    //skims

                    Skin[] aux = new Skin[player.playerSkins.Length];

                    for (int i = 0; i < player.playerSkins.Length; i++)
                    {

                        skins.text = player.playerSkins[0].skin.name.ToString();
                        sskins.text = player.playerSkins[0].skin.name.ToString();

                        skins1.text = player.playerSkins[1].skin.name.ToString();
                        sskins1.text = player.playerSkins[1].skin.name.ToString();

                        skins2.text = player.playerSkins[2].skin.name.ToString();
                        sskins2.text = player.playerSkins[2].skin.name.ToString();

                        sskins3.text = player.playerSkins[i].skin.name.ToString();

                        IdSkinplayer = player.playerSkins[0].id;
                        IdSkinplayer1 = player.playerSkins[1].id;
                        IdSkinplayer2 = player.playerSkins[2].id;
                        IdSkinplayer3 = player.playerSkins[i].id;

                        print(IdSkinplayer);
                        print(IdSkinplayer1);
                        print(IdSkinplayer2);
                        print(IdSkinplayer3);

                    }

                    break;

            };
        }
    }

    IEnumerator Delete(string url)
    {
        using (UnityWebRequest webrequest = UnityWebRequest.Delete(url))
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
                    
                    print("Se elimino");

                    break;

            };
        }
    }

    IEnumerator PostRequestPlayers(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickName", "Pepel");
        form.AddField("id", 2);
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
                    Player player = JsonUtility.FromJson<Player>(webrequest.downloadHandler.text);
                    print(player.id);

                    break;

            };
        }
    }

}
