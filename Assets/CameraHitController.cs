using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraHitController : MonoBehaviour
{
    [SerializeField] float stunnedTime;
    Animator animator;
    Rigidbody2D rb;
    public UnityEvent onRestart;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHitController>().Stunned();
            Hit();
        }
    }

    public void Hit()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("Hit", true);
        Invoke("UnStunned", stunnedTime);
    }

    void UnStunned()
    {
        animator.SetBool("Hit", false);
        onRestart.Invoke();
    }
}
