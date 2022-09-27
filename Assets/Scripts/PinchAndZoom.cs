using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchAndZoom : MonoBehaviour {
    Vector3 touchStart;
    [SerializeField]float leftLimit;
    [SerializeField]float rightLimit;
    [SerializeField]float bottomLimit;
    [SerializeField]float topLimit;


    public int zoomOutMin=19;
    public int zoomOutMax=40;
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)){
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if(Input.touchCount == 2){
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }else if(Input.GetMouseButton(0)){
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
        Camera.main.transform.position=new Vector3(
            Mathf.Clamp(Camera.main.transform.position.x,leftLimit,rightLimit),
            Mathf.Clamp(Camera.main.transform.position.y,bottomLimit,topLimit),
            Camera.main.transform.position.z

        );
	}

    void zoom(float increment){
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

}
//左上:-18,11.8
//右上:20,11.8
//右下:20,-11.8