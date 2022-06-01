using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestTubeHitController : MonoBehaviour
{
    [SerializeField] float stunnedTime;
    Rigidbody2D rb;
    public UnityEvent onRestart;

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
        Invoke("UnStunned", stunnedTime);
    }

    void UnStunned()
    {
        onRestart.Invoke();
    }
}
