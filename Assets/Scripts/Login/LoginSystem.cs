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
    public static string AccountName;

    void Start()
    {
        LoginButton.onClick.AddListener(() => {
            StartCoroutine(Login(AccountInputField.text, PasswordInputField.text));
        });
    }

    public IEnumerator Login(string Account, string password)
    {

        WWWForm form = new WWWForm();
        form.AddField("loginUser", Account);
        form.AddField("loginPass", password);

        if (Account == "") {
            LoginStat.text = "Account is required.";

        }
        else if (password == "") {
            LoginStat.text = "Password is required.";

        }
        else {
            using (UnityWebRequest www = UnityWebRequest.Post("http://u-parkprojectgraduation.com/phpfile/Login.php", form)) {
                www.useHttpContinue=false;
                yield return www.SendWebRequest();
                
                if (www.result != UnityWebRequest.Result.Success) {
                    Debug.Log(www.error);
                }
                else {
                    if (www.downloadHandler.text == "Login Succes") {
                        AccountName = Account;
                        LoginStat.text = "Login Success.";
                        LoginStat.color = Color.green;
                        SceneManager.LoadScene("EnterQR_Generate");
                    }
                    else {
                        LoginStat.text = "Wrong account or password.\n";
                        LoginStat.color = Color.red;
                    }
                }
            }
        }
    }
}