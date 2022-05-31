using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    [SerializeField] public Transform upPosition;
    [SerializeField] public Transform downPosition;

    public string StairsLevel(Vector2 position)
    {
        if(position.y < transform.position.y)
            return "down";
        else
            return "up";
    }

    public Vector2 PointPosition(string level)
    {
        return level == "up" ? upPosition.position : downPosition.position;
    }
}
