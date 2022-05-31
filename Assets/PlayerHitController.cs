using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField] float stunnedTime;
    Rigidbody2D rb;
    PlayerMovementController playerMovementController;

    public bool stunned;

    void Awake()
    {
        stunned = false;

        rb = GetComponent<Rigidbody2D>();
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("MapObject"))
        {
            collisionInfo.gameObject.GetComponent<MapObjectController>().Hit();
            Hit(collisionInfo.GetContact(0).point);
        }
    }

    void Hit(Vector2 position)
    {
        stunned = true;
        int direction = position.x < transform.position.x ? 1 : -1;
        playerMovementController.ExecuteJump(direction);

        this.Invoke("UnStunned", stunnedTime);
    }

    void UnStunned()
    {
        stunned = false;
    }
}
