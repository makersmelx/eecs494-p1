using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour
{
    public GameObject sword;
    public bool _isFlying = false;
    public float _sword_split_speed = 0.5f;

    private int type = 0;

    private void Update()
    {
        if (_isFlying && checkRoomBoundary())
        {
            //if (type == 0) SwordSplit();
            Destroy(this.gameObject, 0.0f);
        }
    }

    public void SetType(int new_type)
    {
        type = new_type;
    }

    private bool checkRoomBoundary()
    {
        return (transform.position.x < GameControl.Instance.leftBound ||
                transform.position.x > GameControl.Instance.rightBound ||
                transform.position.y < GameControl.Instance.downBound ||
                transform.position.y > GameControl.Instance.upBound);
    }

    void OnCollisionEnter()
    {
        Debug.Log("WEAPON COLLISION DETECTED");
        if (_isFlying)
        {
            if (type == 0) SwordSplit();
            _isFlying = false;
            Destroy(this.gameObject, 0.0f);
        }
    }

    public void SwordSplit()
    {
        Vector3 current_position = transform.position;
        float scale_shift = 1;
        Vector3 left_shift = new Vector3(-1.0f, 1.0f, 0.0f) * scale_shift;
        Vector3 up_shift = new Vector3(1.0f, 1.0f, 0.0f) * scale_shift;
        Vector3 right_shift = new Vector3(1.0f, -1.0f, 0.0f) * scale_shift;
        Vector3 down_shift = new Vector3(-1.0f, -1.0f, 0.0f) * scale_shift;

        Quaternion left_direction = Quaternion.identity;
        left_direction.eulerAngles = new Vector3(0, 0, 315);
        Quaternion up_direction = Quaternion.identity;
        up_direction.eulerAngles = new Vector3(0, 0, 225);
        Quaternion right_direction = Quaternion.identity;
        right_direction.eulerAngles = new Vector3(0, 0, 135);
        Quaternion down_direction = Quaternion.identity;
        down_direction.eulerAngles = new Vector3(0, 0, 45);

        GameObject left_sword = Instantiate(sword, current_position + left_shift, left_direction) as GameObject;
        GameObject up_sword = Instantiate(sword, current_position + right_shift, up_direction) as GameObject;
        GameObject right_sword = Instantiate(sword, current_position + right_shift, right_direction) as GameObject;
        GameObject down_sword = Instantiate(sword, current_position + down_shift, down_direction) as GameObject;

        left_sword.GetComponent<Rigidbody>().velocity = left_shift * _sword_split_speed;
        up_sword.GetComponent<Rigidbody>().velocity = up_shift * _sword_split_speed;
        right_sword.GetComponent<Rigidbody>().velocity = right_shift * _sword_split_speed;
        down_sword.GetComponent<Rigidbody>().velocity = down_shift * _sword_split_speed;

        Destroy(left_sword.gameObject, 5.0f);
        Destroy(up_sword.gameObject, 5.0f);
        Destroy(right_sword.gameObject, 5.0f);
        Destroy(down_sword.gameObject, 5.0f);
    }
}