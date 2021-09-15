using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody _rigidbody;

    Vector2[] directions = {Vector2.up, Vector2.right, Vector2.down, Vector2.left};

    private Coroutine _currentMove;

    // Use this for initialization
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        _currentMove = StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float moveSpeed = GameControl.Instance.enemySpeed;
        while (true)
        {
            Vector2 dir = directions[Random.Range(0, directions.Length)];
            for (float moved = 0; moved < 1; moved += moveSpeed * Time.deltaTime)
            {
                _rigidbody.velocity = dir * moveSpeed;
                yield return null;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopCoroutine(_currentMove);
        _currentMove = StartCoroutine(Move());
    }

    private void OnTriggerEnter(Collider other)
    {
        StopCoroutine(_currentMove);
        _currentMove = StartCoroutine(Move());
    }
}