using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int _rupeeCount = 0;
    private int _keyCount = 0;

    public void AlterRupees(int rupeeNumber)
    {
        if (!GameControl.Instance.isInvinsible)
        {
            _rupeeCount += rupeeNumber;
            if (_rupeeCount < 0)
            {
                _rupeeCount = 0;
            }
        }
        
    }

    public int GetRupees()
    {
        return _rupeeCount;
    }
    
    public void AlterKeys(int keyNumber)
    {
        if (!GameControl.Instance.isInvinsible)
        {
            _keyCount += keyNumber;
            if (_keyCount < 0)
            {
                _keyCount = 0;
            }
        }
       
    }

    public int GetKeys()
    {
        return _keyCount;
    }
    
    public void EnableGodMode()
    {
        _rupeeCount = 100;
        _keyCount = 100;
    }
}