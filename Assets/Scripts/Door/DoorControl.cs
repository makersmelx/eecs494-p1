using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private int _gridNumber;
    public bool locked = false;

    void Start()
    {
        Transform[] allTransform = GetComponentsInChildren<Transform>();
        _gridNumber = allTransform.Length;
        BoxCollider[] allBoxCollider = GetComponentsInChildren<BoxCollider>();
        Door[] allDoors = GetComponentsInChildren<Door>();

        foreach (BoxCollider itr in allBoxCollider)
        {
            itr.gameObject.layer = locked ? 0 : 6;
        }
    }

    // Update is called once per frame
    public void TryOpenLockedDoor(Collision other)
    {
        if (locked)
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
                    Door[] allDoors = GetComponentsInChildren<Door>();
                    foreach (Door door in allDoors)
                    {
                        UnLockDoor(door.gameObject);
                    }
                }
            }
        }
    }

    public void UnLockDoor(GameObject doorGameObject)
    {
        doorGameObject.layer = 6;
        doorGameObject.GetComponent<Door>().UnLockChangeSprite();
    }
}