using System;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter(Collision other)
    {
        DoorControl doorControl = GetComponentInParent<DoorControl>();
        doorControl.TryOpenLockedDoor(other);
    }

    public void UnLockChangeSprite()
    {
        Sprite sprite = _spriteRenderer.sprite;
        // todo: implement left/right faced door
        if (sprite.name == "t_080")
        {
            _spriteRenderer.sprite = GameControl.Instance.sprites[92];
        }
        else if (sprite.name == "t_081")
        {
            _spriteRenderer.sprite = GameControl.Instance.sprites[93];
        }
    }
}