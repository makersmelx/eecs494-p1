using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriyaControl : MonoBehaviour
{

  
    private float speed = 2.0f;

    Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    private bool isthrowing = false;

    private Vector2 direction;

    private Coroutine _currentMove;
    private Animator _animator;
    private Rigidbody _rigidbody;

    public GameObject boomerang;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _currentMove = StartCoroutine(Move());
        InvokeRepeating("throw_boomerang", 1.0f, 5.0f);
    }

    IEnumerator Move()
    {
        float moveSpeed = speed;
        while (true)
        {
            direction = directions[UnityEngine.Random.Range(0, directions.Length)];
            for (float moved = 0; moved < 1; moved += moveSpeed * Time.deltaTime)
            {
                if (!isthrowing) {
                    _rigidbody.velocity = direction * moveSpeed;
                    _animator.SetFloat("move_x", direction.x);
                    _animator.SetFloat("move_y", direction.y);
                }
                    
                yield return null;
            }
        }
    }

    void throw_boomerang()
    {
        // Debug.Log("boomerang thrown by goriya");
        if (!isthrowing)
        {
            isthrowing = true;
            GameObject boomerang_thrown = Instantiate(boomerang, transform.position + new Vector3(direction.x, direction.y, 0), Quaternion.identity);
            boomerang_thrown.GetComponent<GoriyaBoomerangControl>().player = this.gameObject;
        }

    }

    public void boomerang_fetched() {
        isthrowing = false;
    }


    


   
}
