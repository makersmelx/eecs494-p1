using UnityEngine;

public class HogControl : MonoBehaviour
{
    
    public GameObject hook_thrown;
    public AudioClip hook_thrown_sound_clip;

    private Rigidbody rb;
    private SpriteRenderer spr;
    private float move_speed = 2.0f;
    private float move_counter = 0;
    private float move_offset = 3.0f;

    private float move_anim_offset = 0.2f;
    private float move_anim_counter = 0;

    private float stop_offset = 0.8f;
    private float stop_counter = 0;
    private float stop_anim_offset = 0.1f;


    private bool in_hooking = false;
    private bool in_second_stage = false;


    public Sprite hog_hook_sprite;
    public Sprite hog_up_sprite;
    public Sprite hog_down_sprite;
    private Sprite hog_init_sprite;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spr = GetComponent<SpriteRenderer>();
        hog_init_sprite = spr.sprite;
        hook_thrown.SetActive(false);
    }

    void Update()
    {
        Move();
    }



    void Move()
    {
        Vector3 player_pos = GameControl.Instance.player.transform.position;
    
        if (!in_hooking)
        {
            move_counter += Time.deltaTime;

            // MOVING
            if (move_counter < move_offset)
            {
                rb.velocity = Vector3.Normalize(new Vector3(player_pos.x - transform.position.x + 0.8f, 0, 0)) * move_speed;
                MoveAnim();
               
            }

            // STOP
            else if (move_counter < move_offset + stop_offset)
            {           
                Stop();
            }
            
            // HOOK
            else
            {
                Hook();
                in_hooking = true;
           
            }
        }

    }

    void MoveAnim()
    {
        move_anim_counter += Time.deltaTime;
        if (move_anim_counter < move_anim_offset)
        {
            this.GetComponent<SpriteRenderer>().sprite = hog_up_sprite;
        }
        else if (move_anim_counter < 2 * move_anim_offset)
        {
            this.GetComponent<SpriteRenderer>().sprite = hog_down_sprite;
        }
        else
        {
            move_anim_counter = 0;
        }
    }

    void Stop()
    { 
        rb.velocity = Vector3.zero;
        stop_counter += Time.deltaTime;
        if (stop_counter < stop_anim_offset)
        {
            this.GetComponent<SpriteRenderer>().sprite = hog_up_sprite;
        }
        else if (stop_counter < 2 * stop_anim_offset)
        {
            this.GetComponent<SpriteRenderer>().sprite = hog_down_sprite;
        } else
        {
            stop_counter = 0;
        }
    }

    void Hook()
    {
        AudioSource.PlayClipAtPoint(hook_thrown_sound_clip, Camera.main.transform.position);
        this.GetComponent<SpriteRenderer>().sprite = hog_hook_sprite;
        hook_thrown.SetActive(true);
        hook_thrown.GetComponent<HogHookControl>().SetHookActive();
        
    }

    public void ResetHook()
    { 
        in_hooking = false;
        hook_thrown.SetActive(false);
        spr.sprite = hog_init_sprite;
        move_counter = 0;
    }





}
