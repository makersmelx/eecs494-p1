using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{

    public float enemyDamage = 0.5f;
    public float enemyHealth;

    private void Start()
    {
        enemyHealth = 1.0f;
    }

    private void Update()
    {
        if (enemyHealth == 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject object_collided = collision.collider.gameObject;
        if (object_collided.CompareTag("Player")){
            GameControl.Instance.AlterHealth(-enemyDamage);
        }
        else if (object_collided.CompareTag("Weapon")){
            enemyHealth -= 0.5f;
        }
    }


}
