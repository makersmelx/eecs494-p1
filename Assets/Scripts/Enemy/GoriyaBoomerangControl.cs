using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriyaBoomerangControl : MonoBehaviour
{
    public GameObject player;
    private float rot_speed = 5f;
    private float speed = 7.0f;
    private float max_distance = 5.0f;
    private Rigidbody rb;
    private Transform player_transform;
    private float min_distance = 0.2f;
    private Vector3 init_position;
    private bool isReturning;
    private float damage = 0.5f;

    private int hit_time = 0;


    // Start is called before the first frame update
    void Start()
    {

        if (player == null)
        {
            Destroy(this.gameObject);
        }

        rb = GetComponent<Rigidbody>();
        player_transform = player.transform;
        init_position = player_transform.position;
        isReturning = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (player == null)
        {
            Destroy(this.gameObject);
        }

        // UPDATE ROTATION
        rb.transform.Rotate(new Vector3(0, 0, rot_speed));


        // UPDATE VELOCITY
        if (isReturning)
        {
            rb.velocity = Vector3.Normalize(player_transform.position - rb.transform.position) * speed;
            if (Vector3.Distance(rb.transform.position, player_transform.position) <= min_distance)
            {
                Destroy(this.gameObject);
                player.GetComponent<GoriyaControl>().boomerang_fetched();
                
            }
        }
        else
        {
            rb.velocity = Vector3.Normalize(rb.transform.position - init_position) * speed;
        }

        if (Vector3.Distance(init_position, rb.transform.position) >= max_distance)
        {
            isReturning = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            // Debug.Log("GORIYAS BOOMERANG HIT PLAYER!");
            if (hit_time == 0 || (hit_time <= 1 && isReturning))
            {
                hit_time += 1;
                other.GetComponent<PlayerControl>().playerKnockBack(-40f);
                GameControl.Instance.AlterHealth(-damage);
                
            }
          
        }
    }



}
