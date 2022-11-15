using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class EmptySlot_A03 : MonoBehaviour
{
    SpriteRenderer sprite;
    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(FindEmptySlot("A03", "1"));
    }


    IEnumerator FindEmptySlot(string username, string occupied)
    {

        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("slotOccupied", occupied);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ParkingLot/Find_empty_slot.php", form)) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                string A = www.downloadHandler.text;
                string C = "比對成功啦";

                if (string.Equals(A, C)) {
                    sprite.color = Color.red;

                }
                else {
                    sprite.color = Color.green;

                }
            }
        }
    }
}


  
