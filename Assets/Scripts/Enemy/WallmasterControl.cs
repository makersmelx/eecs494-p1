using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallmasterControl : MonoBehaviour
{
    private float speed = 1.0f;

    Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    private Vector2 direction;

    private Coroutine _currentMove;
    private Animator _animator;
    private Rigidbody _rigidbody;



    // animator type is true for type 1 direction, false for type 2 direction
    private bool animator_type = true; 


    // Start is called before the first frame update
    void Start()
    {
        Vector3 player_position = GameControl.Instance.player.transform.position;

        
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
       

        if (transform.position.x <= player_position.x + 3)
        {
            animator_type = false;
        }

        _animator.SetBool("type_1", animator_type);


        _currentMove = StartCoroutine(Move());




    }

  

    IEnumerator Move()
    {
        float moveSpeed = speed;
        Vector3 player_position = GameControl.Instance.player.transform.position;

        while (true)
        {
            if (transform.position.y <= 34f)
            {
                direction = Vector2.up;
            }
            else if (transform.position.y >= 42f)
            {
                direction = Vector2.down;
            }
            else if (transform.position.x <= 64f)
            {
                direction = Vector2.right;
            }
            else if (transform.position.x >= 80f)
            {
                direction = Vector2.left;

            }
            else
            {
                if (Random.Range(0,2) == 0)
                {
                    if (player_position.x > transform.position.x + 1)
                    {
                        direction = Vector2.right;
                    } else
                    {
                        direction = Vector2.left;
                    }
                }
                else
                {
                    if (player_position.y > transform.position.y + 1)
                    {
                        direction = Vector2.up;
                    }
                    else
                    {
                        direction = Vector2.down;
                    }
                }
            }


            for (float moved = 0; moved < 1; moved += moveSpeed * Time.deltaTime)
            {             
                _rigidbody.velocity = direction * moveSpeed;
                yield return null;
            }
        }
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other_gameobject = collision.collider.gameObject;
        if (other_gameobject.CompareTag("Player") && !GameControl.Instance.isInvinsible)
        {
            CameraControl.Instance.Reset();
            GameControl.Instance.player.transform.position = new Vector3(39.5f, 2f, 0f);
        }
    }

}
