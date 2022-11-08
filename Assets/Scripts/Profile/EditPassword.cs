using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditPassword : MonoBehaviour
{
    public TMPro.TMP_InputField OldPasswordInputField;
    public TMPro.TMP_InputField NewPasswordInputField;
    public TMPro.TMP_InputField VerrifyPasswordInputField;

    public TMPro.TMP_InputField ProfileUIPasswordInputField;

    public TMPro.TMP_Text EditStat;

    public Button EditPasswordConfirmButton;
    public GameObject EditPasswordUI;
    public GameObject EditProfileUI;

    void Start()
    {
        EditPasswordConfirmButton.onClick.AddListener(() => {
            func_EdirPassword(OldPasswordInputField.text, NewPasswordInputField.text, VerrifyPasswordInputField.text);
        });
    }
    public void func_EdirPassword(string oldpass, string newpass, string verifypass)
    {
        if (OldPasswordInputField.text == "" || VerrifyPasswordInputField.text == "" || NewPasswordInputField.text == "") {
            EditStat.text = "Please fill all the blank";
        }
        else if (oldpass != Profile.Password) {
            EditStat.text = "Your old password is wrong.";
        }
        else if (oldpass == newpass) {
            EditStat.text = "Can't use same password.";
        }
        else if (newpass != verifypass) {
            EditStat.text = "Verify password wrong.";
        }
        else {
            ProfileUIPasswordInputField.text = VerrifyPasswordInputField.text;
            EditPasswordUI.SetActive(false);
            EditProfileUI.SetActive(true);
        }
    }
}