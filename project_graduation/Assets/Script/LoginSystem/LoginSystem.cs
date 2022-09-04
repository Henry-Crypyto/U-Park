using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LoginSystem : MonoBehaviour
{
    public Text LoginStat;
    public Text RegisterStat;
    public InputField UsernameLogin;
    public InputField PasswordLogin;
    public InputField UsernameRegister;
    public InputField PasswordRegister;
    public InputField PasswordVerify;
    public InputField CarNumber;
    public InputField CreditCardNumber;
    public Button LoginButton;
    public Button RegisterButton;
    public Button Register;
    public GameObject LoginUI;
    public GameObject RegisterUI;
    public static string UsernameTemp;
    
    
    void Start()
    {
        // RegisterUser("Henry","12345","12345","1234AB","1111222233334444");
       
        LoginButton.onClick.AddListener(()=>{
            StartCoroutine(Login(UsernameLogin.text,PasswordLogin.text)) ;
        });
        
        RegisterButton.onClick.AddListener(()=>{
        LoginUI.SetActive(false);
        RegisterUI.SetActive(true);

        Register.onClick.AddListener(()=>{
            StartCoroutine(RegisterUser(UsernameRegister.text,PasswordRegister.text,PasswordVerify.text,CarNumber.text,CreditCardNumber.text));
        });
        
        });
    }

    public void MoveToScenes(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    
    public IEnumerator Login(string username,string passwords)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", passwords);
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ParkingLot/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                if(www.downloadHandler.text=="Login Succes"){
                   LoginStat.text="LoginSucess!!";
                   LoginStat.color=Color.green;
                   UsernameTemp=username;
                   MoveToScenes(1);
                   
                //    ChangeScene.MoveToScene(1);
                }
                else{
                   LoginStat.text="Username or password is wrong!!";
                   LoginStat.fontSize=15;
                   LoginStat.color=Color.red;
                }
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator RegisterUser(string username,string passwords,string verifypassword,string CarNum,string CreditCardNum)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", passwords);
        form.AddField("loginCarNum", CarNum);
        form.AddField("loginCreditCardNum", CreditCardNum);
        if(passwords!=verifypassword){
               RegisterStat.text="Password verify failed!!";
        }
        else{
         using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ParkingLot/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                
                if(www.downloadHandler.text=="New record created successfully"){
                   LoginStat.text="Register Sucess!";
                   LoginStat.color=Color.green;
                   LoginUI.SetActive(true);
                   RegisterUI.SetActive(false);
                }
                else{
                    RegisterStat.text="User has already taken!";
                }
                
                Debug.Log(www.downloadHandler.text);
            }
        }
        }
        
    }
}

// public IEnumerator GetUser(string uri)
//     {
//         using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
//         {
//             // Request and wait for the desired page.
//             yield return webRequest.SendWebRequest();

//             string[] pages = uri.Split('/');
//             int page = pages.Length - 1;

//             switch (webRequest.result)
//             {
//                 case UnityWebRequest.Result.ConnectionError:
//                 case UnityWebRequest.Result.DataProcessingError:
//                     Debug.LogError(pages[page] + ": Error: " + webRequest.error);
//                     break;
//                 case UnityWebRequest.Result.ProtocolError:
//                     Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
//                     break;
//                 case UnityWebRequest.Result.Success:
//                     Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
//                     break;
//             }
//         }
//     }