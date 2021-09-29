using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenThenClose : MonoBehaviour
{
    // Start is called before the first frame update


    private void OnCollisionEnter(Collision other)
    {
        if ((other.transform.position - transform.position).magnitude <= 0.3)
        {
            StartCoroutine(PlayerWalkThenDoorClose());
        }
    }

    IEnumerator PlayerWalkThenDoorClose()
    {
        DoorControl doorControl = GetComponentInParent<DoorControl>();
        doorControl.UnLockDoors();
        GameControl.Instance.SetPlayerControl(false);
        Vector2 doorDirection = GetComponentInParent<DoorControl>()
            .gameObject
            .GetComponentInChildren<DoorMoveCamera>()
            .doorDirection;
        yield return GameControl.Instance.player.GetComponent<PlayerControl>()
            .WalkWithCoroutine(-2f * doorDirection);
        doorControl.ResetDoors();
        GameControl.Instance.SetPlayerControl(true);
    }
}