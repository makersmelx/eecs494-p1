using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_2_5_Control : RoomControl
{
    public GameObject monster_3_3;

    public GameObject collection_2_5_key;
    public AudioClip collection_2_5_appear_sound_clip;



    private bool collection_generated = false;
    private bool has_spawned = false;


    public override void InitEnemies()
    {
        if (!has_spawned)
        {
            GameObject goriya_1 = Instantiate(monster_3_3, new Vector3(36, 63, 0), Quaternion.identity) as GameObject;
            GameObject goriya_2 = Instantiate(monster_3_3, new Vector3(40, 61, 0), Quaternion.identity) as GameObject;
            GameObject goriya_3 = Instantiate(monster_3_3, new Vector3(42, 57, 0), Quaternion.identity) as GameObject;
            _enemies.Add(goriya_1);
            _enemies.Add(goriya_2);
            _enemies.Add(goriya_3);
            has_spawned = true;

        }


    }

    private void Update()
    {
        if (CheckAllDead() && !collection_generated && _isPlayerInRoom)
        {
            collection_generated = true;
            AudioSource.PlayClipAtPoint(collection_2_5_appear_sound_clip, Camera.main.transform.position);
            GameObject key = Instantiate(collection_2_5_key, new Vector3(40f, 62f, 0), Quaternion.identity) as GameObject;
        }
        CheckReset();
    }
}
