using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangControl : MonoBehaviour
{


    public float rot_speed = 5f;
    public float speed = 2.0f;
    public float max_distance = 5.0f;

    private Rigidbody rb;
    private Transform player_transform;
    private bool back = false;
    private float min_distance = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        player_transform = GameControl.Instance.player.transform;

        // UPDATE ROTATION
        rb.transform.Rotate(new Vector3(0, 0, rot_speed));


        // UPDATE VELOCITY
        if (back)
        {
            Debug.Log("BOOMERANG RETURNING");
            rb.velocity = Vector3.Normalize(player_transform.position - rb.transform.position) * speed;
            if (Vector3.Distance(player_transform.position, rb.transform.position) < 0.2f)
            {
                Destroy(this.gameObject);
            }
        }
        else if (Vector3.Distance(player_transform.position, rb.transform.position) <= max_distance)
        {
            rb.velocity = Vector3.Normalize(-player_transform.position + rb.transform.position) * speed;
        }
        else if (Vector3.Distance(player_transform.position, rb.transform.position) > max_distance)
        {
            back = true;
        }
    }
}
