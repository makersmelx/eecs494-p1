using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : MonoBehaviour
{


    public GameObject cloud;

    private float duration = 2.0f;
    private float blast_radius = 2.0f;
    private float damage_force = 50f;
    private float damage = 2.0f;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(warn());
        StartCoroutine(blast());
    }

   

    IEnumerator warn()
    {
        yield return new WaitForSeconds(duration-0.5f);
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
    }

    IEnumerator blast()
    {
        yield return new WaitForSeconds(duration);

        GameObject bomb_cloud = Instantiate(cloud, transform.position + new Vector3(0, 0.5f, 0), transform.rotation) as GameObject;
       

        Collider[] hitColliders = Physics.OverlapSphere(rb.transform.position, blast_radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Monster"))
            {
                
                //Debug.Log(hitCollider.tag + " was attacked by BOMB");
                Rigidbody enemy_rb = hitCollider.gameObject.GetComponent<Rigidbody>();
                //enemy_rb.AddForce(damage_force * (rb.transform.position - enemy_rb.transform.position), ForceMode.Impulse);
                hitCollider.gameObject.GetComponent<HasHealth>().AlterHealth(damage);
                hitCollider.gameObject.GetComponent<Animator>().speed = 0;
                enemy_rb.AddForce(40f * Vector3.Normalize(enemy_rb.transform.position - transform.position), ForceMode.Impulse);
                Color restore_color = hitCollider.GetComponent<SpriteRenderer>().color;
                hitCollider.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                StartCoroutine(restoreCollider(hitCollider.gameObject, restore_color));
                
            }
        }

        Destroy(bomb_cloud, 0.5f);
        Destroy(this.gameObject);
        GameControl.Instance.player.GetComponent<WeaponControl>().can_throw = true;



    }


    IEnumerator restoreCollider(GameObject obj, Color init_color)
    {
        yield return new WaitForSeconds(1.0f);
        obj.GetComponent<Animator>().speed = 1;
        obj.GetComponent<SpriteRenderer>().color = init_color;

    }

    /*
     
    IEnumerator cloudVanish() {
    }

    */


   

}
