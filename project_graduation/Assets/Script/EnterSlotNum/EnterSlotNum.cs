using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterSlotNum : MonoBehaviour
{
    public Button ChangeToEnterSlotNumUI;
    public Button Confirm;
    public Button Return;
    public Button ChangeToConfirmSlotNumUI;
    public Button Cancel;
    public InputField EnterSlotNumInput;
    public GameObject FindEmptySlotUI;
    public GameObject EnterSlotNumUI;
    public GameObject ConfirmSlotNumUI;
    public static string SlotNum;
    public Text RegisterStat;
    public Text ConfirmMsg;
    void Start()
    {
        ChangeToEnterSlotNumUI.onClick.AddListener(()=>{
        FindEmptySlotUI.SetActive(false);
        EnterSlotNumUI.SetActive(true);
        });

        Confirm.onClick.AddListener(()=>{
        ConfirmMsg.text="";
        StartCoroutine(func_EnterSlotNum(EnterSlotNumInput.text));
        EnterSlotNumUI.SetActive(true);
        ConfirmSlotNumUI.SetActive(false);
        });
          
        Return.onClick.AddListener(()=>{
        FindEmptySlotUI.SetActive(true);
        EnterSlotNumUI.SetActive(false);
        });

        ChangeToConfirmSlotNumUI.onClick.AddListener(()=>{
        EnterSlotNumUI.SetActive(false);
        ConfirmSlotNumUI.SetActive(true);
        ConfirmMsg.text="Are you sure?\n\n\nYour slot number is  "+EnterSlotNumInput.text;
        });

        Cancel.onClick.AddListener(()=>{
        EnterSlotNumUI.SetActive(true);
        ConfirmSlotNumUI.SetActive(false);
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
              if(www.downloadHandler.text=="Register succesfully!!"){
                      SlotNum=SlotNumInput;
              }
              RegisterStat.text=www.downloadHandler.text;
            }
        }
        
    }
    
}
