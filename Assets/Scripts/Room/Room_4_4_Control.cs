using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_4_4_Control : RoomControl
{


    public GameObject boss_4_4;

    public GameObject collection_4_4_heart;



    private bool collection_generated = false;
    private bool has_spawned = false;


    public override void InitEnemies()
    {
        if (!has_spawned)
        {
            has_spawned = true;

            GameObject boss = Instantiate(boss_4_4, new Vector3(77, 49.5f, 0), Quaternion.identity) as GameObject;
            _enemies.Add(boss);
            boss.GetComponent<HasHealth>().drop_item = collection_4_4_heart;
      
        }


    }

    private void Update()
    {
        if (CheckAllDead() && _isPlayerInRoom)
        {
            // OPEN THE DOOR
        }
        CheckReset();
    }
}
