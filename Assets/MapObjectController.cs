using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectController : MonoBehaviour
{
    int screenUnits = 12;
    Vector2 originalPosition;
    int screenIndex;
    float positionXRight;
    float positionXLeft;
    bool alive;

    void Awake()
    {
        originalPosition = transform.position;
        screenIndex = Mathf.FloorToInt(originalPosition.x / screenUnits);
        positionXLeft = (screenIndex * screenUnits) - 100;
        positionXLeft = (screenIndex * screenUnits) + screenUnits + 100;

        alive = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn(string position)
    {
        float positionX = position == "left" ? positionXLeft : positionXRight;
        transform.position = new Vector2(positionX, transform.position.y);
    }
}
