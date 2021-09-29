using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireIgnite : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        CanBurn canBurn = other.gameObject.GetComponent<CanBurn>();
        if (canBurn != null)
        {
            canBurn.Burn();
            GetComponentInParent<Fire>().goOut = true;
        }
    }
}