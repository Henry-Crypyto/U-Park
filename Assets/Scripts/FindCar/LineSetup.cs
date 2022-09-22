using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LineSetup : MonoBehaviour
{
    public Button ArrowShow;
    public GameObject Line;
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
     void Start(){
         ArrowShow.onClick.AddListener(() => {
            line.SetUpLine(points);
            // Line.SetActive(true);
        });
    }
}