using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Profile : MonoBehaviour
{
      
      public Text UsernameContent;
      public Text PasswordContent;
      public Text CarNumContent;
      public Text CreditCardContent;

     

    void Start()
    {
        UsernameContent.text=LoginSystem.Username;
        PasswordContent.text=LoginSystem.Password;
        CarNumContent.text=LoginSystem.CarNum;
        CreditCardContent.text=LoginSystem.CreditCard;
        
        
    }
    
    
   
}
