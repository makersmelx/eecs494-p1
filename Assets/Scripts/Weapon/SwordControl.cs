using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour
{
    public GameObject sword_1;
    public GameObject sword_2;
    public GameObject sword_3;
    public GameObject sword_4;

    public bool _isFlying = false;
    public float _sword_split_speed = 1.5f;


    public Sprite sword_init_sprite;
    public Sprite sword_glow_sprite;

    private int type = 0;

    private float glow_delay = 0.1f;
    private float glow_counter = 0;

    private void Update()
    {
        if (_isFlying && checkRoomBoundary())
        {
            if (type == 0) SwordSplit();
            Destroy(this.gameObject, 0.0f);
            GameControl.Instance.player.GetComponent<WeaponControl>().can_throw = true;
            return;
        }
        if (_isFlying)
        {
            // ===================BEGINNING OF SWORD GLOWING EFFECT================
            glow_counter += Time.deltaTime;
            if (glow_counter < glow_delay)
            {
                this.GetComponent<SpriteRenderer>().sprite = sword_init_sprite;
            }
            else if (glow_counter < 2 * glow_delay)
            {
                this.GetComponent<SpriteRenderer>().sprite = sword_glow_sprite;
            } else
            {
                glow_counter = 0;
            }
            // =====================END OF SWORD GLOWING EFFECT=====================
        }
    }

    public void SetType(int new_type)
    {
        type = new_type;
    }

    private bool checkRoomBoundary()
    {
        float offset = 2f;
        return (transform.position.x < GameControl.Instance.leftBound + offset ||
                transform.position.x > GameControl.Instance.rightBound - offset  ||
                transform.position.y < GameControl.Instance.downBound ||
                transform.position.y > GameControl.Instance.upBound - offset - 1.5f);
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("WEAPON COLLISION DETECTED");
        if (_isFlying && other.gameObject.CompareTag("Monster"))
        {
            if (type == 0) SwordSplit();
            _isFlying = false;
            Destroy(this.gameObject, 0.0f);
            GameControl.Instance.player.GetComponent<WeaponControl>().can_throw = true;
        }
    }


    

    IEnumerator changeGlowSword(GameObject sword)
    {
        yield return new WaitForSeconds(0.1f);
        sword.GetComponent<SpriteRenderer>().sprite = sword_glow_sprite;
        StartCoroutine(restoreGlowSword(sword));
    }

    IEnumerator restoreGlowSword(GameObject sword)
    {
        yield return new WaitForSeconds(0.1f);
        sword.GetComponent<SpriteRenderer>().sprite = sword_init_sprite;

    }






    public void SwordSplit()
    {

        float destroy_time = 0.5f;
        Vector3 current_position = transform.position;
        float scale_shift = 0.5f;
        Vector3 left_shift = new Vector3(-1.0f, 1.0f, 0.0f) * scale_shift;
        Vector3 up_shift = new Vector3(1.0f, 1.0f, 0.0f) * scale_shift;
        Vector3 right_shift = new Vector3(1.0f, -1.0f, 0.0f) * scale_shift;
        Vector3 down_shift = new Vector3(-1.0f, -1.0f, 0.0f) * scale_shift;

        Quaternion split_direction = Quaternion.identity;
        split_direction.eulerAngles = Vector3.zero;
   

        GameObject left_sword = Instantiate(sword_1, current_position + left_shift, split_direction) as GameObject;
        GameObject up_sword = Instantiate(sword_2, current_position + up_shift, split_direction) as GameObject;
        GameObject right_sword = Instantiate(sword_3, current_position + right_shift, split_direction) as GameObject;
        GameObject down_sword = Instantiate(sword_4, current_position + down_shift, split_direction) as GameObject;

        left_sword.GetComponent<Rigidbody>().velocity = Vector3.Normalize(left_shift) * _sword_split_speed;
        up_sword.GetComponent<Rigidbody>().velocity = Vector3.Normalize(up_shift) * _sword_split_speed;
        right_sword.GetComponent<Rigidbody>().velocity = Vector3.Normalize(right_shift) * _sword_split_speed;
        down_sword.GetComponent<Rigidbody>().velocity = Vector3.Normalize(down_shift) * _sword_split_speed;

        Destroy(left_sword.gameObject, destroy_time);
        Destroy(up_sword.gameObject, destroy_time);
        Destroy(right_sword.gameObject, destroy_time);
        Destroy(down_sword.gameObject, destroy_time);
    }
}