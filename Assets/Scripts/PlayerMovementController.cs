using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    float direction;
    [SerializeField]  bool onFloor;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        direction = 0;
        onFloor = false;
    }

    void FixedUpdate()
    {
        if(onFloor)
            MoveBaseOnDirection();
    }

    void OnMove(InputValue value)
    {
        Debug.Log($"XXX: move: {value.Get()}");
        direction = value.Get<float>();
    }

    void OnJump()
    {
        Debug.Log("XXX: Jump");

        if(onFloor)
            ExecuteJump();
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("Floor"))
            onFloor = true;
    }

    void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("Floor"))
            onFloor = false;
    }

    void MoveBaseOnDirection()
    {
        Vector2 newPosition =
            new Vector2(
                transform.position.x + (direction * speed * Time.deltaTime),
                transform.position.y
            );
        rb.MovePosition(newPosition);
    }

    void ExecuteJump()
    {
        Debug.Log("XXX: ExecuteJump");
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(direction * speed, jumpForce), ForceMode2D.Impulse);
        onFloor = false;
    }
}
