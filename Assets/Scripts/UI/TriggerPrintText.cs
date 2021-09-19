using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerPrintText : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
        }

        boxCollider.isTrigger = true;
        gameObject.layer = 0;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(text.GetComponent<PrintText>().DisablePlayerAndPrint());
    }
}