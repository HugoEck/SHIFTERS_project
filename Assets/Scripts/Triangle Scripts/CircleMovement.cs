using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public float walkSpeed;
    private float inputHorizontal;
    public float jumpForce;

    public bool isJumping;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;

    public bool KnockFromRight;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (KBCounter <= 0)
        {
            rb.velocity = new Vector2(inputHorizontal * walkSpeed, rb.velocity.y);
        }
        else
        {
            if (KnockFromRight == true)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
                
            }
            if (KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }
        }

        KBCounter -= Time.deltaTime;

    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

}
