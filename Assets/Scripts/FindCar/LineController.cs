// https://www.youtube.com/watch?v=5ZBynjAsfwI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Transform[] points;
    Dijkstra p;
    public GameObject a;
    int i=13,k=0;
    int[] q = new int[5];
    
    private void Awake(){
        lr = GetComponent<LineRenderer>();
    }
    public void SetUpLine(Transform[] points){
        lr.positionCount = points.Length-1;
        this.points=points;
        // print("點的數量"+points.Length);
        p = a.GetComponent<Dijkstra>();
        // print("我是線"+p.getpi(23));
        lr.SetPosition(k,points[i].position);
        k++;
        lr.SetPosition(k,points[i+45].position);
        while(0!=p.getpi(i))
        {
            ++k;
            lr.SetPosition(k,points[p.getpi(i)].position);
            // print("我進來了"+p.getpi(i));
            i=p.getpi(i);            
        };
        k=k+1;
        lr.SetPosition(k,points[0].position);
        for(int u=0;u<97-k;u++)
        {
            lr.SetPosition(k+u,points[0].position);
        }
    }
}

/*  private void Update() {
        p = a.GetComponent<Dijkstra>();
        // print("我是線"+p.getpi(23));
        lr.SetPosition(k,points[i].position);
        while(0!=p.getpi(i)){
            ++k;
            lr.SetPosition(k,points[p.getpi(i)].position);
            print("我進來了"+p.getpi(i));
            i=p.getpi(i);            
        }
        lr.SetPosition(k+1,points[0].position);        
    }

    private void Update() {
        for(int i=0;i<points.Length;i++){
            lr.SetPosition(i,points[i].position);
        }
       
    }*/