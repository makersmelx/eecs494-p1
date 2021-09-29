using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasHealth : MonoBehaviour
{


    public bool is_boss = false;
    public float maxHealth = 1;
    private float _health;

    public AudioClip monster_dead_sound_clip;
    public AudioClip monster_attacked_sound_clip;
    public GameObject drop_item;

    private Color _color;
    private Rigidbody _rb;

    private bool in_delay = false;

    private void Start()
    {
        _health = maxHealth;
        _color = GetComponent<SpriteRenderer>().color;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_health <= 0)
        {
            if (drop_item != null)
            {
                Instantiate(drop_item, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
            AudioSource.PlayClipAtPoint(monster_dead_sound_clip, Camera.main.transform.position);
            if (monster_attacked_sound_clip != null)
            {
                AudioSource.PlayClipAtPoint(monster_attacked_sound_clip, Camera.main.transform.position);
            }
        }

    }

    public void AlterHealth(float damage)
    {
        _health -= damage;
    }

    public void Knockback(Vector3 origin_pos)
    {
        if (!is_boss)
        {
            _rb.AddForce(40f * Vector3.Normalize(_rb.transform.position - origin_pos), ForceMode.Impulse);
        }
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        in_delay = true;
        if (this.gameObject.GetComponent<Animator>() != null)
            this.gameObject.GetComponent<Animator>().speed = 0;
        StartCoroutine(restore());

    }

    IEnumerator restore()
    {
        yield return new WaitForSeconds(0.5f);

        this.gameObject.GetComponent<SpriteRenderer>().color = _color;
        in_delay = false;
        if (this.gameObject.GetComponent<Animator>() != null)
            this.gameObject.GetComponent<Animator>().speed = 1;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon") && !in_delay && _health > 0)
        {
            Knockback(other.transform.position);
            StartCoroutine(restore());
        }
    }

}