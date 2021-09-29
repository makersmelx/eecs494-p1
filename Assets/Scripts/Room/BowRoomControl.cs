using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowRoomControl : RoomControl
{

    public GameObject monster_bow_room;


    private void Start()
    {
    }

    public override void InitEnemies()
    {

        GameObject keese_1 = Instantiate(monster_bow_room, new Vector3(13, 59, 0), Quaternion.identity) as GameObject;
        GameObject keese_2 = Instantiate(monster_bow_room, new Vector3(9, 59, 0), Quaternion.identity) as GameObject;
        GameObject keese_3 = Instantiate(monster_bow_room, new Vector3(6, 59, 0), Quaternion.identity) as GameObject;
        GameObject keese_4 = Instantiate(monster_bow_room, new Vector3(2, 59, 0), Quaternion.identity) as GameObject;

        _enemies.Add(keese_1);
        _enemies.Add(keese_2);
        _enemies.Add(keese_3);
        _enemies.Add(keese_4);

    }

    private void Update()
    {
        CheckReset();
    }
}
