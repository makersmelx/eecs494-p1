using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasHealth : MonoBehaviour
{
    public float maxHealth = 1;

    private float _health;

    public float _weapon_damage;

    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;
        _weapon_damage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionGameObject = collision.collider.gameObject;

        if (collisionGameObject.CompareTag("Weapon"))
        {
            // todo: fix that different weapons have different damage @yuyuetu
            _health -= _weapon_damage;
        }
    }

}