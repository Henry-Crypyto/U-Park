using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using TMPro;
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
    
    
    void Start()
    {
        SetUpCamera();
    }

    
    void Update()
    {
        UpdateCameraRender();
        Scan();
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

    public void OnClickScan(){
        Scan();
    }

    void ClickEvent(string sceneName)
    {
        //切換Scene
        SceneManager.LoadScene(sceneName);
    }

    private void Scan(){
        string UParkSerialText="U-Park";
        try{
             IBarcodeReader barcodeReader =new BarcodeReader();
             Result result=barcodeReader.Decode(cameratexture.GetPixels32(),cameratexture.width,cameratexture.height);
             string[] QRtext = result.Text.Split('/');
             string QRSerialText= QRtext[0];
             string Accountname= QRtext[1];
             if(result!=null&&UParkSerialText==QRSerialText){
                textOut.text=result.Text;
                String TimeNow = DateTime.Now.ToString();
                PostEntryTime(Accountname,TimeNow);
                ClickEvent("FindSLot");
             }
             else{
                textOut.text="Failed to read QRcode";
             }
        }
        catch{
            textOut.text="Failed in try";
        }
    }

    public IEnumerator PostEntryTime(string Account,DateTime Entrytime)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginAccount", Account);
        form.AddField("Entrytime", Entrytime);


        using (UnityWebRequest www = UnityWebRequest.Post("http://u-parkprojectgraduation.com/phpfile/EntryTime.php", form)) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                
                string s = www.downloadHandler.text;
                Debug.Log(s);
            }
        }
    }
}