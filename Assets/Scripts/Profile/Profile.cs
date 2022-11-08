using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Profile : MonoBehaviour
{
    public Button EditButton;

    public TMPro.TMP_Text UsernameContent;
    public TMPro.TMP_InputField PasswordContent;
    public TMPro.TMP_Text PhoneNumberCotent;
    public TMPro.TMP_Text CarNumContent;
    public TMPro.TMP_Text CreditCardContent;

    public static string Username;
    public static string Password;
    public static string PhoneNumber;
    public static string CarNum;
    public static string CreditCard;

    void Start()
    {
        // UsernameContent.text=LoginSystem.Username;
        // PasswordContent.text=LoginSystem.Password;
        // CarNumContent.text=LoginSystem.CarNum;
        // CreditCardContent.text=LoginSystem.CreditCard;
        // PhoneNumber.text=LoginSystem.PhoneNumber;

        StartCoroutine(GetUserProfile(LoginSystem.AccountName));
        EditButton.onClick.AddListener(() => {
            SceneManager.LoadScene("EditProfile");
        });
    }
    
    public IEnumerator GetUserProfile(string Account)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", Account);

        using (UnityWebRequest www = UnityWebRequest.Post("https://u-parkprojectgraduation.com/phpfile/Profile.php", form)) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                string s = www.downloadHandler.text;
                string[] subs = s.Split(' ');
                Username = subs[0];
                Password = subs[1];
                CarNum = subs[2];
                CreditCard = subs[3];
                PhoneNumber = subs[4];

                UsernameContent.text = subs[0];
                PasswordContent.text = subs[1];
                CarNumContent.text = subs[2];
                CreditCardContent.text = subs[3];
                PhoneNumberCotent.text = subs[4];
            }
        }
    }
}