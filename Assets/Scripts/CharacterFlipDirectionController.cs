using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlipDirectionController : MonoBehaviour
{
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (
            (rb.velocity.x < -1 && transform.localScale.x > 0) ||
            (rb.velocity.x > 1 && transform.localScale.x < 0)
        )
        {
            Debug.Log($"XXX: {rb.velocity.x}");
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
  }
}
