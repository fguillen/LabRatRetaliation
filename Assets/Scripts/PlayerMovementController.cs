using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    int direction;
    [SerializeField]  bool onFloor;
    Rigidbody2D rb;
    float jumpForce;
    PlayerHitController playerHitController;
    PlayerOnTheStairsController playerOnTheStairsController;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHitController = GetComponent<PlayerHitController>();
        playerOnTheStairsController = GetComponent<PlayerOnTheStairsController>();

        jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
    }

    void Start()
    {
        direction = 0;
        onFloor = false;
    }

    void FixedUpdate()
    {
        if(onFloor && !playerHitController.stunned && !playerOnTheStairsController.onStairsWalking)
            MoveBaseOnDirection();
    }

    void OnMove(InputValue value)
    {
        direction = Mathf.CeilToInt(value.Get<float>());
    }

    void OnJump()
    {
        if(onFloor && !playerHitController.stunned && !playerOnTheStairsController.onStairsWalking)
            ExecuteJump(direction);
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

    public void ExecuteJump(int _direction)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(_direction * speed, jumpForce), ForceMode2D.Impulse);
        onFloor = false;
    }
}
