 using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
public class Payment : MonoBehaviour
{
    public Text UsernameTitle;
    public Text TotalParkingTimeTitles;
    public Text CarNumberTitle;
    public Text SlotNumberTitle;
    public Text FeeTitle;
    

    void Start()
    {
        StartCoroutine(func_Payment(EnterSlotNum.SlotNum));
    }
    public IEnumerator func_Payment(string SlotNum)
    {
        WWWForm form = new WWWForm();
        form.AddField("SlotNum", SlotNum);
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ParkingLot/Payment.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
              string s = www.downloadHandler.text;
              string[] subs = s.Split(' ');
              string[] YearAndMonthStart=subs[3].Split('-');
              string[] TimeStart=subs[4].Split(':');
              string[] YearAndMonthEnd=new string [5];
              string[] TimeEnd=new string [5];
             
            
              String str_start = subs[3]+" "+subs[4];
              String str_end =DateTime.Now.ToString();
              DateTime dt_start = Convert.ToDateTime(str_start);
              DateTime dt_end = Convert.ToDateTime(str_end);
              TimeSpan ts = dt_end - dt_start;

              
              int temp=(int)ts.TotalHours;
              string totalHours = "Total Parked time: "+temp.ToString()+" hours";
              int FeeHours=(int)ts.TotalHours;
              int FeeTemp=FeeHours*40;
              string FinalPrice=FeeTemp.ToString();


              UsernameTitle.text=LoginSystem.Username;
              TotalParkingTimeTitles.text=totalHours;
              CarNumberTitle.text=LoginSystem.CarNum;
              SlotNumberTitle.text="Slot number:"+EnterSlotNum.SlotNum;
              FeeTitle.text="Fee:  "+FinalPrice+" dollars";
              
            }
        }
    }
}
