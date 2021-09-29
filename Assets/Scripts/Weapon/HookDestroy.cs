using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDestroy : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HookControl hookControl = GetComponentInParent<HookControl>();
            if (hookControl != null && hookControl.returning)
            {
                Destroy(hookControl.gameObject);
            }
        }
    }
}