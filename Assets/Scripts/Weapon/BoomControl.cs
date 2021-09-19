using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : MonoBehaviour
{

    public float duration = 4.0f;
    public float blast_radius = 10.0f;
    public float damage = 0.5f;
    public float damage_force = 100f;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(blast());
    }



    IEnumerator blast()
    {
        Debug.Log("COROUTINE BLAST IS CALLED");

        yield return new WaitForSeconds(duration);

        Collider[] hitColliders = Physics.OverlapSphere(rb.transform.position, blast_radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Monster"))
            {
                Rigidbody enemy_rb = hitCollider.GetComponent<Rigidbody>();
                enemy_rb.AddForce(damage * (rb.transform.position - enemy_rb.transform.position), ForceMode.Impulse);
            }
        }

        Destroy(this.gameObject);


    }   


   

}
