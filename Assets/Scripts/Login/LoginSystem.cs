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
    public static string AccountName;
    
    
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
    
    public IEnumerator Login(string Account,string password)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("loginUser", Account);
        form.AddField("loginPass", password);
        if(Account==""){
            LoginStat.text="Please Enter Account first!!!";
            
        }
        else if(password==""){
            LoginStat.text="Please Enter password first!!!";
           
        }
        else{
          using (UnityWebRequest www = UnityWebRequest.Post("https://u-parkprojectgraduation.com/phpfile/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                if(www.downloadHandler.text=="Login Succes"){
                   AccountName=Account;
                   LoginStat.text="LoginSucess!!"+www.downloadHandler.text;
                   LoginStat.color=Color.green;
                //    StartCoroutine(GetUserProfile(Account));
                //    MoveToScenes("FindSlot");
                }
                else{
                   LoginStat.text="Account or password is wrong!!"+www.downloadHandler.text;
                   LoginStat.color=Color.red;
                }
                
            }
        }
    }
}
}