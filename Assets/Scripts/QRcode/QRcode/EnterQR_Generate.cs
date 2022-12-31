using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class EnterQR_Generate : MonoBehaviour
{   
    [SerializeField]
    private RawImage rawImageReceiver;
    [SerializeField]
    private TMP_InputField textInputField;
    public TMPro.TMP_Text textOut;
    public Button OKButton;
    private Texture2D storeEncodeTexture;
    public GameObject ConfirmWindow;
    float time;
    float timeDelay;
    void Start()
    {   
        time = 0f;
        timeDelay = 0.8f;
        storeEncodeTexture=new Texture2D(256,256);
        EncodeTextToQRcode();
        OKButton.onClick.AddListener(() => {
        SceneManager.LoadScene("FindSlot");
        });
    }
    void Update(){
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay) {
            time = 0f;
        StartCoroutine(FetchEntryTime(LoginSystem.AccountName));
        }
        
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
        string textWrite = "U-Park/Enter/"+LoginSystem.AccountName;
        Color32 [] convertPixelTotexture = Encode(textWrite,storeEncodeTexture.width,storeEncodeTexture.height);
        storeEncodeTexture.SetPixels32(convertPixelTotexture);
        storeEncodeTexture.Apply();
        rawImageReceiver.texture=storeEncodeTexture;
    }

    public IEnumerator FetchEntryTime(string Account){
        WWWForm form = new WWWForm();
        form.AddField("Account", Account);

        using (UnityWebRequest www = UnityWebRequest.Post("http://u-parkprojectgraduation.com/phpfile/FetchEntryTime.php", form)) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success){
                Debug.Log(www.error);
            }
            else {
                if(www.downloadHandler.text!="You havn't enter plarkinglot!!!"&&www.downloadHandler.text!="Account does not exists."){
                      ConfirmWindow.SetActive(true);
                      textOut.text=www.downloadHandler.text;
                }
                else{
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }
    }
}