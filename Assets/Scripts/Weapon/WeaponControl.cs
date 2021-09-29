using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject sword;

    public GameObject bow;

    public GameObject boomerang;

    public GameObject bomb;

    public GameObject hook;


    //private float _damage = 2.0f;
    private float _weapon_direction_z = 0;
    private Vector3 _weapon_position_shift;
    private Vector3 _weapon_position;

    public AudioClip sword_use_sound_clip;
    public AudioClip sword_glow_sound_clip;
    public AudioClip hook_use_sound_clip;


    private bool in_delay = false;

    public bool can_throw = true;


    private void Start()
    {
        can_throw = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        GameControl.Instance.UpdateWeapon();
        int main = GameControl.Instance.weaponMain;
        int alt = GameControl.Instance.weaponAlt;
        if (!in_delay && GameControl.Instance.PlayerControlEnabled())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (GameControl.Instance.GetPlayerHealth() == GameControl.Instance.playerMaxHealth && can_throw)
                {
                    WeaponThrust(main);
                }
                else
                {
                    WeaponSwing(main);
                }

                in_delay = true;
                //GameControl.Instance.SetPlayerControl(false);
                StartCoroutine(restore_delay());
            }

            if (Input.GetButtonDown("Fire2"))
            {
                if (GameControl.Instance.GetPlayerHealth() == GameControl.Instance.playerMaxHealth)
                {
                    WeaponThrust(alt);
                }
                else
                {
                    WeaponSwing(alt);
                }

                if (alt != 4)
                {
                    in_delay = true;
                    //GameControl.Instance.SetPlayerControl(false);
                    StartCoroutine(restore_delay());
                }
            }
        }
    }

    IEnumerator restore_delay()
    {
        yield return new WaitForSeconds(0.2f);
        in_delay = false;
        //GameControl.Instance.SetPlayerControl(true);
    }

    void WeaponSwing(int type)
    {
        _weapon_position = transform.position;
        _weapon_position += _weapon_position_shift;
        Quaternion _weapon_direction = Quaternion.identity;
        _weapon_direction.eulerAngles = new Vector3(0, 0, _weapon_direction_z);

        // SWORD 
        if (type == 0)
        {
            GameObject my_weapon = Instantiate(sword, _weapon_position, _weapon_direction) as GameObject;
            my_weapon.GetComponent<Rigidbody>().constraints =
                RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            Destroy(my_weapon, 0.2f);
            AudioSource.PlayClipAtPoint(sword_use_sound_clip, Camera.main.transform.position);
        }

        // BOW
        else if (type == 3 && can_throw)
        {
            if (GameControl.Instance.GetPlayerRupees() > 0)
            {
                if (!GameControl.Instance.isInvinsible) GameControl.Instance.UpdatePlayerRupees(-1);
                can_throw = false;
                GameObject my_weapon = Instantiate(bow, _weapon_position, _weapon_direction) as GameObject;
                my_weapon.GetComponent<Rigidbody>().velocity = _weapon_position_shift * 6;
                Destroy(my_weapon, 2f);
                StartCoroutine(resetCanThrow(2f));
            }
        }

        // BOOMERANG

        else if (type == 2 && can_throw)
        {
            can_throw = false;
            GameObject my_weapon = Instantiate(boomerang, _weapon_position, _weapon_direction) as GameObject;
        }

        // BOMB

        else if (type == 1 && can_throw)
        {
            can_throw = false;
            Vector3 bomb_position_adjustment = new Vector3(0, -0.5f, 0);
            GameObject my_weapon =
                Instantiate(bomb, _weapon_position + bomb_position_adjustment, Quaternion.identity) as GameObject;
        }

        // HOOK

        else if (type == 4)
        {
            AudioSource.PlayClipAtPoint(hook_use_sound_clip, Camera.main.transform.position);
            StartCoroutine(ThrowingHook());
        }
    }

    // The player will be frozen when the hook is thrown out and not had not come back yet
    IEnumerator ThrowingHook()
    {
        GameControl.Instance.SetPlayerControl(false);
        Quaternion weaponDirection = Quaternion.identity;
        weaponDirection.eulerAngles = new Vector3(0, 0, _weapon_direction_z - 90f);
        GameObject tmpHook = Instantiate(hook, transform.position, weaponDirection);
        tmpHook.GetComponent<Rigidbody>().velocity =
            _weapon_position_shift * tmpHook.GetComponent<HookControl>().speed;
        tmpHook.GetComponent<HookControl>().UpdatePlayerPosition(transform.position);
        while (tmpHook != null)
        {
            yield return null;
        }

        GameControl.Instance.SetPlayerControl(true);
        yield return null;
    }

    void WeaponThrust(int type)
    {
        _weapon_position = transform.position;
        _weapon_position += _weapon_position_shift;
        Quaternion _weapon_direction = Quaternion.identity;
        _weapon_direction.eulerAngles = new Vector3(0, 0, _weapon_direction_z);

        // SWORD:
        if (type == 0)
        {
            can_throw = false;
            GameObject my_weapon = Instantiate(sword, _weapon_position, _weapon_direction) as GameObject;
            my_weapon.GetComponent<Rigidbody>().velocity = _weapon_position_shift * 6;
            my_weapon.GetComponent<SwordControl>()._isFlying = true;
            AudioSource.PlayClipAtPoint(sword_use_sound_clip, Camera.main.transform.position);
            AudioSource.PlayClipAtPoint(sword_glow_sound_clip, Camera.main.transform.position);
            //SWORD SPLIT INTO FOUR PIECES AFETER COLLISION
            //my_weapon.GetComponent<SwordControl>().SwordSplit();
        }

        // OTHER WEAPONS:
        else
        {
            WeaponSwing(type);
        }
    }


    void UpdateDirection()
    {
        float _vertical_movement = Input.GetAxisRaw("Vertical");
        float _horizontal_movement = Input.GetAxisRaw("Horizontal");
        // LEFT
        if (_horizontal_movement < 0)
        {
            _weapon_direction_z = 0;
            _weapon_position_shift = new Vector3(-1.0f, 0.0f, 0.0f);
        }
        // UP
        else if (_vertical_movement > 0)
        {
            _weapon_direction_z = 270;
            _weapon_position_shift = new Vector3(0.0f, 1.0f, 0.0f);
        }
        // RIGHT
        else if (_horizontal_movement > 0)
        {
            _weapon_direction_z = 180;
            _weapon_position_shift = new Vector3(1.0f, 0.0f, 0.0f);
        }
        // DOWN
        else if (_vertical_movement < 0)
        {
            _weapon_direction_z = 90;
            _weapon_position_shift = new Vector3(0.0f, -1.0f, 0.0f);
        }
    }


    IEnumerator resetCanThrow(float time)
    {
        yield return new WaitForSeconds(time);
        can_throw = true;
    }
}