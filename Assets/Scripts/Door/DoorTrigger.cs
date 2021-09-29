using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public float pushBlockTime = 2f;
    public float mass = 1000f;
    private Vector3 _moveDirection;
    private Vector3 _originalPosition;
    private Coroutine _currentPush;
    private bool _canMove;
    private bool _triggered;

    void Start()
    {
        AddRigidBody();
        _originalPosition = transform.position;
        ResetTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        if (_canMove && !_triggered)
        {
            StartCoroutine(MoveObjectOverTime(gameObject.transform, transform.position,
                transform.position + _moveDirection.normalized, 0.8f));
            DoorControl doorControl = GetComponentInParent<DoorControl>();
            if (doorControl != null)
            {
                doorControl.UnLockDoors();
            }

            _triggered = true;
        }
    }

    IEnumerator WaitThenPush(Vector3 direction)
    {
        yield return new WaitForSeconds(pushBlockTime);
        _moveDirection = direction;
        _canMove = true;
        yield return null;
    }

    IEnumerator MoveObjectOverTime(Transform target, Vector3 initial, Vector3 dest, float duration)
    {
        float initialTime = Time.time;
        float progress = 0;
        while (progress < 1.0f)
        {
            progress = (Time.time - initialTime) / duration;
            Vector3 newPosition = Vector3.Lerp(initial, dest, progress);
            target.position = newPosition;
            yield return null;
        }

        target.position = dest;
    }

    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (!RoomControl.CheckAllEnemyDeadInRoom(gameObject))
        {
            return;
        }

        if (!_triggered && (Vector2) other.contacts[0].normal ==
            other.gameObject.GetComponent<PlayerControl>().GetInput())
        {
            if (_currentPush == null)
            {
                _currentPush =
                    StartCoroutine(WaitThenPush(other.contacts[0].normal));
            }
        }
        else
        {
            if (_currentPush != null)
            {
                StopCoroutine(_currentPush);
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (_currentPush != null)
        {
            StopCoroutine(_currentPush);
        }
    }


    private void AddRigidBody()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.mass = mass;
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    public void ResetTrigger()
    {
        transform.position = _originalPosition;
        _triggered = false;
        _canMove = false;
        _currentPush = null;
    }
}