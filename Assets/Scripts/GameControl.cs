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
    public int weaponNumbers = 2;
    public Sprite[] sprites;

    public float upBound, downBound, leftBound, rightBound;

    public bool isInvinsible = false;

    private bool _godMode = false;
    private float _playerHealth;

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
    }

    private void Update()
    {
        GameOver();
        EnterGodMode();
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

    private void GameOver()
    {
        if (_playerHealth <= 0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public bool PlayerControlEnabled()
    {
        return AnimatorControl.Instance.IsMoveAnimation() && !moveCamera;
    }

    public void EnterGodMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _godMode = true;
            playerMaxHealth = 99;
            _playerHealth = playerMaxHealth;
            player.GetComponent<Inventory>().AlterRupees(99 - player.GetComponent<Inventory>().GetRupees());
            player.GetComponent<Inventory>().AlterKeys(99 - player.GetComponent<Inventory>().GetKeys());
            isInvinsible = true;
        }
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
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("SWITCH WEAPON DETECTED");
            weaponMain += 1;
            weaponMain %= weaponNumbers;
            weaponAlt += 1;
            weaponAlt %= weaponNumbers;
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
    
}