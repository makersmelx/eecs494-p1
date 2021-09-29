using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_4_3_Control : RoomControl { 


    public GameObject drop_advrupee;
    public GameObject drop_heart;

    public GameObject collectio_key_4_3;

    public GameObject monster_4_3;


    public AudioClip wallmaster_move_sound_clip;



    private bool collection_generated = false;



    public override void InitEnemies()
    {
        StartCoroutine(delayed_spawn_0());
        StartCoroutine(delayed_spawn_1());
        StartCoroutine(delayed_spawn_2());
        if (!collection_generated)
        {
            Instantiate(collectio_key_4_3, new Vector3(73, 35, 0), Quaternion.identity);
        }
    }

    IEnumerator delayed_spawn_0()
    {
        playSound();
        yield return new WaitForSeconds(2.0f);

        if (_isPlayerInRoom)
        {
            playSound();
            GameObject wallmaster_1 = Instantiate(monster_4_3, new Vector3(66, 33, 0), Quaternion.identity) as GameObject;
            GameObject wallmaster_2 = Instantiate(monster_4_3, new Vector3(66, 31, 0), Quaternion.identity) as GameObject;
            GameObject wallmaster_3 = Instantiate(monster_4_3, new Vector3(76, 33, 0), Quaternion.identity) as GameObject;

            _enemies.Add(wallmaster_1);
            _enemies.Add(wallmaster_2);
            _enemies.Add(wallmaster_3);

            wallmaster_1.GetComponent<HasHealth>().drop_item = drop_advrupee;

        }

    }

    IEnumerator delayed_spawn_1()
    {
        yield return new WaitForSeconds(8.0f);

        if (_isPlayerInRoom)
        {
            playSound();

            GameObject wallmaster_4 = Instantiate(monster_4_3, new Vector3(65, 33, 0), Quaternion.identity) as GameObject;

            GameObject wallmaster_5 = Instantiate(monster_4_3, new Vector3(77, 31, 0), Quaternion.identity) as GameObject;

            wallmaster_4.GetComponent<HasHealth>().drop_item = drop_heart;
        }
        
    }

    IEnumerator delayed_spawn_2()
    {
        yield return new WaitForSeconds(12.0f);

        if (_isPlayerInRoom)
        {
            playSound();

            GameObject wallmaster_7 = Instantiate(monster_4_3, new Vector3(66, 33, 0), Quaternion.identity) as GameObject;

            GameObject wallmaster_8 = Instantiate(monster_4_3, new Vector3(74, 33, 0), Quaternion.identity) as GameObject;

            wallmaster_8.GetComponent<HasHealth>().drop_item = drop_advrupee;
        }

       
    }


    private void Update()
    { 
        CheckReset();
    }

    void playSound()
    {
        AudioSource.PlayClipAtPoint(wallmaster_move_sound_clip, Camera.main.transform.position);
    }
}
