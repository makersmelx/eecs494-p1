using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    public List<GameObject> _enemies = new List<GameObject>();

    protected bool _isPlayerInRoom;

    public bool _reset;

    public bool _spawned;

    // Start is called before the first frame update
    void Start()
    {
        _isPlayerInRoom = false;
        _reset = true;
        _spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetRoom()
    {
        ResetBlocksAndUI();
        FarewellPlayer();
    }

    public virtual void ResetBlocksAndUI()
    {
    }

    public virtual void InitEnemies()
    {
    }

    public void WelComePlayer()
    {
        if (!_isPlayerInRoom)
        {
            //Debug.Log("PLAYER ENTERED ROOM");
            _isPlayerInRoom = true;
            if (!_spawned)
            {
                _spawned = true;
                InitEnemies();
            }
        }
    }

    public void FarewellPlayer()
    {
        if (_isPlayerInRoom)
        {
            _isPlayerInRoom = false;
        }
    }

    public static bool IsPlayerInRoom(GameObject currentTile)
    {
        RoomControl rc = currentTile.GetComponentInParent<RoomControl>();
        if (rc != null)
        {
            return rc._isPlayerInRoom;
        }
        else
        {
            return false;
        }
    }

    public static void WelcomePlayerIntoRoom(GameObject currentTile)
    {
        RoomControl rc = currentTile.GetComponentInParent<RoomControl>();
        if (rc != null)
        {
            rc.WelComePlayer();
        }
    }

    public static void ResetRoomOfGameObject(GameObject gb)
    {
        RoomControl rc = gb.GetComponentInParent<RoomControl>();
        if (rc != null)
        {
            rc.ResetRoom();
        }
    }


    public void FreezeAllEnemies()
    {
        if (_enemies == null)
        {
            return;
        }

        foreach (GameObject enemy in _enemies)
        {
            enemy.SetActive(false);
        }
    }


    public void UnFreezeAllEnemies()
    {
        foreach (GameObject enemy in _enemies)
        {
            enemy.SetActive(true);
        }
    }

    public bool CheckAllDead()
    {
        foreach (GameObject monster in _enemies)
        {
            if (monster != null)
            {
                return false;
            }
        }

        return true;
    }

    public void CheckReset()
    {
        // USED IF THE MONSTERS CAN REBORN WHEN PLAYER RE-ENTER THE ROOM

        if (_isPlayerInRoom && !_reset)
        {
            _reset = true;
            return;
        }
        /*
        if (_reset && !_isPlayerInRoom)
        {
            _reset = false;
            foreach (GameObject monster in _enemies)
            {
                Destroy(monster);
            }
            _enemies.Clear();
        }
        */
    }

    public static bool CheckAllEnemyDeadInRoom(GameObject currentTile)
    {
        RoomControl rc = currentTile.GetComponentInParent<RoomControl>();
        if (rc != null)
        {
            return rc.CheckAllDead();
        }

        return false;
    }
}