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
    float positionXInitial;
    float positionXFinal;
    bool alive;
    int direction;
    Rigidbody2D rb;


    void Awake()
    {
        originalPosition = transform.position;
        screenIndex = Mathf.FloorToInt(originalPosition.x / screenUnits);
        positionXLeft = (screenIndex * screenUnits) - 1;
        positionXRight = (screenIndex * screenUnits) + screenUnits + 1;

        alive = false;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(alive)
        {
            CheckIfPositionFinalReached();
        }
    }

    void CheckIfPositionFinalReached()
    {
        if(Mathf.Abs(transform.position.x - positionXFinal) < 0.1)
            transform.position = new Vector2(positionXInitial, transform.position.y);
    }

    public void Spawn(string side)
    {
        positionXInitial = side == "left" ? positionXLeft : positionXRight;
        positionXFinal = side == "left" ? positionXRight : positionXLeft;
        direction = side == "left" ? 1 : -1;
        transform.position = new Vector2(positionXInitial, transform.position.y);
        rb.velocity = new Vector2(direction * speed, 0);

        alive = true;
    }

    public void Stop()
    {
        transform.position = originalPosition;
        rb.velocity = Vector2.zero;

        alive = false;
    }
}
