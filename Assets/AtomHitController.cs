using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AtomHitController : MonoBehaviour
{
    [SerializeField] float stunnedTime;
    Rigidbody2D rb;
    public UnityEvent onRestart;
    float originalGravityScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Player"))
        {
            collisionInfo.gameObject.GetComponent<PlayerHitController>().Hit(collisionInfo.GetContact(0).point);
            Hit();
        }
    }

    void Hit()
    {
        rb.velocity = Vector2.zero;
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        Invoke("UnStunned", stunnedTime);
    }

    void UnStunned()
    {
        rb.gravityScale = originalGravityScale;
        onRestart.Invoke();
    }
}
