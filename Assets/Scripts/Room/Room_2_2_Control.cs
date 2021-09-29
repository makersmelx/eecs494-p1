using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_2_2_Control : RoomControl
{

    // x in [33,46], y in [23,31]
    // walls in (32 + 4or6, 22 + 4or6)
    public GameObject monster_2_2;
    public GameObject drop_advrupee;
    public GameObject drop_heart;

    public GameObject collection_2_2_key;
    public AudioClip collection_2_2_appear_sound_clip;



    private bool collection_generated = false;

   

    public override void InitEnemies()
    {
        
        GameObject staflo_1 = Instantiate(monster_2_2, new Vector3(36, 25, 0), Quaternion.identity) as GameObject;
        GameObject staflo_2 = Instantiate(monster_2_2, new Vector3(42, 25, 0), Quaternion.identity) as GameObject;
        GameObject staflo_3 = Instantiate(monster_2_2, new Vector3(43, 30, 0), Quaternion.identity) as GameObject;
        GameObject staflo_4 = Instantiate(monster_2_2, new Vector3(35, 28, 0), Quaternion.identity) as GameObject;
        GameObject staflo_5 = Instantiate(monster_2_2, new Vector3(40, 28, 0), Quaternion.identity) as GameObject;
        _enemies.Add(staflo_1);
        _enemies.Add(staflo_2);
        _enemies.Add(staflo_3);
        _enemies.Add(staflo_4);
        _enemies.Add(staflo_5);

        staflo_1.GetComponent<HasHealth>().drop_item = drop_advrupee;
        staflo_3.GetComponent<HasHealth>().drop_item = drop_advrupee;
        staflo_5.GetComponent<HasHealth>().drop_item = drop_heart;

    }

    private void Update()
    {
        if (CheckAllDead() && !collection_generated && _isPlayerInRoom)
        {
            collection_generated = true;
            AudioSource.PlayClipAtPoint(collection_2_2_appear_sound_clip, Camera.main.transform.position);
            GameObject key = Instantiate(collection_2_2_key, new Vector3(39.5f, 27f, 0), Quaternion.identity) as GameObject;
        }
        CheckReset();
    }

}
