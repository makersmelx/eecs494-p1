using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAttack : MonoBehaviour
{
    public float attack = 0.5f;

    private void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject object_collided = collision.collider.gameObject;
        if (object_collided.CompareTag("Player"))
        {
            if (!GameControl.Instance.player.GetComponent<PlayerControl>().in_knockback_delay)
            {
                GameControl.Instance.AlterHealth(-attack);
                GameControl.Instance.player.GetComponent<PlayerControl>().playerKnockBack(50f);
            }
            
        }
    }
}