// https://ithelp.ithome.com.tw/articles/10186933

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Button
using UnityEngine.SceneManagement; //SceneManager

public class ButtonChangeScene : MonoBehaviour 
{
    public string sceneName; //要載入的Scene
    
    void Start()
    {
		//為按鈕加入On Click事件
        GetComponent<Button> ().onClick.AddListener(() => {
            ClickEvent(sceneName);
        });
    }
    
    void ClickEvent(string sceneName)
    {
		//切換Scene
        SceneManager.LoadScene (sceneName);
    }
}