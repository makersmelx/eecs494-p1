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
            GameControl.Instance.AlterHealth(-attack);
        }
    }
}