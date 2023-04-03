using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public float walkSpeed;
    private float inputHorizontal;
    public float jumpForce;

    CircleMovement circleMovement;

    public bool isJumping;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        walkSpeed = 30f;
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(inputHorizontal * walkSpeed, rb.velocity.y);

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

        if (other.gameObject.tag == "Weak Point")// && circleMovement.KBCounter <= 0)
        {
            circleMovement.KBCounter = circleMovement.KBTotalTime;
            if (other.transform.position.x <= transform.position.x)
            {
                circleMovement.KnockFromRight = true;
            }
            if (other.transform.position.x >= transform.position.x)
            {
                circleMovement.KnockFromRight = false;
            }
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
