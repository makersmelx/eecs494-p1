using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject FirePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            StartCoroutine(LavaBurnToAsh(other.gameObject));
        }
    }

    IEnumerator LavaBurnToAsh(GameObject victim)
    {
        Rigidbody rb = victim.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }

        BoxCollider bc = victim.GetComponent<BoxCollider>();
        if (rb != null)
        {
            Destroy(bc);
        }

        victim.transform.position = transform.position;
        GameObject fire = Instantiate(FirePrefab, victim.transform);
        fire.GetComponent<SpriteRenderer>().color = new Color(87 / 255f, 87 / 255f, 80 / 255f, 140 / 255f);
        Destroy(fire.GetComponent<BoxCollider>());
        yield return new WaitForSeconds(2);
        Destroy(victim);
        yield return null;
    }
}