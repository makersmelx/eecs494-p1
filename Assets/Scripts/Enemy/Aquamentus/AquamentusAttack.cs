using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquamentusAttack : MonoBehaviour
{
    public float attack = 0.5f;
    public float launchCoolDown = 4f;
    public float beamSpeed = 4f;
    public GameObject beamPrefab;
    private Animator _animator;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
        StartCoroutine(FireBeam());
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Player"))
        {
            GameControl.Instance.AlterHealth(-attack);
            otherGameObject.GetComponent<PlayerControl>().playerKnockBack(40f);
        }
    }


    IEnumerator FireBeam()
    {
        while (true)
        {
            _animator.SetTrigger("Fire");
            yield return new WaitForSeconds(1);
            Vector3 mouth = transform.position + new Vector3(-0.5f, 0.5f, 0);
            Vector3 playerPosition = GameControl.Instance.player.transform.position;
            float horizontalAxis = playerPosition.x > transform.position.x ? 1 : -1;
            Vector3 direction = new Vector3(horizontalAxis * 10f, 3, 0);
            for (int i = 0; i < 3; i++)
            {
                GameObject beam = Instantiate(beamPrefab, mouth, Quaternion.identity);
                beam.GetComponent<Rigidbody>().velocity =
                    direction.normalized * beamSpeed;
                beam.GetComponent<BeamAttack>().attack = attack;
                direction += Vector3.down * 3f;
            }

            yield return new WaitForSeconds(4);
            yield return null;
        }
    }
}