using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float movementSpeed = 4f;

    private Vector3 playerDirection = Vector3.zero;

    Color _original_sprite_color;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _original_sprite_color = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        _rigidbody.velocity = GameControl.Instance.PlayerControlEnabled() ? GetInput() * movementSpeed : Vector2.zero;
    }

    Vector2 GetInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        if (Math.Abs(horizontalInput) > 0.0f)
        {
            verticalInput = 0.0f;
        }

        return new Vector2(horizontalInput, verticalInput);
    }



    private void OnCollisionEnter(Collision collision)
    {
        GameObject object_collided = collision.collider.gameObject;
        if (object_collided.CompareTag("Monster"))
        {
            Debug.Log("PLAYER COLLIDED WITH MONSTER");
            if (!GameControl.Instance.isInvinsible)
            {
                _rigidbody.AddForce(playerDirection * (-40), ForceMode.Impulse);
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                StartCoroutine(restoreColor());
            }
           
        }
    }

    
    IEnumerator restoreColor()
    {
        Debug.Log("Coroutine DETECTED" + Time.deltaTime);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = _original_sprite_color;
        Debug.Log("Coroutine Ended" + Time.deltaTime);
       
    }



    public void UpdateDirection()
    {
        if (GetInput().x < 0) playerDirection = new Vector3(-1.0f, 0, 0);
        if (GetInput().x > 0) playerDirection = new Vector3(1.0f, 0, 0);
        if (GetInput().y < 0) playerDirection = new Vector3(0, -1.0f, 0);
        if (GetInput().y > 0) playerDirection = new Vector3(0, 1.0f, 0);
    }


}