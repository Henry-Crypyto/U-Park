using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
public class EnterQR_Scan : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImageBackGround;
    [SerializeField]
    private AspectRatioFitter aspectRatioFitter;
    [SerializeField]
    private TMPro.TMP_Text textOut;
    [SerializeField]
    private RectTransform scanZone;
    public static string QRcodeSlotNum;
    public static int QRcodeControlBit;
    private bool CameraStat;
    private WebCamTexture cameratexture;
    float time;
    float timeDelay;
    string Final;
    
    void Start()
    {
        time = 0f;
        timeDelay = 0.3f;
        SetUpCamera();
    }

    
    void Update()
    {
        UpdateCameraRender();
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay) {
            time = 0f;
        Scan();
        }
    }
    
    private void SetUpCamera(){
        WebCamDevice[] devices=WebCamTexture.devices;
        if(devices.Length==0){
            CameraStat=false;
            return;
        }
        for(int i=0;i<devices.Length;i++){
            if(devices[i].isFrontFacing==false){
                cameratexture=new WebCamTexture(devices[i].name,(int)scanZone.rect.width,(int)scanZone.rect.height);
            }
        }
        cameratexture.Play();
        rawImageBackGround.texture=cameratexture;
        CameraStat=true;
    }

    private void UpdateCameraRender(){
        if(CameraStat==false){
            return;
        }
        float ratio=(float)cameratexture.width/(float)cameratexture.height;
        aspectRatioFitter.aspectRatio=ratio;

        int orientation=-cameratexture.videoRotationAngle;
        rawImageBackGround.rectTransform.localEulerAngles=new Vector3(0,0,orientation);
    }

   

    void GetCurrentTime(string sceneName)
    {
        //切換Scene
        SceneManager.LoadScene(sceneName);
    }

    private void Scan(){
        
             IBarcodeReader barcodeReader =new BarcodeReader();
             Result result=barcodeReader.Decode(cameratexture.GetPixels32(),cameratexture.width,cameratexture.height);

             string UParkSerialText="U-Park";
             string[] QRtext = result.Text.Split('/');
             string QRSerialText= QRtext[0];
             string Accountname= QRtext[1];
             if(result!=null&& QRSerialText==UParkSerialText){
                string Timelater = DateTime.Now.ToString();
                string[] test = Timelater.Split(' ');
                string[] test2 = test[0].Split('/');
                string Final=test2[2]+"-"+test2[1]+"-"+test2[0]+" "+test[1];
                textOut.text=Final;
                StartCoroutine(PostEntryTime(Accountname,Final));
             }
             else{
                textOut.text="Failed to read QRcode";
             }
        }
    

    public IEnumerator PostEntryTime(string Account,string Entrytime)
    {
        WWWForm form = new WWWForm();
        form.AddField("Account", Account);
        form.AddField("Entrytime", Entrytime);

        using (UnityWebRequest www = UnityWebRequest.Post("http://u-parkprojectgraduation.com/phpfile/EntryTime.php", form)) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                string s = www.downloadHandler.text;
            }
        }
    }
}
        // string TimeNow = DateTime.Now.ToString();
        // string[] test = TimeNow.Split('下');
        // string Years=test[0];
        // string[] test2 = TimeNow.Split('午');
        // string hourToseconds=test2[1];
        // string FinalTime=Years+hourToseconds;
        // string test1=FinalTime.Replace("/", "-");
        // test1 = Regex.Replace(test1, "[@,\\.\";'\\\\ ]", "}");
        // string[] test3=test1.Split('}');
        // Final=test3[0]+" "+test3[2];
        // textOut.text=Final;