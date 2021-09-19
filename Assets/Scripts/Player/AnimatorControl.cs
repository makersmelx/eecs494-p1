using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    public static AnimatorControl Instance;
    private Animator _animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameControl.Instance.PlayerControlEnabled())
        {
            _animator.SetFloat("verticalInput", Input.GetAxisRaw("Vertical"));
            _animator.SetFloat("horizontalInput", Input.GetAxisRaw("Horizontal"));
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                _animator.speed = 0;
            }
            else
            {
                _animator.speed = 1;
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                _animator.speed = 1;
                _animator.ResetTrigger("attack");
                _animator.SetTrigger("attack");
            }
        }
        else
        {
            // wait for the frames to follow
            if (!IsMoveAnimation())
            {
                _animator.speed = 1;
                SetAnimation2DAxis(Vector2.zero);
            }
            else
            {
                _animator.speed = 0;
            }
        }
    }

    public void SetAnimation2DAxis(Vector2 input)
    {
        _animator.SetFloat("horizontalInput", input.x);
        _animator.SetFloat("verticalInput", input.y);
    }

    public bool IsAttackAnimation()
    {
        string[] attackState = {"SwordUp", "SwordDown", "SwordLeft", "SwordRight"};
        foreach (string state in attackState)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(state))
            {
                return true;
            }
        }

        return false;
    }

    public bool IsMoveAnimation()
    {
        string[] states = {"RunUp", "RunDown", "RunLeft", "RunRight"};
        foreach (string state in states)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(state))
            {
                return true;
            }
        }

        return false;
    }
}