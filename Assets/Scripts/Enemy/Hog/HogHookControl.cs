using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogHookControl: MonoBehaviour
{
    
    public GameObject spawner;
    public GameObject fire;
    public float speed = 10f;
   
    private Rigidbody _rigidbody;
    private Vector3 init_position;

    private bool _return = false;
    private bool _hook_active = false;

    private bool _hooked_player = false;
    private bool has_fire = false;


    private void Start()
    {
        if (spawner == null)
        {
            Destroy(this.gameObject);
        }

        _rigidbody = GetComponent<Rigidbody>();
        init_position = transform.position;
    }

    private void Update()
    {
        if (_hook_active)
        {
            
            if (_return)
            {
                _rigidbody.velocity = new Vector3(0, 1.0f, 0) * speed;
                if (Vector3.Distance(init_position, transform.position) < 0.1f)
                {
                    _return = false;
                    _hook_active = false;
                    transform.position = init_position;

                    if (_hooked_player)
                    {
                        _hooked_player = false;
                        GameControl.Instance.SetPlayerControl(true);
                    }
                    spawner.GetComponent<HogControl>().ResetHook();
                }
            }
            else
            {
                _rigidbody.velocity = new Vector3(0, -1.0f, 0) * speed;
                if (Vector3.Distance(init_position, transform.position) > 10f)
                {
                    Debug.Log("HOOK INIT DISTANCE " + (init_position.y - transform.position.y).ToString());
                    _return = true;
                    Debug.Log("HOOK RETURNING");
                }
            }


            if (_hooked_player)
            {
                GameControl.Instance.player.GetComponent<Rigidbody>().velocity = _rigidbody.velocity;
            }
        } 


    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject other_gameobject = other.gameObject;
        Debug.Log("HogRoom Hooked " + other_gameobject.tag);
        if (_hook_active)
        {
            if (other_gameobject.tag != "Monster") _return = true;

            if (other_gameobject.tag == "Player")
            {
                _hooked_player = true;
                GameControl.Instance.SetPlayerControl(false);
            }

            if (other_gameobject.tag == "HogRoomFire" && !has_fire)
            {
                has_fire = true;
                GameObject fire_taken = Instantiate(fire, transform.position, Quaternion.identity);
                fire_taken.GetComponent<Rigidbody>().velocity = new Vector3(0, 1.0f, 0) * speed;
                fire_taken.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.5f);
            }
        }
    }

    public void SetHookActive()
    {
        _hook_active = true;
        has_fire = false;
        init_position = transform.position;
    }

}
