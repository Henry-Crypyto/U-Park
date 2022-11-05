// https://streakbyte.com/unity-ui-input-field-password-show-hide/

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowPassword : MonoBehaviour 
{
    public TMPro.TMP_InputField PasswordInputField;
        
    public void ToggleContentType()
    {
        if (this.PasswordInputField.contentType == TMPro.TMP_InputField.ContentType.Password)
        {
            this.PasswordInputField.contentType = TMPro.TMP_InputField.ContentType.Standard;
        }
        else
        {
            this.PasswordInputField.contentType = TMPro.TMP_InputField.ContentType.Password;
        }
        
        this.PasswordInputField.ForceLabelUpdate();
    }
}
