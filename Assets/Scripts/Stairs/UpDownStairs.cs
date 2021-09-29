using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownStairs : MonoBehaviour
{
    // Start is called before the first frame update
    public static IEnumerator UpDownStairsCoroutine(Vector3 deltaCamera, Vector3 playerPosition, float duration)
    {
        GameControl.Instance.player.SetActive(false);
        GameControl.Instance.player.transform.position = playerPosition;
        GameControl.Instance.SetPlayerControl(false);
        float start = Time.time;
        float progress = (Time.time - start) / duration;
        while (progress < 0.5f)
        {
            progress = (Time.time - start) / duration;
            float alpha = (float) Math.Pow((progress * 2), 5) * 255f;
            Camera.main.GetComponentInChildren<SpriteRenderer>().color =
                new Color(0, 0, 0, alpha);
            yield return null;
        }


        Camera.main.GetComponentInChildren<SpriteRenderer>().color = Color.black;
        Camera.main.transform.position += deltaCamera;
        GameControl.Instance.UpdateBounds(Camera.main.transform.position);

        GameControl.Instance.player.SetActive(true);
        while (progress < 1f)
        {
            progress = (Time.time - start) / duration;
            float alpha = (float) Math.Pow((2 - progress * 2), 5) * 255f;
            Camera.main.GetComponentInChildren<SpriteRenderer>().color =
                new Color(0, 0, 0, alpha);
            yield return null;
        }

        Camera.main.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        GameControl.Instance.SetPlayerControl(true);


        yield return null;
    }
}