using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterSystem : MonoBehaviour
{
    public TMPro.TMP_Text RegisterStat;
    public TMPro.TMP_InputField UserNameInputField;
    public TMPro.TMP_InputField PhoneNumberInputField;
    public TMPro.TMP_InputField AccountInputField;
    public TMPro.TMP_InputField PasswordInputField;
    public TMPro.TMP_InputField VerifyPasswordInputField;
    public Button SignUpButton;

    void Start()
    {
        SignUpButton.onClick.AddListener(() => {
            StartCoroutine(RegisterUser(UserNameInputField.text, PhoneNumberInputField.text, AccountInputField.text, PasswordInputField.text, VerifyPasswordInputField.text));
        });
    }
    public IEnumerator RegisterUser(string username, string PhoneNum, string Account, string passwords, string verifypassword)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", passwords);
        form.AddField("loginAccount", Account);
        form.AddField("loginPhoneNum", PhoneNum);
        if (username == "") {
            RegisterStat.text = "Please enter username first.";
        }
        else if (PhoneNum == "") {
            RegisterStat.text = "Please enter phonenumber first.";
        }
        else if (Account == "") {
            RegisterStat.text = "Please enter account first.";
        }
        else if (passwords == "") {
            RegisterStat.text = "Please enter password first.";
        }
        else if (passwords != verifypassword) {
            RegisterStat.text = "Password verify failed.";
        }
        else {
            using (UnityWebRequest www = UnityWebRequest.Post("https://u-parkprojectgraduation.com/phpfile/RegisterUser.php", form)) {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success) {
                    Debug.Log(www.error);
                }
                else {
                    if (www.downloadHandler.text == "New record created successfully") {
                        RegisterStat.text = "Register Sucess.";
                        RegisterStat.color = Color.green;
                        SceneManager.LoadScene("Login");
                    }
                    else {
                        RegisterStat.text = "User has already exist.";
                    }
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }
    }
}