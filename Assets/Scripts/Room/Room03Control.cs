using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room03Control : RoomControl
{
    public override void ResetBlocksAndUI()
    {
        GameObject.Find("Dialogue").GetComponent<PrintText>().ResetText();
    }
}