using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public float pushBlockTime = 2f;
    public float mass = 1000f;
    public Vector3 movePath;
    private Vector3 _originalPosition;
    private Coroutine _currentPush;


    void Start()
    {
        _originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onMovePathOfTrigger())
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }

            transform.position = (_originalPosition - transform.position).magnitude >
                                 movePath.magnitude
                ? _originalPosition + movePath
                : _originalPosition;
        }

        if (transform.position.Equals(_originalPosition + movePath))
        {
            print("OK");
            GetComponentInParent<DoorControl>().UnLockDoors();
        }
    }

    IEnumerator WaitThenPush()
    {
        yield return new WaitForSeconds(pushBlockTime);
        AddRigidBody();

        yield return null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        _currentPush = StartCoroutine(WaitThenPush());
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        StopCoroutine(_currentPush);
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Destroy(rb);
        }
    }

    private void AddRigidBody()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.mass = mass;
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation
                             | RigidbodyConstraints.FreezePositionZ
                             | RigidbodyConstraints.FreezePositionY;
        }
    }

    private bool onMovePathOfTrigger()
    {
        Vector3 shift = transform.position - _originalPosition;
        print(_originalPosition);
        print(movePath);
        print(shift);
        print("=============");
        return shift.Equals(Vector3.zero) ||
               (shift.normalized.Equals(movePath.normalized) && shift.magnitude <= movePath.magnitude);
    }
}