using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    float direction;
    bool jumping;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        direction = 0;
    }

    void FixedUpdate()
    {
        if(!jumping)
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

        if(jumping) return;

        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(direction * speed, jumpForce), ForceMode2D.Impulse);
        jumping = true;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("Floor"))
            jumping = false;
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
}
