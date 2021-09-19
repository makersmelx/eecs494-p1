using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room03Reset : RoomReset
{
    public override void ResetRoom()
    {
        GameObject.Find("Dialogue").GetComponent<PrintText>().ResetText();
    }
}