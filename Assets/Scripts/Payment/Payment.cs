using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
public class Payment : MonoBehaviour
{
    public TMPro.TMP_Text TotalParkingTimeTitles;
    public TMPro.TMP_Text CarNumberTitle;
    public TMPro.TMP_Text SlotNumberTitle;
    public TMPro.TMP_Text FeeTitle;
    public TMPro.TMP_Text ParkingStartTimeTitles;
    public TMPro.TMP_Text RemindMessage;

    public GameObject ConfirmWindow;
    public GameObject CompleteWindow;
    public TMPro.TMP_Text ConfirmMessage;
    public TMPro.TMP_Text CompleteMessage;
    public Button PayButton;
    public Button ConfirmButton;
    public Button CancelButton;
    public Button OKButton;
    int Valuation = 40;
    string FinalPrice;

    void Start()
    {
        if (EnterSlotNum.SlotNum == null) {
            RemindMessage.text = "Please enter slot number first.";
        }
        else {
            StartCoroutine(ShowPaymentStat(EnterSlotNum.SlotNum));
        }
        
         PayButton.onClick.AddListener(() => {
            ConfirmWindow.SetActive(true);
            ConfirmMessage.text = "$ "+FinalPrice+" dollars";
        });
    
        ConfirmButton.onClick.AddListener(() => {
            ConfirmWindow.SetActive(false);
            CompleteWindow.SetActive(true);
            CompleteMessage.text="Payment Complete!!\n\nHave a nice day";
        });

        CancelButton.onClick.AddListener(() => {
            ConfirmWindow.SetActive(false);
        });

        OKButton.onClick.AddListener(() => {
             CompleteWindow.SetActive(false);
        });
        //StartCoroutine(ShowPaymentStat("A01"));
    }

    public IEnumerator ShowPaymentStat(string SlotNum)
    {
        WWWForm form = new WWWForm();
        form.AddField("SlotNum", SlotNum);
        using (UnityWebRequest www = UnityWebRequest.Post("http://u-parkprojectgraduation.com/phpfile/Payment.php", form)) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                string s = www.downloadHandler.text;
                string[] subs = s.Split(' ');

                String str_start = subs[3] + " " + subs[4];
                String str_end = DateTime.Now.ToString();
                DateTime dt_start = Convert.ToDateTime(str_start);
                DateTime dt_end = Convert.ToDateTime(str_end);
                TimeSpan ts = dt_end - dt_start;

                int tempMinutes = (int)ts.Minutes;
                int tempHours = (int)ts.TotalHours;
                int tempSeconds = (int)ts.Seconds;
                string totalHours = tempHours.ToString() + ":" + tempMinutes.ToString() + ":" + tempSeconds.ToString();

                int FeeHours = (int)ts.TotalHours;
                int FeeTemp = FeeHours * Valuation;
                FinalPrice = FeeTemp.ToString();


                TotalParkingTimeTitles.text = totalHours;
                CarNumberTitle.text = Profile.CarNum;
                SlotNumberTitle.text = "1st Floor - " + EnterSlotNum.SlotNum;
                ParkingStartTimeTitles.text = "From " + str_start;
                FeeTitle.text = "$ " + FinalPrice;
            }
        }
    }
}

            //不要刪除下面這些
            //   
            //   TotalParkingTimeTitles.text=totalHours;
            //   CarNumberTitle.text=LoginSystem.CarNum;
            //   SlotNumberTitle.text="Slot number:"+EnterSlotNum.SlotNum;
            //   FeeTitle.text="Fee:  "+FinalPrice+" dollars";