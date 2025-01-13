using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLimitation : MonoBehaviour
{
    public float zOffset = 1f; // X ekseninde ilerleme miktarý

    public void MoveObjectsOnXAxis()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Moveable");

        foreach (var obj in objects)
        {
            Vector3 currentPosition = obj.transform.position;
            obj.transform.position = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z - zOffset);
        }
    }
}
