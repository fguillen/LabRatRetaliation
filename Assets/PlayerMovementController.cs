using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float speed;
    float direction;
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
        Vector2 newPosition =
            new Vector2(
                transform.position.x + (direction * speed * Time.deltaTime),
                transform.position.y
            );
        rb.MovePosition(newPosition);
    }


    void OnMove(InputValue value)
    {
        Debug.Log($"XXX: move: {value.Get()}");
        direction = value.Get<float>();
    }

    void OnJump()
    {
        Debug.Log("XXX: Jump");
    }

    void OnDuck(InputValue value)
    {
        Debug.Log($"XXX: Duck: {value.Get()}");
        Debug.Log($"XXX: DuckIsPressed?: {value.isPressed}");
    }
}
