using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToMonster : MonoBehaviour
{

    public float weapon_damage = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster") || other.gameObject.CompareTag("Wallmaster"))
        {
            if (other.gameObject.GetComponent<HasHealth>() != null)
                other.gameObject.GetComponent<HasHealth>().AlterHealth(weapon_damage);
        }
    }

}
