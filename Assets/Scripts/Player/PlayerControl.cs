using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float movementSpeed = 4f;

    public Vector2 playerDirection = Vector2.zero;

    public AudioClip player_knocked_sound_clip;

    Color _original_sprite_color;

    public bool in_knockback_delay;

    public Vector2 directionKeyDown;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _original_sprite_color = GetComponent<SpriteRenderer>().color;
        in_knockback_delay = false;
        directionKeyDown = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = GameControl.Instance.PlayerControlEnabled() ? GetInput() * movementSpeed : Vector2.zero;
        UpdateDirection();
    }

    public Vector2 GetInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            directionKeyDown = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            directionKeyDown = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            directionKeyDown = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            directionKeyDown = Vector2.right;
        }

        if (directionKeyDown.y != 0)
        {
            if (Math.Abs(verticalInput) > 0.0f)
            {
                horizontalInput = 0.0f;
            }
        }

        if (directionKeyDown.x != 0)
        {
            if (Math.Abs(horizontalInput) > 0.0f)
            {
                verticalInput = 0.0f;
            }
        }


        return new Vector2(horizontalInput, verticalInput);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        GameObject object_collided = collision.collider.gameObject;
        if (object_collided.CompareTag("Monster"))
        {
            playerKnockBack(40);
        }
    }
    */

    public void playerKnockBack(float force)
    {
        Debug.Log("PLAYER COLLIDED WITH MONSTER");
        if (!GameControl.Instance.isInvinsible && !in_knockback_delay)
        {
            Debug.Log("PLAYER COLLIDED WITH MONSTER");
            _rigidbody.AddForce(Vector3.Normalize(playerDirection) * (-force), ForceMode.Impulse);
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            in_knockback_delay = true;
            GameControl.Instance.SetPlayerControl(false);
            StartCoroutine(restore());
            AudioSource.PlayClipAtPoint(player_knocked_sound_clip, Camera.main.transform.position);
        }
    }

    // RESTORE: COLOR, MOVE, ABLILITY TO BE ATTACKED
    IEnumerator restore()
    {
        GameControl.Instance.SetPlayerControl(false);
        Debug.Log("Coroutine RESTORE COLOR DETECTED" + Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(restore_knockback_delay());
        GetComponent<SpriteRenderer>().color = _original_sprite_color;
        GameControl.Instance.SetPlayerControl(true);
    }

    IEnumerator restore_knockback_delay()
    {
        yield return new WaitForSeconds(1f);
        in_knockback_delay = false;
    }


    public void UpdateDirection()
    {
        playerDirection = GetInput();
    }

    public IEnumerator WalkWithCoroutine(Vector3 shift)
    {
        Vector3 dir = shift.normalized;
        for (float moved = 0; moved < shift.magnitude; moved += movementSpeed * Time.deltaTime)
        {
            _rigidbody.velocity = shift;
            GetComponent<AnimatorControl>().SetAnimation2DAxis(shift);
            yield return null;
        }
    }
}