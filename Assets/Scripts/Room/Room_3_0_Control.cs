using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_3_0_Control : RoomControl
{
    public GameObject monster_3_0;
    public GameObject drop_key;
    public GameObject drop_rupee;

    private bool[] collection_generated = { false, false, false };

    private void Start()
    {
    }

    public override void InitEnemies()
    {
        GameObject staflo_1 = Instantiate(monster_3_0, new Vector3(51, 3, 0), Quaternion.identity) as GameObject;
        GameObject staflo_2 = Instantiate(monster_3_0, new Vector3(52, 7, 0), Quaternion.identity) as GameObject;
        GameObject staflo_3 = Instantiate(monster_3_0, new Vector3(54, 6, 0), Quaternion.identity) as GameObject;
        GameObject staflo_4 = Instantiate(monster_3_0, new Vector3(59, 3, 0), Quaternion.identity) as GameObject;
        GameObject staflo_5 = Instantiate(monster_3_0, new Vector3(61, 4, 0), Quaternion.identity) as GameObject;
        _enemies.Add(staflo_1);
        _enemies.Add(staflo_2);
        _enemies.Add(staflo_3);
        _enemies.Add(staflo_4);
        _enemies.Add(staflo_5);

        staflo_1.GetComponent<HasHealth>().drop_item = drop_key;
        staflo_2.GetComponent<HasHealth>().drop_item = drop_rupee;
        staflo_5.GetComponent<HasHealth>().drop_item = drop_rupee;
     
    }

    private void Update()
    {
        CheckReset();
    }


}
