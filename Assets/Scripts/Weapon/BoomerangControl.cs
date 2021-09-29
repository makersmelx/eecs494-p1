using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoomerangControl : MonoBehaviour
{

    private float rot_speed = 10f;
    private float speed = 5.0f;
    private float max_distance = 5.0f;
    private Rigidbody rb;
    private Transform player_transform;
    private bool back = false;
    private float min_distance = 0.2f;
    private Vector3 init_position;
    private bool isReturning;
    public AudioClip _rupee_collection_sound_clip;
    public AudioClip _advrupee_collection_sound_clip;
    public AudioClip _health_collection_sound_clip;
    public AudioClip _key_collection_sound_clip;
    public AudioClip _boomerang_thrown_sound_clip;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player_transform = GameControl.Instance.player.transform;
        init_position = player_transform.position;
        isReturning = false;
        AudioSource.PlayClipAtPoint(_boomerang_thrown_sound_clip, Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        // UPDATE ROTATION
        rb.transform.Rotate(new Vector3(0, 0, rot_speed));


        // UPDATE VELOCITY
        if (isReturning)
        {
            rb.velocity = Vector3.Normalize(player_transform.position - rb.transform.position) * speed;
            if (Vector3.Distance(rb.transform.position, player_transform.position) <= min_distance)
            {
                Destroy(this.gameObject);
                GameControl.Instance.player.GetComponent<WeaponControl>().can_throw = true;
            }
        }
        else
        {
            rb.velocity = Vector3.Normalize(rb.transform.position - init_position) * speed;
        }

        if (Vector3.Distance(init_position, rb.transform.position) >= max_distance)
        {
            isReturning = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("BOOMERANG TRIGGER DETECTED");

        GameObject player = GameControl.Instance.player;
        Inventory _inventory = player.GetComponent<Inventory>();
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
            AudioSource.PlayClipAtPoint(_advrupee_collection_sound_clip, Camera.main.transform.position);
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
    }





}
