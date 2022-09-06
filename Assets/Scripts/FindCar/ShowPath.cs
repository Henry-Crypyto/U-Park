using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowPath : MonoBehaviour
{
    
    public Button ShowPathButton;
    public GameObject Line;
    void Start()
    {
        ShowPathButton.onClick.AddListener(() => {
            Line.SetActive(true);
        });
    }

    
}
