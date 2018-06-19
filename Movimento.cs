using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimento : MonoBehaviour {
    public float speed = 3f;
    public float jumpForce = 200f;
    private bool facingRight = true;
    private float movX;
    private Rigidbody2D rb;

    private bool jumping;
    private Animator playerAnimator;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        jumping = false;
        playerAnimator = GetComponent<Animator>();
	}

    private void FixedUpdate()
    {
        Mov();
        Jump();
    }

    void Mov()
    {
        movX = Input.GetAxis("Horizontal");
        if (movX > 0 && !facingRight)
        {
            Flip();
        }
        else if (movX < 0 && facingRight)
            Flip();

        rb.velocity = new Vector2(movX * speed, rb.velocity.y);

        if(movX != 0)
            playerAnimator.SetBool("walk", true);
        else
            playerAnimator.SetBool("walk", false);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = GetComponent<Transform>().localScale;
        scale.x *= -1;
        GetComponent<Transform>().localScale = scale;
    }

    void Jump()
    {
        var AbsVelY = Mathf.Abs(rb.velocity.y);
        jumping = AbsVelY >= 0.05f;

        if(Input.GetKeyDown("up") && !jumping)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
            playerAnimator.SetBool("jump", true);
        } else if (!jumping)
        {
            playerAnimator.SetBool("jump", false);
        }
    }
}
