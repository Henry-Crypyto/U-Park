using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Profile : MonoBehaviour
{
      
      public Text Username;
      public Text Password;
      public Text CarNum;
      public Text CreditCard;

    void Start()
    {
        string Username=LoginSystem.UsernameTemp;
        StartCoroutine(GetUserProfile(Username)); 
        
    }
    IEnumerator GetUserProfile(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ParkingLot/Profile.php", form))
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
              Username.text=subs[0];
               Password.text=subs[1];
               CarNum.text=subs[2];
               CreditCard.text=subs[3];
            }
        }
    }
    
   
}
