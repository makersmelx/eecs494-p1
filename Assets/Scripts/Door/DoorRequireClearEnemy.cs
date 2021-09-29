using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRequireClearEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _triggered;

    private void Start()
    {
        _triggered = false;
    }

    private void Update()
    {
        if (!_triggered && RoomControl.IsPlayerInRoom(gameObject) && RoomControl.CheckAllEnemyDeadInRoom(gameObject))
        {
            _triggered = true;
            GetComponentInParent<DoorControl>().UnLockDoors();
        }
    }

    public void ResetEnemyTrigger()
    {
        _triggered = false;
    }
}