// https://www.youtube.com/watch?v=5ZBynjAsfwI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSetup : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;

    private void Start(){
        line.SetUpLine(points);
    }
}