using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasHealth : MonoBehaviour
{
    public float maxHealth = 1;

    private float _health;

    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;
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
            _health -= 0.5f;
        }
    }
}