using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{
    public float attack;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        if (other.gameObject.CompareTag("Player"))
        {
            GameControl.Instance.AlterHealth(-1f * attack);
            otherGameObject.GetComponent<PlayerControl>().playerKnockBack(40f);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (GameControl.Instance.IsOutOfBound(transform))
        {
            Destroy(gameObject);
        }
    }
}