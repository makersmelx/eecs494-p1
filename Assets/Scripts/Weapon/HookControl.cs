using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookControl : MonoBehaviour
{
    public float speed = 10f;
    public int type = 4;
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private int _catch;
    public bool returning;
    private Vector3 _playerPos;
    private GameObject _rope;
    private GameObject _victim;
    private Vector3 _initialVelocity;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        gameObject.tag = "Anchor";
        gameObject.layer = 14;
        _rigidbody = GetComponent<Rigidbody>();
        returning = false;
        _catch = 0;
        _rope = new GameObject();
    }

    private void Start()
    {
        _initialVelocity = _rigidbody.velocity;
        StartCoroutine(HookRope());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_catch > 0)
        {
            return;
        }

        if ((other.gameObject.CompareTag("Monster") || other.gameObject.CompareTag("Fire")) && !returning)
        {
            _catch += 1;

            _victim = other.gameObject;
            if (_victim.CompareTag("Monster"))
            {
                _victim.layer = 19;
            }
            else
            {
                _victim.GetComponent<Rigidbody>().isKinematic = false;
            }

            _victim.transform.position += _initialVelocity.normalized * -0.15f;
            _rigidbody.velocity *= -0.5f;
            _boxCollider.isTrigger = false;
            returning = true;
        }
        else if (other.gameObject.layer == 0 && _catch == 0)
        {
            if (!returning)
            {
                _rigidbody.velocity *= -1f;
                returning = true;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (returning && other.gameObject != _victim)
        {
            _boxCollider.isTrigger = true;
            _rigidbody.velocity = _initialVelocity * -1f;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 0 && _catch == 0)
        {
            _boxCollider.isTrigger = true;
        }
    }

    public void UpdatePlayerPosition(Vector3 pos)
    {
        _playerPos = pos;
    }

    IEnumerator HookRope()
    {
        LineRenderer lineRenderer = _rope.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.SetPosition(0, _playerPos);
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        while (true)
        {
            lineRenderer.SetPosition(1, transform.position);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        Destroy(_rope);
        if (_victim != null)
        {
            if (_victim.CompareTag("Fire"))
            {
                _victim.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                _victim.layer = 7;
            }
        }
    }
}