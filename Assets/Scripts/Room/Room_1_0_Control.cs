using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_1_0_Control : RoomControl
{

    public GameObject monster_1_0;
    public GameObject collection_1_0;
    public AudioClip collection_1_0_appear_sound_clip;

    private bool collection_generated;

    private void Start()
    {
        collection_generated = false;
    }

    public override void InitEnemies()
    {
        
        GameObject keese_1 = Instantiate(monster_1_0, new Vector3(18, 6, 0), Quaternion.identity) as GameObject;
        GameObject keese_2 = Instantiate(monster_1_0, new Vector3(23, 3, 0), Quaternion.identity) as GameObject;
        GameObject keese_3 = Instantiate(monster_1_0, new Vector3(19, 4, 0), Quaternion.identity) as GameObject;
        _enemies.Add(keese_1);
        _enemies.Add(keese_2);
        _enemies.Add(keese_3);
    }

    private void Update()
    {
        if (CheckAllDead() && !collection_generated && _isPlayerInRoom)
        {
            collection_generated = true;
            AudioSource.PlayClipAtPoint(collection_1_0_appear_sound_clip, Camera.main.transform.position);
            GameObject key = Instantiate(collection_1_0, new Vector3(18, 6, 0), Quaternion.identity) as GameObject;
        }
        
        CheckReset();
    }


 


}
