using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSlotNum : MonoBehaviour
{
    public static int SlotNumber;
    void Start()
    {
        Dictionary<string, int> DictSlotNum =
        new Dictionary<string, int>(){
            {"A01", 8},{"A02", 9},{"A03", 10},{"A04", 11},{"A05", 12},{"A06", 13},{"A07", 14},{"A08", 15},{"A09", 16},{"A10", 17},{"A11", 18},{"A12", 19},{"A13", 20},{"A14", 21},{"A15", 22},{"A16", 23},{"A17", 24},{"A18", 25},{"A19", 26},{"A20", 27},{"A21", 28},{"A22", 29},{"A23", 30},
            {"B01", 31},{"B02", 32},{"B03", 33},{"B04", 34},{"B05", 35},{"B06", 36},{"B07", 37},{"B08", 38},{"B09", 39},{"B10", 40},{"B11", 41},{"B12", 42},{"B13", 43},{"B14", 44},{"B15", 45},{"B16", 46},{"B17", 47},{"B18", 48},{"B19", 49},{"B20", 50},{"B21", 51},{"B22", 52}
        };

        if ( true == ( DictSlotNum.ContainsKey( EnterSlotNum.SlotNum ) ) ){
        SlotNumber=DictSlotNum[EnterSlotNum.SlotNum];
        }
        else{
        print("Not Found");
        }
    }

   
}
