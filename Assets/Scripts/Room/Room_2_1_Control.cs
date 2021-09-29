using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_2_1_Control : RoomControl
{
    public GameObject monster_2_1;
    public GameObject drop_advrupee;
   

    private bool[] collection_generated = { false, false };

    private void Start()
    {
    }

    public override void InitEnemies()
    {
        GameObject staflo_1 = Instantiate(monster_2_1, new Vector3(36, 14, 0), Quaternion.identity) as GameObject;
        GameObject staflo_2 = Instantiate(monster_2_1, new Vector3(38, 18, 0), Quaternion.identity) as GameObject;
        GameObject staflo_3 = Instantiate(monster_2_1, new Vector3(43, 17, 0), Quaternion.identity) as GameObject;
        _enemies.Add(staflo_1);
        _enemies.Add(staflo_2);
        _enemies.Add(staflo_3);

        staflo_1.GetComponent<HasHealth>().drop_item = drop_advrupee;
        staflo_2.GetComponent<HasHealth>().drop_item = drop_advrupee;

    }

    private void Update()
    {
        CheckReset();
    }
}