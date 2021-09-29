using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquamentusMove : MonoBehaviour
{
    public float speed;
    private Rigidbody _rigidbody;

    Vector2[] directions = {Vector2.right, Vector2.left};

    private Coroutine _currentMove;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentMove = StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float moveSpeed = speed;
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
}