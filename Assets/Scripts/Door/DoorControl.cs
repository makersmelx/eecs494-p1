using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private int _gridNumber;
    public bool locked = false;
    public bool triggered = false;
    public GameObject trigger;
    private Door[] _allDoors;

    void Start()
    {
        Transform[] allTransform = GetComponentsInChildren<Transform>();
        _gridNumber = allTransform.Length;
        _allDoors = GetComponentsInChildren<Door>();
        foreach (Door itr in _allDoors)
        {
            itr.gameObject.layer = locked || triggered ? 0 : 6;
        }
    }

    // This is the door that uses the key to open
    public void TryOpenLockedDoor(Collision other)
    {
        if (trigger == null && locked)
        {
            GameObject otherGameObject = other.gameObject;
            if (otherGameObject.CompareTag("Player"))
            {
                Inventory playerInventory = otherGameObject.GetComponent<Inventory>();
                if (playerInventory != null && playerInventory.GetKeys() > 0)
                {
                    locked = false;
                    playerInventory.AlterKeys(-1);
                    // unlock all doors
                    UnLockDoors();
                }
            }
        }
    }


    public void UnLockDoors()
    {
        foreach (Door door in _allDoors)
        {
            door.gameObject.layer = 6;
            door.gameObject.GetComponent<Door>().UnLockChangeSprite();
        }
    }

    public void ResetDoors()
    {
        if (triggered)
        {
            foreach (Door door in _allDoors)
            {
                door.ResetDoor();
                door.gameObject.layer = triggered ? 0 : 6;
            }
        }
    }
}