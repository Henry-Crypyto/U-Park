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
    public Button OKButton;
    public Button NoticeConfirmButton;
    public Button ScanButton;
    public TMPro.TMP_Text ConfirmMessage;
    public TMPro.TMP_Text NoticeMessage;
    public TMPro.TMP_InputField SlotNumInputField;
    public GameObject ConfirmWindow;
    public GameObject NoticeWindow;
    public GameObject RegisterStatWindow;
    public static string SlotNum="";
    public TMPro.TMP_Text RegisterStat;

    void Start()
    {
        
        CheckQRcode();
        if(SlotNum!=""){
                ScanButton.enabled = false;
                NoticeWindow.SetActive(true);
                NoticeMessage.text = "Your registered slot number is"+" "+SlotNum;
                NoticeConfirmButton.onClick.AddListener(() => {
                    NoticeWindow.SetActive(false);
                 });
        }else{
            NoticeConfirmButton.onClick.AddListener(() => {
                    RegisterStatWindow.SetActive(false);
            });
            EnterButton.onClick.AddListener(() => {
            if (SlotNumInputField.text == "") {
                RegisterStatWindow.SetActive(true);
                RegisterStat.text = "Please enter slot number first!!!";
            }
            else {
                ConfirmWindow.SetActive(true);
                ConfirmMessage.text = "Your slot number is " + SlotNumInputField.text + "\nIs it correct?";
            }
        });

        ConfirmButton.onClick.AddListener(() => {
            RegisterStatWindow.SetActive(true);
            ConfirmWindow.SetActive(false);
            StartCoroutine(func_EnterSlotNum(SlotNumInputField.text,LoginSystem.AccountName));
        });

        OKButton.onClick.AddListener(() => {
            RegisterStat.text="";
            RegisterStatWindow.SetActive(false);
        });

        CancelButton.onClick.AddListener(() => {
            ConfirmWindow.SetActive(false);
        });
      } 
    }
    void Update(){
            if(SlotNum!=""){
                 EnterButton.enabled = false;
                 ScanButton.enabled = false;
                 SlotNumInputField.DeactivateInputField();
            }
    }

    public IEnumerator func_EnterSlotNum(string SlotNumInput,string Account)
    {
        string UpperSlotNumInput = SlotNumInput.ToUpper();
        WWWForm form = new WWWForm();
        form.AddField("SlotNum", UpperSlotNumInput);
        form.AddField("Account", Account);
        using (UnityWebRequest www = UnityWebRequest.Post("http://u-parkprojectgraduation.com/phpfile/EnterSlotNum.php", form)) {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                if (www.downloadHandler.text == "Register succesfully!!") {
                    SlotNum = UpperSlotNumInput;
                }
                RegisterStat.text = www.downloadHandler.text;
            }
        }
    }

    public void CheckQRcode(){
       if(QRcode.QRcodeControlBit==1&&QRcode.QRcodeSlotNum!=null){
            ConfirmWindow.SetActive(true);
            ConfirmMessage.text = "Your slot number is " + QRcode.QRcodeSlotNum + "\nIs it correct?";
            SlotNumInputField.text=QRcode.QRcodeSlotNum;
            QRcode.QRcodeControlBit=0;
        }
    }
}
