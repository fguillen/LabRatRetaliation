using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField] float stunnedTime;
    PlayerMovementController playerMovementController;

    public bool stunned;

    void Awake()
    {
        stunned = false;
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    public void Hit(Vector2 position)
    {
        stunned = true;
        int direction = position.x < transform.position.x ? 1 : -1;
        playerMovementController.ExecuteJump(direction);

        this.Invoke("UnStunned", stunnedTime);
    }

    public void Stunned()
    {
        stunned = true;
        this.Invoke("UnStunned", stunnedTime);
    }

    void UnStunned()
    {
        stunned = false;
    }
}
