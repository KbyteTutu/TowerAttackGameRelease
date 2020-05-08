using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform[] wayPoints;
    public static Transform[] way1;
    public static Transform[] way2;
    public static Transform[] way3;


    private void Awake()
    {
        wayPoints = new Transform[transform.childCount];
        
        int[] way1Index = { 0, 1,2,3,4,5,6,7,16 };
        int[] way2Index = { 8,9,10,11,12,6,7,16 };
        int[] way3Index = { 13,14,15,7,16 };

        way1 = new Transform[way1Index.Length];
        way2 = new Transform[way2Index.Length];
        way3 = new Transform[way3Index.Length];

        //for (int i = 0; i < wayPoints.Length; i++)
        //{
        //    way1[i] = transform.GetChild
        //}
        for (int i = 0; i < way1Index.Length; i++)
        {
            way1[i] = transform.GetChild(way1Index[i]);
        }
        for (int i = 0; i < way2Index.Length; i++)
        {
            way2[i] = transform.GetChild(way2Index[i]);
        }
        for (int i = 0; i < way3Index.Length; i++)
        {
            way3[i] = transform.GetChild(way3Index[i]);
        }
    }
}
