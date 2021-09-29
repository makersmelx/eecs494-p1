using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room15Control : RoomControl
{
    // Start is called before the first frame update
    public override void ResetBlocksAndUI()
    {
        foreach (DownStairsBowRoom downStairsBowRoom in GetComponentsInChildren<DownStairsBowRoom>())
        {
            downStairsBowRoom.ResetStairs();
        }

        foreach (DoorTrigger doorTrigger in GetComponentsInChildren<DoorTrigger>())
        {
            doorTrigger.ResetTrigger();
        }
    }
}