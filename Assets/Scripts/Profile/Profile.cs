using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Profile : MonoBehaviour
{
      
      public TMPro.TMP_Text UsernameContent;
      public TMPro.TMP_InputField PasswordContent;
      public TMPro.TMP_Text CarNumContent;
      public TMPro.TMP_Text CreditCardContent;
      public TMPro.TMP_Text PhoneNumberCotent;
      public Button EditButton;
      public static string Username;
      public static string Password;
      public static string CarNum;
      public static string CreditCard;
      public static string PhoneNumber;

    void Start()
    {
        // UsernameContent.text=LoginSystem.Username;
        // PasswordContent.text=LoginSystem.Password;
        // CarNumContent.text=LoginSystem.CarNum;
        // CreditCardContent.text=LoginSystem.CreditCard;
        // PhoneNumber.text=LoginSystem.PhoneNumber;
       StartCoroutine(GetUserProfile(LoginSystem.AccountName));
        EditButton.onClick.AddListener(()=>{
          MoveToScenes("EditProfile");
        }); 
        
    }
    public void MoveToScenes(string sceneName)
    {
        SceneManager.LoadScene (sceneName);
    }
    public IEnumerator GetUserProfile(string Account)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("loginUser", Account);
        
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
              PhoneNumber=subs[4];
              

              UsernameContent.text=subs[0];
              PasswordContent.text=subs[1];
              CarNumContent.text=subs[2];
              CreditCardContent.text=subs[3];
              PhoneNumberCotent.text=subs[4];
            }
        }
    }
}
