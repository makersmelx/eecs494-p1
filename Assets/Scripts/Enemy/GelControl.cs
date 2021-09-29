using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelControl : MonoBehaviour
{
    private float speed = 1.0f;

    Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    private Vector2 direction;
    private Coroutine _currentMove;
    private Rigidbody _rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
         _currentMove = StartCoroutine(Move());
        
    }

    IEnumerator Move()
    {
        float moveSpeed = speed;
        float duration_offset = Random.Range(0.8f, 1.2f);

        while (true)
        {
            direction = directions[UnityEngine.Random.Range(0, directions.Length)];
            for (float moved = 0; moved < duration_offset; moved += moveSpeed * Time.deltaTime)
            {
                _rigidbody.velocity = direction * moveSpeed;
                yield return null;
            }
            for (float moved = 0; moved < duration_offset/2; moved += moveSpeed * Time.deltaTime)
            {
                // Debug.Log("GEL STOPPED");
                _rigidbody.velocity = Vector3.zero;
                yield return null;
            }
        }
    }

   
}
