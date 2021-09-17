using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class KeeseMove : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float speed;
    public float period = 8f;

    readonly Vector2[] _directions =
    {
        Vector2.up, Vector2.right, Vector2.down, Vector2.left,
        new Vector2(1, 1).normalized,
        new Vector2(-1, 1).normalized,
        new Vector2(1, -1).normalized,
        new Vector2(-1, -1).normalized,
    };

    private Coroutine _currentMove, _speedChange;
    private float _currentSpeed;

    // Use this for initialization
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        _currentMove = StartCoroutine(Move());
        _speedChange = StartCoroutine(SpeedChange());
        _currentSpeed = 0;
    }

    private void Update()
    {
    }

    IEnumerator MoveInDirectionOnce(Vector2 direction)
    {
        for (float moved = 0; moved < 1f; moved += _currentSpeed * Time.deltaTime)
        {
            _rigidbody.velocity = direction * _currentSpeed;

            yield return null;
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            Vector2 dir = _directions[Random.Range(0, _directions.Length)].normalized;
            for (float moved = 0; moved < 1; moved += _currentSpeed * Time.deltaTime)
            {
                _rigidbody.velocity = dir * _currentSpeed;

                yield return null;
            }
        }
    }

    IEnumerator Move(Vector2 direction)
    {
        yield return MoveInDirectionOnce(direction);


        while (true)
        {
            Vector2 dir = _directions[Random.Range(0, _directions.Length)].normalized;
            for (float moved = 0; moved < 1; moved += _currentSpeed * Time.deltaTime)
            {
                _rigidbody.velocity = dir * _currentSpeed;

                yield return null;
            }
        }
    }

    IEnumerator SpeedChange()
    {
        while (true)
        {
            float initialTime = Time.time;
            float progress = (Time.time - initialTime) / period;

            while (progress < 1.0f)
            {
                progress = (Time.time - initialTime) / (period);
                if (progress < 1f / 8f)
                {
                    _currentSpeed = progress * speed * 2f;
                }
                else if (progress < 2f / 8f)
                {
                    _currentSpeed = speed / 2f;
                }
                else if (progress < 3f / 8f)
                {
                    _currentSpeed = speed / 2f + (progress - 2f / 8f) * speed * 4f;
                }
                else if (progress < 4f / 8f)
                {
                    _currentSpeed = speed;
                }
                else if (progress < 5f / 8f)
                {
                    _currentSpeed = speed - (progress - 4f / 8f) * speed * 4f;
                }
                else if (progress < 6f / 8f)
                {
                    _currentSpeed = speed / 2f;
                }
                else if (progress < 7f / 8f)
                {
                    _currentSpeed = speed / 2f - (progress - 6f / 8f) * speed * 4f;
                }
                else
                {
                    _currentSpeed = 0f;
                }

                yield return null;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        StopCoroutine(_currentMove);
        _currentMove = StartCoroutine(Move(other.impulse));
    }
}