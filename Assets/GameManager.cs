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
    void Start()
    {
        instance = this; 
        //   StartCoroutine(PostRequestPlayers("http://localhost:8242/api/players/"));
        canvasBuy.SetActive(false);
        canvasMenu.SetActive(false);
        canvasNicjname.SetActive(false);
        canvasLogin.SetActive(true);
        activarSkins = false;
        activarCambio = false;
    }
    public void boton()
    {

        StartCoroutine(LoginUser("http://localhost:8242/api/Users1/"));
        canvasMenu.SetActive(true);
        canvasBuy.SetActive(false);
        canvasLogin.SetActive(false);
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
    }
    public void NicknameCanvas()
    {
        canvasMenu.SetActive(false);
        canvasBuy.SetActive(false);
        canvasLogin.SetActive(false);
        canvasNicjname.SetActive(true);
    }

    public void ShopCanvas()
    {
        canvasMenu.SetActive(false);
        canvasBuy.SetActive(true);
        canvasLogin.SetActive(false);
        canvasNicjname.SetActive(false);
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

              //    print(player.id+player.nickName);

                    idPlayer=player.id;
                    nameP = player.nickName;
                    //SkinsAdd = player.playerSkins[0].skin.name;
                   // print(player);
                    NamePlayer.text=nameP.ToString();
                    NamePlayer1.text=nameP.ToString();
                   
                    skins.text=player.playerSkins[0].skin.name.ToString();
                    skins1.text=player.playerSkins[1].skin.name.ToString();
                    skins2.text=player.playerSkins[2].skin.name.ToString();
                   
                   // print(player.playerSkins[0].skin.name);

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
