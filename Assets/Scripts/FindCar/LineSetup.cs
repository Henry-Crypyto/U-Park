using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LineSetup : MonoBehaviour
{
    public Button ArrowShow;
    public GameObject Line;
    public TMPro.TMP_Text RemindMessage;
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
    
    void Update() {
        if(GetSlotNum.SlotNumber==0){
                RemindMessage.text="EnterSlotNum first!!";
            }
        ArrowShow.onClick.AddListener(() => {
            if(GetSlotNum.SlotNumber==0){
                RemindMessage.text="EnterSlotNum first!!";
            }
            else{line.SetUpLine(points);}
            // Line.SetActive(true);
        });
    }
}