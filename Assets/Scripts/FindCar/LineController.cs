using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Transform[] points;
    Dijkstra_E pE;
    Dijkstra_S pS;
    Dijkstra_W pW;
    public GameObject a,b,c;
    int selectdoor=98; //0是東門
    int nowpoint;
    int k=0;
    int[] q = new int[5];
    
    private void Awake(){
        lr = GetComponent<LineRenderer>();
    }
    public void SetUpLine(Transform[] points)
    {
        int i=GetSlotNum.SlotNumber;
        if(selectdoor!=0&&selectdoor!=98&&
        selectdoor!=99)
        {
            return;
        }
        lr.positionCount = points.Length-1;
        this.points=points;

        pE = a.GetComponent<Dijkstra_E>();
        pS = b.GetComponent<Dijkstra_S>();
        pW = c.GetComponent<Dijkstra_W>();
        lr.SetPosition(k,points[i].position);//把車位加進去
        k++;
        lr.SetPosition(k,points[i+45].position);//車位前面那個點
        if(selectdoor==0)
        {
            nowpoint=pE.getpi(i);
        }
        else if(selectdoor==98)
        {
            nowpoint=pS.getpi(i);
           
        }
        else
        {
            nowpoint=pW.getpi(i);
            
        }
        
        while(0!=nowpoint)
        {
            ++k;
            lr.SetPosition(k,points[nowpoint].position);
            if(selectdoor==0)
            {
                nowpoint=pE.getpi(nowpoint);
                
            }
            else if(selectdoor==98)
            {
                nowpoint=pS.getpi(nowpoint);
                
            }
            else
            {
                nowpoint=pW.getpi(nowpoint);
            }           
        };
        k=k+1;
        if(selectdoor==0)
        {
            lr.SetPosition(k,points[0].position);
        }
        else if(selectdoor==98)
        {
            lr.SetPosition(k,points[98].position);
        }
        else
        {
            lr.SetPosition(k,points[99].position);
        }   
        for(int u=0;u<99-k;u++)
        {
            if(selectdoor==0)
            {
                lr.SetPosition(k+u,points[0].position);
            }
            else if(selectdoor==98)
            {
                lr.SetPosition(k+u,points[98].position);
            }
            else
            {
                lr.SetPosition(k+u,points[99].position);
            } 
        }
            
    }
    
}

/*
    private void Update() {
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
*/

/*
    private void Update() {
       for(int i=0;i<points.Length;i++){
        lr.SetPosition(i,points[i].position);
       }
    }
*/