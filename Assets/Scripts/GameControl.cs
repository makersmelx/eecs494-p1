using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;

    public const float GridWidth = 16;
    public const float GridHeight = 11;

    public GameObject player;

    public bool moveCamera = false;
    public float playerMaxHealth = 3f;

    public int weaponMain, weaponAlt;
    private int weaponNumbers;
    private bool[] enabled_alt_weapon = {false, true, false, false, false};


    public Sprite[] sprites;

    public float upBound, downBound, leftBound, rightBound;

    public bool isInvinsible = false;

    private bool _godMode = false;
    private float _playerHealth;

    private bool _enablePlayerControl;

    private int _key_record, _rupee_record;
    private float _health_record;
    private bool[] _weapon_record = {false, false, false, false, false};

    private bool update_weapon = false;

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

    private void Start()
    {
        _playerHealth = playerMaxHealth;
        sprites = Resources.LoadAll<Sprite>("Zelda/TileSpriteSheet");
        UpdateBounds(new Vector2(39.5f, 6.5f));

        weaponMain = 0;
        weaponAlt = 1;
        weaponNumbers = 5;

        _enablePlayerControl = true;
    }

    private void Update()
    {
        GameOver();
        EnterGodMode();
        CustomLevel();
    }

    public void MoveCamera(Vector2 direction)
    {
        moveCamera = true;
        if (Camera.main != null)
        {
            Camera.main.GetComponent<CameraControl>().MoveCamera(direction);
        }

        moveCamera = false;
    }

    public void UpdateBounds(Vector2 cameraPosition)
    {
        upBound = cameraPosition.y + GridHeight / 2f;
        downBound = cameraPosition.y - GridHeight / 2f;
        leftBound = cameraPosition.x - GridWidth / 2f;
        rightBound = cameraPosition.x + GridWidth / 2f;
    }

    public bool IsOutOfBound(Transform tr)
    {
        return tr.position.x > rightBound
               || tr.position.x < leftBound
               || tr.position.y > upBound
               || tr.position.y < downBound;
    }

    private void GameOver()
    {
        if (_playerHealth <= 0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public bool PlayerControlEnabled()
    {
        return _enablePlayerControl && AnimatorControl.Instance.IsMoveAnimation() && !moveCamera;
    }

    public void SetPlayerControl(bool value)
    {
        _enablePlayerControl = value;
    }


    public void EnterGodMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !_godMode)
        {
            Debug.Log("ENTER GOD MODE");

            RecordStats();

            _godMode = true;

            playerMaxHealth = 99;
            _playerHealth = playerMaxHealth;
            player.GetComponent<Inventory>().AlterRupees(99 - player.GetComponent<Inventory>().GetRupees());
            player.GetComponent<Inventory>().AlterKeys(99 - player.GetComponent<Inventory>().GetKeys());
            for (int i = 1; i < weaponNumbers; i++)
            {
                enabled_alt_weapon[i] = true;
            }

            isInvinsible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("EXIT GOD MODE");

            playerMaxHealth = 3;
            isInvinsible = false;
            RestoreStats();
            _godMode = false;

            update_weapon = true;
            UpdateWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("ENTER DEBUGGER MODE CAN'T EXIT!!!!");

            RecordStats();

            _godMode = true;
            player.GetComponent<PlayerControl>().movementSpeed = 20f;
            Camera.main.GetComponent<CameraControl>().moveDuration = 0.2f;

            playerMaxHealth = 99;
            _playerHealth = playerMaxHealth;
            player.GetComponent<Inventory>().AlterRupees(99 - player.GetComponent<Inventory>().GetRupees());
            player.GetComponent<Inventory>().AlterKeys(99 - player.GetComponent<Inventory>().GetKeys());
            for (int i = 0; i < 4; i++)
            {
                enabled_alt_weapon[i] = true;
            }

            isInvinsible = true;
        }


        update_weapon = false;
    }

    public void AlterHealth(float alter)
    {
        if (!_godMode)
        {
            _playerHealth += alter;
            if (_playerHealth > playerMaxHealth)
            {
                _playerHealth = playerMaxHealth;
            }
        }
    }

    public float GetPlayerHealth()
    {
        return _playerHealth;
    }


    public void UpdateWeapon()
    {
        if (Input.GetButtonDown("Jump") || update_weapon)
        {
            for (int i = weaponAlt + 1; i < weaponNumbers; i++)
            {
                if (enabled_alt_weapon[i])
                {
                    weaponAlt = i;
                    return;
                }
            }

            for (int i = 0; i <= weaponAlt; i++)
            {
                if (enabled_alt_weapon[i])
                {
                    weaponAlt = i;
                    return;
                }
            }
        }
    }

    public void AddWeapon(string weapon_name)
    {
        if (weapon_name == "bow")
        {
            Debug.Log("BOW ENABLED");
            enabled_alt_weapon[3] = true;
            _weapon_record[3] = true;
        }

        if (weapon_name == "boomerang")
        {
            Debug.Log("BOOMERANG ENABLED");
            enabled_alt_weapon[2] = true;
            _weapon_record[2] = true;
        }

        if (weapon_name == "hook")
        {
            enabled_alt_weapon[4] = true;
            _weapon_record[4] = true;
            weaponAlt = 4;
        }
    }


    public int GetPlayerRupees()
    {
        return player.GetComponent<Inventory>().GetRupees();
    }

    public void UpdatePlayerRupees(int change)
    {
        player.GetComponent<Inventory>().AlterRupees(change);
    }


    private void RecordStats()
    {
        _health_record = _playerHealth;
        _key_record = player.GetComponent<Inventory>().GetKeys();
        _rupee_record = player.GetComponent<Inventory>().GetRupees();
        for (int i = 0; i < weaponNumbers; i++)
        {
            _weapon_record[i] = enabled_alt_weapon[i];
        }
    }

    private void RestoreStats()
    {
        _playerHealth = _health_record;
        player.GetComponent<Inventory>().AlterKeys(_key_record - player.GetComponent<Inventory>().GetKeys());
        player.GetComponent<Inventory>().AlterRupees(_rupee_record - player.GetComponent<Inventory>().GetRupees());
        for (int i = 0; i < weaponNumbers; i++)
        {
            enabled_alt_weapon[i] = _weapon_record[i];
        }
    }

    public void CustomLevel()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.transform.position = new Vector3(39.5f, -2f, 0);
            if (Camera.main is { })
            {
                Transform cameraTransform = Camera.main.transform;
                cameraTransform.position = new Vector3(39.5f, -4.5f, -34.15f);
                UpdateBounds(cameraTransform.position);
            }
        }
    }
}