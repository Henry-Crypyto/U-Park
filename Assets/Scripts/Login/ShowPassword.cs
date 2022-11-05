using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowPassword : MonoBehaviour 
{
    public Button ShowPasswordButton;
    public TMPro.TMP_InputField PasswordInputField;
    int a=1;
    void Start()
    {
        ShowPasswordButton.onClick.AddListener(() => { 
            this.PasswordInputField.contentType= TMPro.TMP_InputField.ContentType.Standard;
        });
    }


}
