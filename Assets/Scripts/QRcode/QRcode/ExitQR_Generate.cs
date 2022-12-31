using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;
using TMPro;


public class ExitQR_Generate : MonoBehaviour
{   
    [SerializeField]
    private RawImage rawImageReceiver;
    [SerializeField]
    private TMP_InputField textInputField;

    private Texture2D storeEncodeTexture;
    
    void Start()
    {
        storeEncodeTexture=new Texture2D(256,256);
        EncodeTextToQRcode();
    }

    private Color32 [] Encode(string textForEncoding,int width,int height){
        BarcodeWriter writer = new BarcodeWriter{
            Format = BarcodeFormat.QR_CODE,
            Options=new QrCodeEncodingOptions{
                Height=height,
                Width=width
            }
        };
        return writer.Write(textForEncoding);
    }
    public void OnClickEncode(){
           EncodeTextToQRcode();
    }
    private void EncodeTextToQRcode(){
        string textWrite = "U-Park/Exit/"+LoginSystem.AccountName;
        Color32 [] convertPixelTotexture = Encode(textWrite,storeEncodeTexture.width,storeEncodeTexture.height);
        storeEncodeTexture.SetPixels32(convertPixelTotexture);
        storeEncodeTexture.Apply();
        rawImageReceiver.texture=storeEncodeTexture;
    }
}