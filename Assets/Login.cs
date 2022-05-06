using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Login : MonoBehaviour
{
    [SerializeField]
    private InputField correo;
    [SerializeField]
    private InputField contra;
    public Text comprobacion;
    private string loginText;
    private string passwordText;
    public Text skins;
    public Text skins1;
    public Text skins2;
    public GameObject canvasBuy;
    public GameObject canvasLogin;
    void Start()
    {
      //   StartCoroutine(PostRequestPlayers("http://localhost:8242/api/players/"));
     
        canvasBuy.SetActive(false);
        canvasLogin.SetActive(true);
    }
    public void boton()
    {
       
        StartCoroutine(LoginUser("http://localhost:8242/api/Users1/"));
        canvasBuy.SetActive(true);
        canvasLogin.SetActive(false);
    }
    
    void Update()
    {
        
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
                    print(webrequest.downloadHandler.text);
                 
                    Player player=JsonUtility.FromJson<Player>(webrequest.downloadHandler.text);

                    //  print(user.Email);
               
                    print(player.playerSkins[0].skin.name);

                    break;

            };
        }
    }
    IEnumerator PostRequestPlayers(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickName", "Pepel");
        form.AddField("id", 2);
        using (UnityWebRequest webrequest = UnityWebRequest.Post(url,form))
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
