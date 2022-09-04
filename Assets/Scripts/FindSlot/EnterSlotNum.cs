using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterSlotNum : MonoBehaviour
{
    public Button EnterButton;
    public Button ConfirmButton;
    public Button CancelButton;
    public Text ConfirmMessage;
    public InputField SlotNumInputField;
    public GameObject ConfirmWindow;

    public static string SlotNum;
    public Text RegisterStat;

    void Start()
    {
        EnterButton.onClick.AddListener(() => {
            ConfirmWindow.SetActive(true);
            ConfirmMessage.text = "Your slot number is " + SlotNumInputField.text + "\nIs it correct?";
        });

        ConfirmButton.onClick.AddListener(() => {
            ConfirmWindow.SetActive(false);
            StartCoroutine(func_EnterSlotNum(SlotNumInputField.text));
        });

        CancelButton.onClick.AddListener(() => {
            ConfirmWindow.SetActive(false);
        });
    }

    public IEnumerator func_EnterSlotNum(string SlotNumInput)
    {
        WWWForm form = new WWWForm();
        form.AddField("SlotNum", SlotNumInput);
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ParkingLot/EnterSlotNum.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                if(www.downloadHandler.text == "Register succesfully!!"){
                    SlotNum = SlotNumInput;
            }
            RegisterStat.text = www.downloadHandler.text;
            }
        }
    }
}
