using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FindEmptySlot : MonoBehaviour
{
    SpriteRenderer sprite;
    float time;
    float timeDelay;
    // Start is called before the first frame update
    public void Start()
    {
        time = 0f;
        timeDelay = 0.6f;
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay) {
            time = 0f;
            StartCoroutine(ChangeSlotColor());
        }
    }


    IEnumerator ChangeSlotColor()
    {
        WWWForm form = new WWWForm();
        using (UnityWebRequest www = UnityWebRequest.Post("http://u-parkprojectgraduation.com/phpfile/FindEmptySlot.php", form)) {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                string AllParkingSlotStat = www.downloadHandler.text;
                string[] subs = AllParkingSlotStat.Split('/');
                string[] SlotNum = new string[subs.Length-1];
                string[] Occupied=new string[subs.Length-1];
                for(int i=0;i<subs.Length-1;i++){
                    string[] temp =subs[i].Split(':');
                    SlotNum[i]=temp[0];
                    Occupied[i]=temp[1];
                    if(Occupied[i]=="0"){
                    GameObject[] objects = GameObject.FindGameObjectsWithTag("SlotSprite");
                    foreach (GameObject go in objects) {
                        if (go.name == SlotNum[i]) {
                            SpriteRenderer[] renderers = go.GetComponents<SpriteRenderer>();
                            renderers[0].color = Color.green;
                            }
                        }
                    }
                    else{
                    GameObject[] objects = GameObject.FindGameObjectsWithTag("SlotSprite");
                    foreach (GameObject go in objects) {
                        if (go.name == SlotNum[i]) {
                            SpriteRenderer[] renderers = go.GetComponents<SpriteRenderer>();
                            renderers[0].color = Color.red;
                            }
                        }
                    }

                    }
                }
            }
        }   
}
