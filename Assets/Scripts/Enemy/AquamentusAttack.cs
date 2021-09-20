using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquamentusAttack : MonoBehaviour
{
    public float attack = 0.5f;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Player"))
        {
            GameControl.Instance.AlterHealth(-attack);
        }
    }
}