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
        // Vector2 newPosition =
        //     new Vector2(
        //         transform.position.x + (direction * speed * Time.deltaTime),
        //         transform.position.y
        //     );
        // rb.MovePosition(newPosition);
    }


    void OnMove(InputValue value)
    {
        Debug.Log($"XXX: move: {value.Get()}");
        direction = value.Get<float>();
    jumping = false;
  }

    void OnJump()
    {
        Debug.Log("XXX: Jump");
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        jumping = true;
    }

    void OnDuck(InputValue value)
    {
        Debug.Log($"XXX: Duck: {value.Get()}");
        Debug.Log($"XXX: DuckIsPressed?: {value.isPressed}");
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.compareTag("floor"))
        {

        }
    }
}
