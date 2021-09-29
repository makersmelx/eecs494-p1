using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpStairsBowRoom : MonoBehaviour
{
    // Start is called before the first frame update
    private float duration = 3f;

    private void OnCollisionEnter(Collision other)
    {
        StartCoroutine(UpDownStairs.UpDownStairsCoroutine(
            Vector3.right * GameControl.GridWidth,
            new Vector3(22f, 58f, 0),
            duration
        ));
    }
}