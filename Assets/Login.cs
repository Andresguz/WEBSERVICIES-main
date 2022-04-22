using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Login : MonoBehaviour
{
    [SerializeField]
    private InputField correo=null;
    [SerializeField]
    private InputField contra=null;
    public Text comprobacion;
    private string loginText;
    private string passwordText;
    void Start()
    {
      //   StartCoroutine(PostRequestPlayers("http://localhost:8242/api/players/"));
     

    }
    public void boton()
    {
        StartCoroutine(LoginUser("http://localhost:8242/api/Users1/"));
    }
    public void readStringLogin(string s)
    {
        loginText = s;
       // Debug.Log(loginText);
    }
    public void readStringPassword(string s)
    {
        passwordText = s;
      //  Debug.Log(passwordText);
    }
    void Update()
    {
        
    }

     IEnumerator LoginUser(string url)
    {
        WWWForm form = new WWWForm();

       // form.AddField("FirstName", "Carlos");
       // form.AddField("LastName", "perez");
        //form.AddField("dateOfBirthday", "2022-04-06T09:43:00");
        //form.AddField("middleName", "loep");

        //form.AddField("Age", 22);
        form.AddField("Email", correo.text);
        form.AddField("Password", contra.text);
        //form.AddField("Mnk","huevos123");
       
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
                 
                    User user=JsonUtility.FromJson<User>(webrequest.downloadHandler.text);

                   print(user.Email);
                    //if (user.Email == "andresguzguzman@gmail.com")
                    //{
                    //    Debug.Log("aaa");
                    //}

                    // print(player.nickName);
                    //if (correo.text == "papa")
                    //{
                    //    comprobacion.text = "exitoso";
                    //}
                  
                  
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
