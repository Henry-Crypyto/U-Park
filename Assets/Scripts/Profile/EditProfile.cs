using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditProfile : MonoBehaviour
{
    public TMPro.TMP_InputField UserNameInputField;
    public TMPro.TMP_InputField PasswordInputField;
    public TMPro.TMP_InputField PhoneNumberInputField;
    public TMPro.TMP_InputField CarNumberInputField;
    public TMPro.TMP_InputField CreditCardInputField;

    public TMPro.TMP_Text EditStat;
    
    public Button EditPasswordButton;
    public Button ConfirmButton;
    public GameObject EditPasswordUI;
    public GameObject EditProfileUI;

    void Start()
    {
        UserNameInputField.text = Profile.Username;
        PasswordInputField.text = Profile.Password;
        CarNumberInputField.text = Profile.CarNum;
        CreditCardInputField.text = Profile.CreditCard;
        PhoneNumberInputField.text = Profile.PhoneNumber;

        ConfirmButton.onClick.AddListener(() => {
            StartCoroutine(func_EditProfile(LoginSystem.AccountName, UserNameInputField.text, PasswordInputField.text, CarNumberInputField.text, CreditCardInputField.text, PhoneNumberInputField.text));
        });
        EditPasswordButton.onClick.AddListener(() => {
            EditPasswordUI.SetActive(true);
            EditProfileUI.SetActive(false);
        });
    }

    public IEnumerator func_EditProfile(string Account, string username, string passwords, string CarNumber, string CreditCardNumber, string PhoneNumber)
    {
        if (UserNameInputField.text == "" || PasswordInputField.text == "" || CarNumberInputField.text == "" || CreditCardInputField.text == "" || PhoneNumberInputField.text == "") {
            EditStat.text = "Please fill all the blank.";
        }
        else {
            WWWForm form = new WWWForm();
            form.AddField("loginAccount", Account);
            form.AddField("loginUser", username);
            form.AddField("loginPass", passwords);
            form.AddField("loginCarNum", CarNumber);
            form.AddField("loginCreditCardNum", CreditCardNumber);
            form.AddField("loginPhoneNum", PhoneNumber);

            using (UnityWebRequest www = UnityWebRequest.Post("https://u-parkprojectgraduation.com/phpfile/EditProfile.php", form)) {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success) {
                    Debug.Log(www.error);
                }
                else {
                    if (www.downloadHandler.text == "Profile edit successfully.") {
                        SceneManager.LoadScene("Profile");
                    }
                    else {
                        Debug.Log(www.downloadHandler.text);
                    }
                }
            }
        }
    }
}