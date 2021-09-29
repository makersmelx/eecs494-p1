using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Hog_Control : RoomControl
{
   

    Vector3[] fire_positions = { new Vector3(19, -7, 0), new Vector3(22, -7, 0), new Vector3(25, -7, 0), new Vector3(28, -7, 0) };


    public GameObject fire;
    public GameObject boss;

    private void Start()
    {
        boss.SetActive(false);
    }

    public override void InitEnemies()
    {
        boss.SetActive(true);
        InvokeRepeating("set_random_fire", 0.0f, 15.0f);
        
        
    }

    private void Update()
    {
        
    }

    void set_random_fire()
    {

 
            int random_not_generate = Random.Range(0, 4);
            for (int i = 0; i < 4; i++)
            {
                if (i != random_not_generate)
                {
                    Instantiate(fire, fire_positions[i], Quaternion.identity);
                  
                }

            }
        
    }
    
    /*
    private void Update()
    {
        fire_counter += Time.deltaTime;
        if (fire_counter > fire_offset)
        {
            fire_counter = 0;
            int random_not_generate = Random.Range(0, 4);
            for (int i = 0; i < 4; i++)
            {
                if (i != random_not_generate)
                {
                    GameObject tp_fire = Instantiate(fire, fire_positions[i], Quaternion.identity);
                    tp_fire.GetComponent<Fire>().goOut = true;
                }

            }
        }
    }
    */


}
