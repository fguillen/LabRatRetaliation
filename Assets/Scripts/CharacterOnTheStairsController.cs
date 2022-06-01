using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[System.Serializable] public class OnTouchStairsEvent : UnityEvent<string> { }

public class CharacterOnTheStairsController : MonoBehaviour
{
    [SerializeField] float stairsSpeed;
    Collider2D theCollider;

    StairsController stairsController;
    public string stairsLevel;
    bool onStairs;
    public bool onStairsWalking;
    Vector2 endPosition;
    Rigidbody2D rb;
    float originalGravityScale;
    public OnTouchStairsEvent onTouchingStairs;
    public UnityEvent onEndOfStairs;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        theCollider = GetComponent<Collider2D>();

        originalGravityScale = rb.gravityScale;

        if(onTouchingStairs == null)
            onTouchingStairs = new OnTouchStairsEvent();

        if(onEndOfStairs == null)
            onEndOfStairs = new UnityEvent();
    }

    void Update()
    {
        if(onStairsWalking)
            CheckIfStairsFinished();
    }

    void CheckIfStairsFinished()
    {
        if(((Vector2)transform.position - endPosition).magnitude < 0.1)
        {
            onStairsWalking = false;
            rb.velocity = Vector2.zero;
            theCollider.enabled = true;
            rb.gravityScale = originalGravityScale;
            onEndOfStairs.Invoke();
        }
    }

    void OnUp(InputValue value)
    {
        Debug.Log("XXX: OnUp()");
        if(onStairs && stairsLevel == "down" && value.isPressed)
        {
            WalkTheStairs();
        }
    }

    void OnDown(InputValue value)
    {
        Debug.Log("XXX: OnDown()");
        if(onStairs && stairsLevel == "up" && value.isPressed)
        {
            WalkTheStairs();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Stairs"))
            OnTheStairs(other.GetComponent<StairsController>());
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Stairs"))
            OffTheStairs(other.GetComponent<StairsController>());
    }

    void OnTheStairs(StairsController stairsController)
    {
        this.stairsController = stairsController;
        stairsLevel = stairsController.StairsLevel(transform.position);
        onStairs = true;
        onTouchingStairs.Invoke(stairsLevel);
    }

    void OffTheStairs(StairsController stairsController)
    {
        Debug.Log("XXX: OffTheStairs()");
        stairsController = null;
        onStairs = false;
    }

    public void WalkTheStairs()
    {
        Debug.Log("XXX: WalkTheStairs()");
        Vector2 startPosition = stairsController.PointPosition(stairsLevel);
        endPosition = stairsController.PointPosition(stairsLevel == "up" ? "down" : "up");
        rb.MovePosition(startPosition);
        Vector2 direction = (endPosition - startPosition).normalized;
        rb.velocity = direction * stairsSpeed;
        Debug.Log($"XXX: gravityScale1: {rb.gravityScale}");
        rb.gravityScale = 0;
        Debug.Log($"XXX: gravityScale2: {rb.gravityScale}");

        Debug.Log($"XXX: {direction}, {rb.velocity}");
        onStairsWalking = true;
        theCollider.enabled = false;
    }
}
