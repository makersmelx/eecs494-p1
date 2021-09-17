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

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !GameControl.Instance.PlayerControlEnabled())
        {
            return;
        }

        DoorControl doorControl = GetComponentInParent<DoorControl>();

        // Link can unlock the door only when he is facing the door
        foreach (DoorMoveCamera itr in doorControl.GetComponentsInChildren<DoorMoveCamera>())
        {
            if (itr.doorDirection != other.gameObject.GetComponent<PlayerControl>().playerDirection)
            {
                return;
            }
        }

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
        else if (sprite.name == "t_095")
        {
            _spriteRenderer.sprite = GameControl.Instance.sprites[48];
        }
        else if (sprite.name == "t_094")
        {
            _spriteRenderer.sprite = GameControl.Instance.sprites[48];
        }
    }
}