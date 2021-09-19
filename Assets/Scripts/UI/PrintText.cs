using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PrintText : MonoBehaviour
{
    private string _wholeText;
    private string _displayText;
    private Text _text;
    private bool _count;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _wholeText = _text.text;
        ResetText();
    }


    public IEnumerator DisablePlayerAndPrint()
    {
        if (_count)
        {
            GameControl.Instance.SetPlayerControl(false);
            gameObject.SetActive(true);
            _displayText = "";
            for (int i = 0; i < _wholeText.Length; i++)
            {
                _displayText += _wholeText[i];
                _text.text = _displayText;
                yield return new WaitForSeconds(0.1f);
            }

            yield return null;
            GameControl.Instance.SetPlayerControl(true);
            _count = false;
        }
    }

    public void ResetText()
    {
        _displayText = "";
        gameObject.SetActive(false);
        _displayText = "";
        _count = true;
    }
}