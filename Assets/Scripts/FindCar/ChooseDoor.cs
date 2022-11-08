using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseDoor : MonoBehaviour
{
    public static int DoorNumber;
    public TMPro.TMP_Dropdown myDrop;
    public Button ArrowShow;
    public void ChooseDoorNumber()
    {
        if (myDrop.value == 2) {
            DoorNumber = 0;
        }
        else if (myDrop.value == 3) {
            DoorNumber = 98;
        }
        else if (myDrop.value == 4) {
            DoorNumber = 99;
        }
        else if (myDrop.value == 1) {
            print("我好帥1");
        }
        else {
            print("我好帥0");
        }
    }
}