using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectilMovementRunningController : MonoBehaviour
{
    int direction;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        StartMovement();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + (direction * speed * Time.deltaTime), transform.position.y);
    }

    void StartMovement()
    {

    }
}
