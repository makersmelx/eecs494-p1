using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_3_3_Control : RoomControl
{
    public GameObject monster_3_3;
    public GameObject drop_rupee;

    public GameObject collection_3_3_boomerang;
    public AudioClip collection_3_3_appear_sound_clip;



    private bool collection_generated = false;
    private bool has_spawned = false;


    public override void InitEnemies()
    {
        if (!has_spawned)
        {
            GameObject goriya_1 = Instantiate(monster_3_3, new Vector3(53, 35, 0), Quaternion.identity) as GameObject;
            GameObject goriya_2 = Instantiate(monster_3_3, new Vector3(58, 41, 0), Quaternion.identity) as GameObject;
            GameObject goriya_3 = Instantiate(monster_3_3, new Vector3(56, 41, 0), Quaternion.identity) as GameObject;
            _enemies.Add(goriya_1);
            _enemies.Add(goriya_2);
            _enemies.Add(goriya_3);
            goriya_1.GetComponent<HasHealth>().drop_item = drop_rupee;
            has_spawned = true;

        }
  

    }

    private void Update()
    {
        if (CheckAllDead() && !collection_generated && _isPlayerInRoom)
        {
            collection_generated = true;
            AudioSource.PlayClipAtPoint(collection_3_3_appear_sound_clip, Camera.main.transform.position);
            Instantiate(collection_3_3_boomerang, new Vector3(55.5f, 38f, 0), Quaternion.identity);
        }
        CheckReset();
    }
}
