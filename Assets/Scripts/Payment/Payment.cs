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
    int Valuation = 40;

    void Start()
    {
        if (EnterSlotNum.SlotNum == "") {
            RemindMessage.text = "Please enter slot number first.";
        }
        else {
            StartCoroutine(func_Payment(EnterSlotNum.SlotNum));
        }
        //StartCoroutine(func_Payment("A01"));
    }

    public IEnumerator func_Payment(string SlotNum)
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
                string FinalPrice = FeeTemp.ToString();


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