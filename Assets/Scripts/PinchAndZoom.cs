using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchAndZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float xAxisLimit = 10;
    public float yAxisLimit = 0;
    public float zoomInMin = 7;
    public float zoomOutMax = 19;
    
    float multiple;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2) {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            
            float difference = currentMagnitude - prevMagnitude;

            
            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0)) {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel") * 3);
        
        Camera.main.transform.position = new Vector3(
            Mathf.Clamp(Camera.main.transform.position.x, -xAxisLimit - (float)(-0.5 * multiple + 10), xAxisLimit + (float)(-0.5 * multiple + 10)),
            Mathf.Clamp(Camera.main.transform.position.y, -yAxisLimit - (float)(-0.52 * multiple + 10), yAxisLimit + (float)(-0.52 * multiple + 10)),
            Camera.main.transform.position.z
        );
    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomInMin, zoomOutMax);
        multiple = Camera.main.orthographicSize - increment;
    }
}

//左上:-18,11.8
//右上:20,11.8
//右下:20,-11.8