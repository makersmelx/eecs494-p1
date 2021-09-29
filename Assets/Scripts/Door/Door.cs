using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In this project, a Door must be placed as a child of DoorControl
// Manage Door's sprite here
// Collision layer of the door is maintained by DoorControl
public class Door : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private int _originalIndex;

    public AudioClip _door_open_sound_clip;

    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        try
        {
            _originalIndex = Int32.Parse(_spriteRenderer.sprite.name.Substring(2, 3));
        }
        catch (FormatException e)
        {
            _originalIndex = 0;
        }

        if (GetComponentInParent<DoorControl>().triggered)
        {
            gameObject.AddComponent<DoorOpenThenClose>();
        }

        gameObject.AddComponent<BoxCollider>();
    }

    private void Update()
    {
        Vector3 playerPosition = GameControl.Instance.player.transform.position;
        if (Math.Abs(playerPosition.x - transform.position.x) <= 1 &&
            Math.Abs(playerPosition.y - transform.position.y) <= 1)
        {
            RoomControl.WelcomePlayerIntoRoom(gameObject);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("Player") || !GameControl.Instance.PlayerControlEnabled())
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
        AudioSource.PlayClipAtPoint(_door_open_sound_clip, Camera.main.transform.position);

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
        else if (sprite.name == "t_100")
        {
            _spriteRenderer.sprite = GameControl.Instance.sprites[51];
        }
        else if (sprite.name == "t_094")
        {
            if (GetComponentInParent<DoorControl>().GetComponentInChildren<DoorMoveCamera>().doorDirection.x > 0)
            {
                _spriteRenderer.sprite = GameControl.Instance.sprites[48];
            }
            else
            {
                _spriteRenderer.sprite = GameControl.Instance.sprites[51];
            }
        }
    }

    public void ResetDoor()
    {
        _spriteRenderer.sprite = GameControl.Instance.sprites[_originalIndex];
    }
}