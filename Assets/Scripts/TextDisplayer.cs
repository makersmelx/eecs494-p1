using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    public Inventory inventory;
    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Weapon"))
        {
            _text.text = "WEAPON: ";
            _text.text += WeaponStatusToString(GameControl.Instance.weaponMain);
            _text.text += ", ";
            _text.text += WeaponStatusToString(GameControl.Instance.weaponAlt);

        }
        else if (gameObject.CompareTag("Heart"))
        {
            _text.text = "HEALTH: ";
            _text.text += (GameControl.Instance.GetPlayerHealth());
        }
        else if (gameObject.CompareTag("Key"))
        {
            if (inventory != null && _text != null)
            {
                _text.text = "KEYS: ";
                _text.text += inventory.GetKeys().ToString();
            }
        }
        else if (gameObject.CompareTag("Rupee"))
        {
            if (inventory != null && _text != null)
            {
                _text.text = "RUPEES: ";
                _text.text += inventory.GetRupees().ToString();
            }
        }
    }
    
    string WeaponStatusToString(int type)
    {
        if (type == 0) return "SWORD";
        if (type == 1) return "BOW";
        return "";
        
    }
}