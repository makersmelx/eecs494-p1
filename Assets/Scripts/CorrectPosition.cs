using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectPosition : MonoBehaviour
{
    // Start is called before the first frame update

    private void Update()
    {
        float horizontalTrial = Input.GetAxisRaw("Horizontal");
        float verticalTrial = Input.GetAxisRaw("Vertical");
        if (Math.Abs(horizontalTrial) > 0.0f)
        {
            float deltaY = transform.position.y % 1f;
            float deltaYAbs = Math.Abs(deltaY);
            float floorY = transform.position.y - deltaY;
            if (deltaYAbs < 0.25)
            {
                transform.position = new Vector2(transform.position.x, floorY);
            }
            else if (deltaYAbs >= 0.25 && deltaYAbs <= 0.75)
            {
                transform.position = new Vector2(transform.position.x, floorY + 0.5f * deltaYAbs / deltaY);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, floorY + 1f * deltaYAbs / deltaY);
            }
        }

        if (Math.Abs(verticalTrial) > 0.0f)
        {
            float deltaX = transform.position.x % 1f;
            float deltaXAbs = Math.Abs(deltaX);
            float floorX = transform.position.x - deltaX;
            if (deltaXAbs < 0.25)
            {
                transform.position = new Vector2(floorX, transform.position.y);
            }
            else if (deltaXAbs >= 0.25 && deltaXAbs <= 0.75)
            {
                transform.position = new Vector2(floorX + 0.5f * deltaXAbs / deltaX, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(floorX + 1f * deltaXAbs / deltaX, transform.position.y);
            }
        }
    }
}