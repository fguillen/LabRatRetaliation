using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAnimationsController : MonoBehaviour
{
    Animator animator;
    List<string> animations = new List<string>(){ "Walking", "Jumping", "Stunned", "Hiding", "Hit", "Idle" };

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Walking()
    {
        SetAnimation("Walking");
    }

    public void Jumping()
    {
        SetAnimation("Jumping");
    }

    public void Idle()
    {
        SetAnimation("Idle");
    }

    void SetAnimation(string animation)
    {
        animations.ForEach(e => {
            if(e == animation && animator.GetBool(e) != true)
                animator.SetBool(e, true);

            if(e != animation && animator.GetBool(e) == true)
                animator.SetBool(e, false);
        });
    }
}
