using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room13Control : RoomControl
{
    public GameObject monster_1_3;

    // Start is called before the first frame update
    public override void ResetBlocksAndUI()
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


    public override void InitEnemies()
    {
        GameObject gel_1 = Instantiate(monster_1_3, new Vector3(26, 36, 0), Quaternion.identity) as GameObject;
        GameObject gel_2 = Instantiate(monster_1_3, new Vector3(25, 40, 0), Quaternion.identity) as GameObject;
        GameObject gel_3 = Instantiate(monster_1_3, new Vector3(20, 35, 0), Quaternion.identity) as GameObject;

        _enemies.Add(gel_1);
        _enemies.Add(gel_2);
        _enemies.Add(gel_3);
    }

    private void Update()
    {
        CheckReset();
    }
}