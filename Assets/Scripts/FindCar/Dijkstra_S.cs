using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Dijkstra_S : MonoBehaviour
{
    public static int cornerNumber = 7;
    public static int CarNumber = 45;
    public static int ParkA = 23;
    public static int ParkB = 22;
    public GameObject[] corner = new GameObject[cornerNumber];
    public GameObject[] carpositionA = new GameObject[ParkA];
    public GameObject[] carpositionB = new GameObject[ParkB];//此兩行可以合併,單純這樣前端比較好加入車子
    private double[,] Cost = new double[cornerNumber + CarNumber + 2, cornerNumber + CarNumber + 2];
    public static int[,] Neighber = new int[cornerNumber + CarNumber + 1, cornerNumber + CarNumber + 1];//前面七個放corner 後面45放車位
    private double[] shortestPath = new double[cornerNumber + CarNumber + 1];
    public static int[,] pi = new int[cornerNumber + CarNumber + 1, cornerNumber + CarNumber + 1];
    public GameObject myself;
    void Start()
    {

        for (int i = 0; i < cornerNumber + CarNumber + 1; i++) {
            for (int j = 0; j < cornerNumber + CarNumber + 1; j++) {
                if (i == j) {
                    Cost[i, j] = 0;
                    Neighber[i, j] = 1;
                }
                else {
                    Cost[i, j] = 9007199254740992.0;//將所有花費設成最大值
                    Neighber[i, j] = 0;
                }
            }
        }
        Cost[0, cornerNumber + CarNumber + 1] = 9007199254740992.0;
        initial();
        initial_cost();
        dijkstra_FindSSSP();
        dijkstra_FindSSSP();
        // print("我進來了");
        // getpi(33);
        // for(int i=0;i<53;i++)
        // {
        //     print("東門透過"+pi[0,i]+"到"+i);
        // }
    }
    void Update()
    {

    }
    public int getpi(int k)
    {
        return pi[0, k];
    }
    public void dijkstra_FindSSSP()
    {
        double[] S = new double[cornerNumber + CarNumber + 1];//1為S set , 0為Q set 
        int flag = 1;//Q set是否還有
        S[0] = 1;
        for (int i = 1; i < cornerNumber + CarNumber + 1; i++) {
            S[i] = 0;
        }
        while (flag == 1) {
            int min = cornerNumber + CarNumber + 1;//當初創陣列保留的最後一位(double的最大值)
            for (int i = 1; i < cornerNumber + CarNumber + 1; i++) {
                if (S[i] == 1)//已經在S set裡面不用比
                {
                    continue;
                }
                if (Cost[0, min] >= Cost[0, i])//找出與東門距離最短的
                {
                    min = i;//下一個要進入set的節點
                }

            }
            for (int j = 1; j < cornerNumber + CarNumber + 1; j++)//釋放j
            {
                if (Neighber[min, j] == 1)//代表下個要進入的點它的鄰居
                {
                    if (Cost[0, j] > Cost[0, min] + Cost[min, j])//如果可以透過此節點去到j比較快
                    {
                        Cost[0, j] = Cost[0, min] + Cost[min, j];//更新東門到此點的距離
                        pi[0, j] = min;//0去往j是由min過去的
                        pi[j, 0] = min;
                    }
                }
            }
            S[min] = 1;//此點加入S set
            for (int k = 1; k < cornerNumber + CarNumber + 1; k++)//檢查Q裡面還有沒有
            {
                if (S[k] == 0)//有東西在Qset
                {
                    flag = 1;//Q有東西
                    break;
                }
                else {
                    flag = 0;
                }
            }
        }
    }
    public void initial_cost()
    {
        for (int i = 0; i < cornerNumber + CarNumber; i++) {
            for (int j = 0; j < cornerNumber + CarNumber; j++) {
                if (i > 7 && j > 7 || i == j)//如果兩個節點都是車位就下一圈(不能從車位走到車位)
                {
                    continue;
                }
                if (Neighber[i, j] == 1) {
                    GameObject A, B;
                    if (i <= 7) {
                        if (i == 0) {
                            A = myself;
                        }
                        else {
                            A = corner[i - 1];//轉角
                        }

                    }
                    else {
                        if (i >= 8 && i < 31)//A車位
                        {
                            A = carpositionA[i - 7 - 1];
                        }
                        else//B車位
                        {
                            A = carpositionB[i - 30 - 1];
                        }
                    }

                    if (j <= 7) {
                        if (j == 0) {
                            B = myself;
                        }
                        else {
                            B = corner[j - 1];//轉角
                        }
                    }
                    else {
                        if (j >= 8 && j < 31)//A車位
                        {
                            B = carpositionA[j - 7 - 1];
                        }
                        else//B車位
                        {
                            B = carpositionB[j - 30 - 1];
                        }
                    }
                    Cost[i, j] = distanse(A, B);

                }
            }
        }
    }
    public void initial()
    {
        Neighber[0, 3] = 1;//0到5號節點是鄰居
        //以上為南門附近的corner
        Neighber[1, 2] = 1;
        Neighber[1, 3] = 1;
        //以上為1號corner(A1)的鄰居corner
        for (int i = 12; i <= 23; i++) {
            Neighber[1, i + 7] = 1;
        }
        //以上為1號corner可直達的車位
        Neighber[2, 1] = 1;
        Neighber[2, 4] = 1;
        //以上為2號corner(A2)的鄰居
        for (int i = 6; i < 12; i++) {
            Neighber[2, i + 7] = 1;
        }
        // Neighber[2,1+7]=1;
        // Neighber[2,2+7]=1;
        // Neighber[2,3+7]=1;
        // Neighber[2,17+7]=1;
        Neighber[2, 22 + 7] = 1;
        Neighber[2, 23 + 7] = 1;
        // for(int i=1;i<=6;i++)
        // {
        //     Neighber[2,i+7+23]=1;
        // }
        //以上為2號corner可直達的車位
        Neighber[3, 1] = 1;
        //A3
        for (int i = 1; i <= 5; i++) {
            Neighber[3, i + 7] = 1;
        }
        for (int i = 12; i <= 21; i++) {
            Neighber[3, i + 7] = 1;
        }
        //3號可直達的車位
        Neighber[4, 2] = 1;
        Neighber[4, 5] = 1;
        Neighber[4, 6] = 1;
        //B1
        for (int i = 4; i <= 9; i++) {
            Neighber[4, i + 7 + 23] = 1;
        }
        //B1可直達的車位
        Neighber[5, 4] = 1;
        Neighber[5, 7] = 1;
        //B2
        for (int i = 18; i <= 22; i++) {
            Neighber[5, i + 7 + 23] = 1;
        }
        // Neighber[5,7+7+23]=1;
        //B2可直達車位
        Neighber[6, 4] = 1;
        Neighber[6, 7] = 1;
        //B3

        for (int i = 1; i <= 14; i++) {
            Neighber[6, i + 7 + 23] = 1;
        }
        // Neighber[6,22+7+23]=1;
        //B3可直達的車位
        Neighber[7, 5] = 1;
        Neighber[7, 6] = 1;
        //B4
        for (int i = 11; i <= 22; i++) {
            Neighber[7, i + 7 + 23] = 1;
        }
        // Neighber[7,9+7+23]=1;
        //B4可直達的車位
        for (int i = 0; i < cornerNumber + CarNumber; i++) {
            for (int j = 0; j < cornerNumber + CarNumber; j++) {
                if (Neighber[i, j] == 1) {
                    pi[i, j] = i;//代表i前往j 是靠i過去的
                    pi[j, i] = j;
                }
                else {
                    pi[i, j] = -1;
                }
            }
        }



    }
    public double distanse(GameObject A, GameObject B)
    {
        Vector2 posA = A.transform.position;
        Vector2 posB = B.transform.position;
        double d;
        if (posA.x > posB.x) {
            d = posA.x - posB.x;
        }
        else {
            d = posB.x - posA.x;
        }
        if (posA.y > posB.y) {
            d += posA.y - posB.y;
        }
        else {
            d += posB.y - posA.y;
        }
        return d;
    }
}