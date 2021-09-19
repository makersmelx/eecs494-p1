using System;
using System.Collections.Generic;
using UnityEngine;


// Manage Door's sprite here
public class Door : MonoBehaviour
{
    public bool isTriggerDoor;
    private SpriteRenderer _spriteRenderer;
    private int _originalIndex;
    private bool _isBackFromDoor;

    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalIndex = Int32.Parse(_spriteRenderer.sprite.name.Substring(2, 3));
        _isBackFromDoor = false;
    }

    private void Update()
    {
        // Reset the door when the player comes by transition and then leaves the exact block
        if (_isBackFromDoor && isTriggerDoor && gameObject.layer == 6)
        {
            Vector3 playerPosition = GameControl.Instance.player.transform.position;
            if (Math.Abs(playerPosition.x - transform.position.x) >= 1 ||
                Math.Abs(playerPosition.y - transform.position.y) >= 1)
            {
                GetComponentInParent<DoorControl>().ResetDoors();
                _isBackFromDoor = false;
            }
        }
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

    private void OnCollisionEnter(Collision other)
    {
        if ((other.transform.position - transform.position).magnitude <= 0.3)
        {
            GetComponentInParent<DoorControl>().UnLockDoors();
            _isBackFromDoor = true;
        }
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

    public void ResetDoor()
    {
        _spriteRenderer.sprite = GameControl.Instance.sprites[_originalIndex];
    }
}