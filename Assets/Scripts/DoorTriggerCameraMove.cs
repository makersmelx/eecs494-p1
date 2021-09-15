using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerCameraMove : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private SpriteRenderer _spriteRenderer;
    public Vector2 doorDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sortingOrder = 3;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Player"))
        {
            otherGameObject.transform.position = transform.position;
            GameControl.Instance.MoveCamera(doorDirection);
        }
    }
}