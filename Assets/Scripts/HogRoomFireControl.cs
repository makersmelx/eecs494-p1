using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogRoomFireControl : MonoBehaviour
{

    public bool goOut;
    public float goOutDuration = 4.0f;

    private void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    // Start is called before the first frame update
    public IEnumerator SelfDestroy()
    {
        while (true)
        {
            if (goOut)
            {
                float start = Time.time;
                float progress = 0f;
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                while (progress < 1.0f)
                {
                    progress = (Time.time - start) / goOutDuration;
                    sr.color = new Color(1f, 1f, 1f, 1f - progress / 4f);
                    yield return null;
                }

                Destroy(gameObject);
                yield return null;
            }

            yield return null;
        }
    }

    public void StartSelfDestroy(float duration)
    {
        goOutDuration = duration;
        goOut = true;
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("FIRE CAUGHT " + other.gameObject.tag);
        if (other.gameObject.layer == 16)
        {
            Debug.Log("Hog caught fire");
            other.gameObject.GetComponent<HasHealth>().AlterHealth(5f);
            other.gameObject.GetComponent<HasHealth>().Knockback(transform.position);
            Destroy(this.gameObject);
        }
    }

    /*
    private void Update()
    {
        flame_counter += Time.deltaTime;
        if (flame_counter > flame_offset)
        {
            flame_counter = 0;
            Destroy(this.gameObject);
        }
    }
    */
}
