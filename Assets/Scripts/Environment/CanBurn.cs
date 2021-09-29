using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBurn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject FirePrefab;
    private bool _isBurn;

    private void Start()
    {
        _isBurn = false;
        StartCoroutine(BurnToAsh());
    }

    public void Burn()
    {
        if (!_isBurn)
        {
            GameObject fire = Instantiate(FirePrefab, transform);
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                Destroy(boxCollider);
            }

            _isBurn = true;
        }
        // Destroy(gameObject);
    }

    IEnumerator BurnToAsh()
    {
        while (true)
        {
            if (_isBurn)
            {
                yield return new WaitForSeconds(2);
                Destroy(gameObject);
            }

            yield return null;
        }
    }
}