using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    bool inCage;
    Vector2 originalPosition;
    Animator animator;
    Rigidbody2D rb;
    Transform playerTransform;

    [SerializeField] float runSpeed;

    void Awake()
    {
        originalPosition = transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!inCage)
            CheckIfFarEnough();
    }

    void CheckIfFarEnough()
    {
        if(Mathf.Abs(playerTransform.position.x - transform.position.x) > 14)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameManagerController.Instance.player.transform;
        InCage();
    }

    void InCage()
    {
        inCage = true;
        animator.SetBool("Run", false);
    }

    public void Free()
    {
        inCage = false;
        animator.SetBool("Run", true);

        Vector2 direction;
        if(playerTransform.position.x < transform.position.x)
            direction = Vector2.right;
        else
            direction = Vector2.left;

        rb.velocity = direction * runSpeed;
    }
}
