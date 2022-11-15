using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditPassword : MonoBehaviour
{
    public TMPro.TMP_InputField OldPasswordInputField;
    public TMPro.TMP_InputField NewPasswordInputField;
    public TMPro.TMP_InputField VerifyPasswordInputField;

    public TMPro.TMP_InputField ProfileUIPasswordInputField;

    public TMPro.TMP_Text EditStat;

    public Button ConfirmButton;
    public Button CancelButton;
    public GameObject EditPasswordUI;
    public GameObject EditProfileUI;

    void Start()
    {
        ConfirmButton.onClick.AddListener(() => {
            func_EditPassword(OldPasswordInputField.text, NewPasswordInputField.text, VerifyPasswordInputField.text);
        });
        CancelButton.onClick.AddListener(() => {
            EditPasswordUI.SetActive(false);
            EditProfileUI.SetActive(true);
        });
    }
    
    public void func_EditPassword(string oldpass, string newpass, string verifypass)
    {
        if (OldPasswordInputField.text == "" || NewPasswordInputField.text == "" || VerifyPasswordInputField.text == "") {
            EditStat.text = "Please fill all the blank";
        }
        else if (oldpass != Profile.Password) {
            EditStat.text = "The old password is wrong.";
        }
        else if (oldpass == newpass) {
            EditStat.text = "Can't use same password.";
        }
        else if (newpass != verifypass) {
            EditStat.text = "Verify password wrong.";
        }
        else {
            ProfileUIPasswordInputField.text = VerifyPasswordInputField.text;
            EditPasswordUI.SetActive(false);
            EditProfileUI.SetActive(true);
        }
    }
}