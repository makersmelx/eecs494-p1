using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownStairsBowRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration = 3f;
    private Coroutine _current;

    private void Start()
    {
        _current = StartCoroutine(Transit());
    }


    IEnumerator Transit()
    {
        while (true)
        {
            if ((GameControl.Instance.player.transform.position - transform.position).magnitude <= 0.1f)
            {
                yield return UpDownStairs.UpDownStairsCoroutine(
                    Vector3.left * GameControl.GridWidth,
                    new Vector3(3f, 65f, 0),
                    duration
                );
                yield return GameControl.Instance.player.GetComponent<PlayerControl>()
                    .WalkWithCoroutine(2f * Vector3.down);
                RoomControl.ResetRoomOfGameObject(gameObject);
                break;
            }

            yield return null;
        }
    }

    public void ResetStairs()
    {
        if (_current != null)
            StopCoroutine(_current);
        _current = StartCoroutine(Transit());
    }
}