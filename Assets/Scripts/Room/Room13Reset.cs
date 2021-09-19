using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room13Reset : RoomReset
{
    // Start is called before the first frame update
    public override void ResetRoom()
    {
        foreach (DoorControl doorControl in GetComponentsInChildren<DoorControl>())
        {
            doorControl.ResetDoors();
        }

        foreach (DoorTrigger doorTrigger in GetComponentsInChildren<DoorTrigger>())
        {
            doorTrigger.ResetTrigger();
        }
    }
}