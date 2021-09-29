using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_3_2_Control : RoomControl
{

    // (49, 23)

    public GameObject monster_3_2;


    private bool[] collection_generated = { false, false };

    private void Start()
    {
    }

    public override void InitEnemies()
    {
        GameObject keese_1 = Instantiate(monster_3_2, new Vector3(50, 24, 0), Quaternion.identity) as GameObject;
        GameObject keese_2 = Instantiate(monster_3_2, new Vector3(52, 27, 0), Quaternion.identity) as GameObject;
        GameObject keese_3 = Instantiate(monster_3_2, new Vector3(60, 25, 0), Quaternion.identity) as GameObject;
        GameObject keese_4 = Instantiate(monster_3_2, new Vector3(58, 24, 0), Quaternion.identity) as GameObject;
        GameObject keese_5 = Instantiate(monster_3_2, new Vector3(50, 30, 0), Quaternion.identity) as GameObject;
        GameObject keese_6 = Instantiate(monster_3_2, new Vector3(54, 27, 0), Quaternion.identity) as GameObject;
        GameObject keese_7 = Instantiate(monster_3_2, new Vector3(53, 28, 0), Quaternion.identity) as GameObject;
        GameObject keese_8 = Instantiate(monster_3_2, new Vector3(61, 29, 0), Quaternion.identity) as GameObject;
        _enemies.Add(keese_1);
        _enemies.Add(keese_2);
        _enemies.Add(keese_3);
        _enemies.Add(keese_4);
        _enemies.Add(keese_5);
        _enemies.Add(keese_6);
        _enemies.Add(keese_7);
        _enemies.Add(keese_8);



    }

    private void Update()
    {
        CheckReset();
    }
}
