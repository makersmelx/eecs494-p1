using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject sword;

    public GameObject bow;

    public GameObject boomerang;

    public GameObject bomb;

    //private float _damage = 2.0f;
    private float _weapon_direction_z = 0;
    private Vector3 _weapon_position_shift;
    private Vector3 _weapon_position;




    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        GameControl.Instance.UpdateWeapon();
        int main = GameControl.Instance.weaponMain;
        int alt = GameControl.Instance.weaponAlt;
        if (Input.GetButtonDown("Fire1"))
        {
            if (GameControl.Instance.GetPlayerHealth() == GameControl.Instance.playerMaxHealth)
            {
                WeaponThrust(main);
            }
            else
            {
                WeaponSwing(main);
            }
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
        }
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
            my_weapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            Destroy(my_weapon, 0.2f);
        }

        // BOW
        else if (type == 1)
        {
            if (GameControl.Instance.GetPlayerRupees() > 0)
            {
                if (!GameControl.Instance.isInvinsible) GameControl.Instance.UpdatePlayerRupees(-1);
                GameObject my_weapon = Instantiate(bow, _weapon_position, _weapon_direction) as GameObject;
                my_weapon.GetComponent<Rigidbody>().velocity = _weapon_position_shift * 4;
                my_weapon.GetComponent<SwordControl>().SetType(1);
                Destroy(my_weapon, 0.2f);
            }
        }

        // BOOMERANG

        else if (type == 2)
        {
            GameObject my_weapon = Instantiate(boomerang, _weapon_position, _weapon_direction) as GameObject;

        }

        // BOMB

        else if (type == 3)
        {
            GameObject my_weapon = Instantiate(bomb, _weapon_position, Quaternion.identity) as GameObject;
        }
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
            GameObject my_weapon = Instantiate(sword, _weapon_position, _weapon_direction) as GameObject;
            my_weapon.GetComponent<Rigidbody>().velocity = _weapon_position_shift * 4;
            my_weapon.GetComponent<SwordControl>()._isFlying = true;
            //SWORD SPLIT INTO FOUR PIECES AFETER COLLISION
            //my_weapon.GetComponent<SwordControl>().SwordSplit();
        }

        // BOW:
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







}