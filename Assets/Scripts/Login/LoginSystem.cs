using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LoginSystem : MonoBehaviour
{
    public TMPro.TMP_Text LoginStat;
    public TMPro.TMP_InputField AccountInputField;
    public TMPro.TMP_InputField PasswordInputField;
    public Button LoginButton;
    public Button RegisterButton;
    public static string Username;
    public static string Password;
    public static string CarNum;
    public static string CreditCard;
    
    
    void Start()
    {
        LoginButton.onClick.AddListener(()=>{
            StartCoroutine(Login(AccountInputField.text,PasswordInputField.text)) ;
        });
        
        RegisterButton.onClick.AddListener(()=>{
           MoveToScenes("Register");
        });
    }

    // public void MoveToScenes(int sceneID)
    // {
    //     SceneManager.LoadScene(sceneID);
    // }
    public void MoveToScenes(string sceneName)
    {
		//切換Scene
        SceneManager.LoadScene (sceneName);
    }
    
    public IEnumerator Login(string username,string passwords)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", passwords);
        
        using (UnityWebRequest www = UnityWebRequest.Post("https://breezeless-transmit.000webhostapp.com/phpfile/Login.php", form))
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
                   StartCoroutine(GetUserProfile(username));
                   MoveToScenes("FindSlot");
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
    public IEnumerator GetUserProfile(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        
        using (UnityWebRequest www = UnityWebRequest.Post("https://breezeless-transmit.000webhostapp.com/phpfile/Profile.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
              string s = www.downloadHandler.text;
              string[] subs = s.Split(' ');
              Username=subs[0];
              Password=subs[1];
              CarNum=subs[2];
              CreditCard=subs[3];
            }
        }
    }
}

