using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectController : MonoBehaviour
{
    [SerializeField] float speed;

    int screenUnits = 12;
    Vector2 originalPosition;
    int screenIndex;
    float positionXRight;
    float positionXLeft;
    bool alive;
    int direction;

    void Awake()
    {
        originalPosition = transform.position;
        screenIndex = Mathf.FloorToInt(originalPosition.x / screenUnits);
        positionXLeft = (screenIndex * screenUnits) - 1;
        positionXRight = (screenIndex * screenUnits) + screenUnits + 1;

        alive = false;
    }

    void Update()
    {
        if(alive)
            transform.Translate(direction * speed * Time.deltaTime, 0, 0);
    }

    public void Spawn(string side)
    {
        float positionX = side == "left" ? positionXLeft : positionXRight;
        direction = side == "left" ? 1 : -1;
        transform.position = new Vector2(positionX, transform.position.y);
        alive = true;
    }

    public void Stop()
    {
        alive = false;
    }
}
