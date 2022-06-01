using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CageController : MonoBehaviour
{
    bool open;
    public UnityEvent onOpen;
    Animator animator;

    void Awake()
    {
        open = false;
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !open)
            Open();
    }

    void Open()
    {
        animator.SetBool("Open", true);
        open = true;
        onOpen.Invoke();
    }
}
