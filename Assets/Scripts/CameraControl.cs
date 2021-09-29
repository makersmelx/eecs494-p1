using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Update is called once per frame
    public GameObject player;
    public float moveDuration = 2.5f;

    public static CameraControl Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void MoveCamera(Vector2 direction)
    {
        StartCoroutine(MoveCameraCoroutine(direction));
    }

    public void Reset()
    {
        StartCoroutine(ResetCoroutine());
    }



    IEnumerator MoveCameraCoroutine(Vector2 direction)
    {
        float transitX = direction.normalized.x * GameControl.GridWidth;
        float transitY = direction.normalized.y * GameControl.GridHeight;
        Vector3 transit = new Vector3(transitX, transitY, 0);
        player.SetActive(false);
        player.transform.position = (Vector2) player.transform.position + (2f * direction);
        yield return StartCoroutine(MoveObjectOverTime(transform, transform.position,
            transform.position + transit, moveDuration));
        // After moving Camera
        player.SetActive(true);
        player.GetComponent<AnimatorControl>().SetAnimation2DAxis(direction);
        GameControl.Instance.UpdateBounds(transform.position);
    }

    IEnumerator ResetCoroutine()
    {
        player.SetActive(false);
        Vector3 start_point = new Vector3(39.5f, 6.5f, -34.15f);
        yield return StartCoroutine(MoveObjectOverTime(transform, transform.position,
            start_point, moveDuration));
        // After moving Camera
        player.SetActive(true);
        player.GetComponent<AnimatorControl>().SetAnimation2DAxis(start_point - transform.position);
        GameControl.Instance.UpdateBounds(transform.position);
    }

  

    public static IEnumerator MoveObjectOverTime(Transform target, Vector3 initial, Vector3 dest, float duration)
    {
        float initialTime = Time.time;
        float progress = (Time.time - initialTime) / duration;
        while (progress < 1.0f)
        {
            progress = (Time.time - initialTime) / duration;
            Vector3 newPosition = Vector3.Lerp(initial, dest, progress);
            target.position = newPosition;
            yield return null;
        }

        target.position = dest;
    }
}