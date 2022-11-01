using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterSystem : MonoBehaviour
{
    public TMPro.TMP_Text RegisterStat;
    public TMPro.TMP_InputField UserNameInputField;
    public TMPro.TMP_InputField AccountInputField;
    public TMPro.TMP_InputField PhoneNumberInputField;
    public TMPro.TMP_InputField PasswordInputField;
    public TMPro.TMP_InputField VerifyPasswordInputField;
    public Button ReturnLoginButton;
    public Button SignUpButton;
    
    void Start()
    {
        SignUpButton.onClick.AddListener(()=>{
            StartCoroutine(RegisterUser(UserNameInputField.text,PasswordInputField.text,VerifyPasswordInputField.text,AccountInputField.text,PhoneNumberInputField.text));
        });
        ReturnLoginButton.onClick.AddListener(()=>{
             MoveToScenes("Login");
        });
    }

    public void MoveToScenes(string sceneName)
    {
        SceneManager.LoadScene (sceneName);
    }

    public IEnumerator RegisterUser(string username,string passwords,string verifypassword,string Account,string PhoneNumber)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", passwords);
        form.AddField("loginAccount", Account);
        form.AddField("loginPhoneNumber", PhoneNumber);
        if(username==""){
            RegisterStat.text="Please enter username first!";
        }
        else if(PhoneNumber==""){
             RegisterStat.text="Please enter phonenumber first!";
        }
        else if(Account==""){
             RegisterStat.text="Please enter account first!";
        }
        else if(passwords==""){
             RegisterStat.text="Please enter password first!";
        }
        else if(passwords!=verifypassword){
               RegisterStat.text="Password verify failed!!";
        }
        else{
         using (UnityWebRequest www = UnityWebRequest.Post("https://breezeless-transmit.000webhostapp.com/phpfile/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                if(www.downloadHandler.text=="New record created successfully"){
                   RegisterStat.text="Register Sucess!";
                   RegisterStat.color=Color.green;
                   MoveToScenes("Login");
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