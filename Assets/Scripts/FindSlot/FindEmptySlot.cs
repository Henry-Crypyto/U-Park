using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class  FindEmptySlot : MonoBehaviour
{
    SpriteRenderer sprite;
    // Start is called before the first frame update
    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        
        StartCoroutine(ChangeSlotColor("A01","1"));
        StartCoroutine(ChangeSlotColor("A02","1"));
        StartCoroutine(ChangeSlotColor("A03","1"));
        StartCoroutine(ChangeSlotColor("A04","1"));
        StartCoroutine(ChangeSlotColor("A05","1"));
        StartCoroutine(ChangeSlotColor("A06","1"));
        StartCoroutine(ChangeSlotColor("A07","1"));
        StartCoroutine(ChangeSlotColor("A08","1"));
        StartCoroutine(ChangeSlotColor("A09","1"));
        // StartCoroutine(ChangeSlotColor("A10","1"));
        // StartCoroutine(ChangeSlotColor("A11","1"));
        // StartCoroutine(ChangeSlotColor("A12","1"));
        // StartCoroutine(ChangeSlotColor("A13","1"));
        // StartCoroutine(ChangeSlotColor("A14","1"));
        // StartCoroutine(ChangeSlotColor("A15","1"));
        // StartCoroutine(ChangeSlotColor("A16","1"));
        // StartCoroutine(ChangeSlotColor("A17","1"));
        // StartCoroutine(ChangeSlotColor("A18","1"));
        // StartCoroutine(ChangeSlotColor("A19","1"));
        // StartCoroutine(ChangeSlotColor("A20","1"));
        // StartCoroutine(ChangeSlotColor("A21","1"));
        // StartCoroutine(ChangeSlotColor("A22","1"));
        // StartCoroutine(ChangeSlotColor("A23","1"));
        
        // StartCoroutine(ChangeSlotColor("B01","1"));
        // StartCoroutine(ChangeSlotColor("B02","1"));
        // StartCoroutine(ChangeSlotColor("B03","1"));
        // StartCoroutine(ChangeSlotColor("B04","1"));
        // StartCoroutine(ChangeSlotColor("B05","1"));
        // StartCoroutine(ChangeSlotColor("B06","1"));
        // StartCoroutine(ChangeSlotColor("B07","1"));
        // StartCoroutine(ChangeSlotColor("B08","1"));
        // StartCoroutine(ChangeSlotColor("B09","1"));
        // StartCoroutine(ChangeSlotColor("B10","1"));
        // StartCoroutine(ChangeSlotColor("B11","1"));
        // StartCoroutine(ChangeSlotColor("B12","1"));
        // StartCoroutine(ChangeSlotColor("B13","1"));
        // StartCoroutine(ChangeSlotColor("B14","1"));
        // StartCoroutine(ChangeSlotColor("B15","1"));
        // StartCoroutine(ChangeSlotColor("B16","1"));
        // StartCoroutine(ChangeSlotColor("B17","1"));
        // StartCoroutine(ChangeSlotColor("B18","1"));
        // StartCoroutine(ChangeSlotColor("B19","1"));
        // StartCoroutine(ChangeSlotColor("B20","1"));
        // StartCoroutine(ChangeSlotColor("B21","1"));
        // StartCoroutine(ChangeSlotColor("B22","1"));
    }

    
     IEnumerator ChangeSlotColor(string username, string occupied)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("slotOccupied", occupied);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ParkingLot/Find_empty_slot.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string A=www.downloadHandler.text;
                string C="比對成功啦";
            
                if(string.Equals(A,C)){
                    GameObject[] objects = GameObject.FindGameObjectsWithTag("SlotSprite");
                    foreach (GameObject go in objects) {
                        if(go.name==username){
                            SpriteRenderer[] renderers = go.GetComponents<SpriteRenderer>();
                            renderers[0].color=Color.red;
                        }
                    } 
                }
                else{
                    GameObject[] objects = GameObject.FindGameObjectsWithTag("SlotSprite");
                    foreach (GameObject go in objects) {
                        if(go.name==username){
                            
                            SpriteRenderer[] renderers = go.GetComponents<SpriteRenderer>();
                            renderers[0].color=Color.green;
                        }
                    } 
                }
            }
        }
    }
}


  
