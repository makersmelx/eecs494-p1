using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladetrapControl : MonoBehaviour
{

    public bool[] _active_directions = { false, false, false, false };
    private Vector3[] directions = { new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 0, 0), new Vector3(0, -1, 0) };
    private Vector3 _init_position;
    private float speed = 2.0f;
    private Rigidbody _rigidbody;

    private float _max_move_x = 6.0f;
    private float _max_move_y = 3.5f;
    private float _max_move;

    private bool returning = false;
    private bool activated = false;

    private int _dir_index = -1;
    

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _init_position = transform.position;
        Debug.Log(_init_position.x.ToString() + ", " + _init_position.y.ToString());
    }

    
    void Update()
    {

        if (!activated)
        {
            for (int i = 0; i < 4; i++)
            {
                if (_active_directions[i] && Physics.Raycast(transform.position, directions[i], 8f, 1 << 3))
                {
                    activated = true;
                    _rigidbody.velocity = directions[i] * speed * 3;
                    _dir_index = i;
                    _max_move = (i == 1 || i == 3) ? _max_move_y : _max_move_x;
                }
            }
        }

        else
        {
            if (returning)
            {
                if (Vector3.Distance(_rigidbody.transform.position, _init_position) <= 0.2f)
                {
                    activated = false;
                    returning = false;
                    _rigidbody.velocity = Vector3.zero;
                    transform.position = new Vector3 (Mathf.Round(_init_position.x), Mathf.Round(_init_position.y), 0);
                }
                else
                    _rigidbody.velocity = directions[_dir_index] * (-speed);
            }
            else if (Vector3.Distance(_rigidbody.transform.position, _init_position) > _max_move)
            {
                returning = true;
            }

            else
            {
                _rigidbody.velocity = directions[_dir_index] * speed * 3;
            }
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (activated && !returning) returning = true;
    }

}
