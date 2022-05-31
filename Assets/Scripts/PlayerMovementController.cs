using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    float direction;
    [SerializeField]  bool onFloor;
    Rigidbody2D rb;
    float jumpForce;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
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
        direction = value.Get<float>();
    }

    void OnJump()
    {
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
        rb.velocity = new Vector2(direction * speed, 0);
    }

    void ExecuteJump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(direction * speed, jumpForce), ForceMode2D.Impulse);
        onFloor = false;
    }
}
