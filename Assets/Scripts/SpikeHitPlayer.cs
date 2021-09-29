using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHitPlayer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameControl.Instance.player.GetComponent<Rigidbody>().AddForce(40f * new Vector3(0, -1.0f, 0), ForceMode.Impulse);
            GameControl.Instance.player.GetComponent<PlayerControl>().playerKnockBack(0);
            GameControl.Instance.AlterHealth(-0.5f);
        }
        
    }

}
