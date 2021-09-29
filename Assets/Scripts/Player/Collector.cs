using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private Inventory _inventory;
    public AudioClip _rupee_collection_sound_clip;
    public AudioClip _advrupee_collectio_sound_clip;
    public AudioClip _health_collection_sound_clip;
    public AudioClip _bow_collection_sound_clip;
    public AudioClip _key_collection_sound_clip;

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
        else if (otherGameObject.CompareTag("AdvRupee"))
        {
            if (_inventory != null)
            {
                _inventory.AlterRupees(5);
            }

            Destroy(otherGameObject);
            AudioSource.PlayClipAtPoint(_advrupee_collectio_sound_clip, Camera.main.transform.position);
        }
        else if (otherGameObject.CompareTag("Heart"))
        {
            GameControl.Instance.AlterHealth(1f);
            Destroy(otherGameObject);
            AudioSource.PlayClipAtPoint(_health_collection_sound_clip, Camera.main.transform.position);
        }
        else if (otherGameObject.CompareTag("Key"))
        {
            if (_inventory != null)
            {
                _inventory.AlterKeys(1);
            }

            Destroy(otherGameObject);
            AudioSource.PlayClipAtPoint(_key_collection_sound_clip, Camera.main.transform.position);
        }
        else if (otherGameObject.CompareTag("Bow"))
        {
            StartCoroutine(GetBow(otherGameObject));
        }
        else if (otherGameObject.CompareTag("Boomerang"))
        {
            Destroy(otherGameObject);
            GameControl.Instance.AddWeapon("boomerang");
            AudioSource.PlayClipAtPoint(_bow_collection_sound_clip, Camera.main.transform.position);
        }
        else if (otherGameObject.CompareTag("HookCollectable"))
        {
            StartCoroutine(GetHook(otherGameObject));
        }
    }

    private IEnumerator GetBow(GameObject otherGameObject)
    {
        otherGameObject.transform.position = transform.position + Vector3.up + 0.20f * Vector3.left;
        GetComponent<Animator>().SetBool("getBow", true);
        GameControl.Instance.SetPlayerControl(false);

        GameControl.Instance.AddWeapon("bow");
        AudioSource.PlayClipAtPoint(_bow_collection_sound_clip, Camera.main.transform.position);

        yield return new WaitForSeconds(2);
        GetComponent<Animator>().SetBool("getBow", false);
        GameControl.Instance.SetPlayerControl(true);
        Destroy(otherGameObject);
        yield return null;
    }

    private IEnumerator GetHook(GameObject otherGameObject)
    {
        otherGameObject.transform.position = transform.position + Vector3.up + 0.20f * Vector3.left;
        GetComponent<Animator>().SetBool("getBow", true);
        GameControl.Instance.SetPlayerControl(false);

        GameControl.Instance.AddWeapon("hook");
        AudioSource.PlayClipAtPoint(_bow_collection_sound_clip, Camera.main.transform.position);

        yield return new WaitForSeconds(2);
        GetComponent<Animator>().SetBool("getBow", false);
        GameControl.Instance.SetPlayerControl(true);
        Destroy(otherGameObject);
        yield return null;
    }
}