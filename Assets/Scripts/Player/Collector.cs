using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private Inventory _inventory;
    public AudioClip _rupee_collection_sound_clip;

    private void Start()
    {
        _inventory = GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Rupee"))
        {
            if (_inventory != null)
            {
                _inventory.AlterRupees(1);
            }

            Destroy(otherGameObject);
            AudioSource.PlayClipAtPoint(_rupee_collection_sound_clip, Camera.main.transform.position);
        }
        else if (otherGameObject.CompareTag("Heart"))
        {
            GameControl.Instance.AlterHealth(1f);
            Destroy(otherGameObject);
        }else if (otherGameObject.CompareTag("Key"))
        {
            if (_inventory != null)
            {
                _inventory.AlterKeys(1);
            }
            Destroy(otherGameObject);
        }
    }
}