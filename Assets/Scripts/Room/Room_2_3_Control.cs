using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_2_3_Control : RoomControl
{

    public GameObject monster_2_3;


    private void Start()
    {
    }

    public override void InitEnemies()
    {

        GameObject gel_1 = Instantiate(monster_2_3, new Vector3(35.5f, 38, 0), Quaternion.identity) as GameObject;
        GameObject gel_2 = Instantiate(monster_2_3, new Vector3(40, 40, 0), Quaternion.identity) as GameObject;
        GameObject gel_3 = Instantiate(monster_2_3, new Vector3(34, 35.5f, 0), Quaternion.identity) as GameObject;
        GameObject gel_4 = Instantiate(monster_2_3, new Vector3(38, 36, 0), Quaternion.identity) as GameObject;
        GameObject gel_5 = Instantiate(monster_2_3, new Vector3(40, 39f, 0), Quaternion.identity) as GameObject;
    
        _enemies.Add(gel_1);
        _enemies.Add(gel_2);
        _enemies.Add(gel_3);
        _enemies.Add(gel_4);
        _enemies.Add(gel_5);

    }

    private void Update()
    {
        CheckReset();
    }
}
