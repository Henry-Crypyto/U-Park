using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QRcode : MonoBehaviour
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
        try{
             IBarcodeReader barcodeReader =new BarcodeReader();
             Result result=barcodeReader.Decode(cameratexture.GetPixels32(),cameratexture.width,cameratexture.height);
             if(result!=null){
                textOut.text=result.Text;
                QRcodeSlotNum=result.Text;
                QRcodeControlBit=1;
                ClickEvent("FindSLot");
             }
             else{
                QRcodeControlBit=0;
                textOut.text="Failed to read QRcode";
             }
        }
        catch{
            QRcodeControlBit=0;
            textOut.text="Failed in try";
        }
    }
}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using ZXing;
// using TMPro;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;
// public class QRcode : MonoBehaviour
// {
//     [SerializeField]
//     private RawImage rawImageBackGround;
//     [SerializeField]
//     private AspectRatioFitter aspectRatioFitter;
//     [SerializeField]
//     public static TMPro.TMP_Text QRcodeSlotNum;
//     [SerializeField]
//     private RectTransform scanZone;
//     [SerializeField]
//     public static TMPro.TMP_Text QRcodeStat;

//     private bool CameraStat;
//     private WebCamTexture cameratexture;
    
    
//     void Start()
//     {
//         SetUpCamera();
//     }

    
//     void Update()
//     {
//         UpdateCameraRender();
//         Scan();
//     }
    
//     private void SetUpCamera(){
//         WebCamDevice[] devices=WebCamTexture.devices;
//         if(devices.Length==0){
//             CameraStat=false;
//             return;
//         }
//         for(int i=0;i<devices.Length;i++){
//             if(devices[i].isFrontFacing==false){
//                 cameratexture=new WebCamTexture(devices[i].name,(int)scanZone.rect.width,(int)scanZone.rect.height);
//             }
//         }
//         cameratexture.Play();
//         rawImageBackGround.texture=cameratexture;
//         CameraStat=true;
//     }

//     private void UpdateCameraRender(){
//         if(CameraStat==false){
//             return;
//         }
//         float ratio=(float)cameratexture.width/(float)cameratexture.height;
//         aspectRatioFitter.aspectRatio=ratio;

//         int orientation=-cameratexture.videoRotationAngle;
//         rawImageBackGround.rectTransform.localEulerAngles=new Vector3(0,0,orientation);
//     }

//     public void OnClickScan(){
//         Scan();
//     }
//      void ClickEvent(string sceneName)
//     {
//         //切換Scene
//         SceneManager.LoadScene(sceneName);
//     }
//     private void Scan(){
//         try{
//              IBarcodeReader barcodeReader =new BarcodeReader();
//              Result result=barcodeReader.Decode(cameratexture.GetPixels32(),cameratexture.width,cameratexture.height);
//              if(result!=null){
//                 QRcodeSlotNum.text=result.Text;
//                 ClickEvent("FindSlot");
//              }
//              else{
//                 QRcodeStat.text="Failed to read QRcode";
//              }
//         }
//         catch{
//             Debug.Log("QRcode fail!");
//         }
//     }
// }
