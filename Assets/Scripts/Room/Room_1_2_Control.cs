using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_1_2_Control : RoomControl
{
    // (16, 22)
    public GameObject monster_3_2;


    private bool[] collection_generated = {false, false};

    private void Start()
    {
    }

    public override void InitEnemies()
    {
        GameObject keese_1 = Instantiate(monster_3_2, new Vector3(18, 24, 0), Quaternion.identity) as GameObject;
        GameObject keese_2 = Instantiate(monster_3_2, new Vector3(20, 27, 0), Quaternion.identity) as GameObject;
        GameObject keese_3 = Instantiate(monster_3_2, new Vector3(22, 25, 0), Quaternion.identity) as GameObject;
        GameObject keese_4 = Instantiate(monster_3_2, new Vector3(24, 24, 0), Quaternion.identity) as GameObject;
        GameObject keese_5 = Instantiate(monster_3_2, new Vector3(25, 26, 0), Quaternion.identity) as GameObject;
        GameObject keese_6 = Instantiate(monster_3_2, new Vector3(26, 27, 0), Quaternion.identity) as GameObject;
        _enemies.Add(keese_1);
        _enemies.Add(keese_2);
        _enemies.Add(keese_3);
        _enemies.Add(keese_4);
        _enemies.Add(keese_5);
        _enemies.Add(keese_6);
    }

    private void Update()
    {
        CheckReset();
    }

    public override void ResetBlocksAndUI()
    {
        foreach (DoorControl doorControl in GetComponentsInChildren<DoorControl>())
        {
            doorControl.ResetDoors();
        }
    }
}