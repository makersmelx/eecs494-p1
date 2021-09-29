using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_2_4_Control : RoomControl {

    public GameObject monster_2_4;
    public GameObject drop_key;


    private void Start()
    {
    }

    public override void InitEnemies()
    {
        GameObject staflo_1 = Instantiate(monster_2_4, new Vector3(37, 48, 0), Quaternion.identity) as GameObject;
        GameObject staflo_2 = Instantiate(monster_2_4, new Vector3(35, 50, 0), Quaternion.identity) as GameObject;
        GameObject staflo_3 = Instantiate(monster_2_4, new Vector3(42, 51, 0), Quaternion.identity) as GameObject;
        _enemies.Add(staflo_1);
        _enemies.Add(staflo_2);
        _enemies.Add(staflo_3);

        staflo_1.GetComponent<HasHealth>().drop_item = drop_key;

    }

    private void Update()
    {
        CheckReset();
    }


}
