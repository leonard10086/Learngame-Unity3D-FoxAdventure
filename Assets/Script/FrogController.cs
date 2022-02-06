using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : Enemy
{
    
    public float Speed = 1;
    public Transform leftpoint, rightpoint;
    public float JumpForce = 10;
    public LayerMask Ground;


    private Rigidbody2D rb2d;
    //private Animator animator;
    private Collider2D collider2D;
    private float leftx, rightx;
    private bool FaceLeft = true;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb2d = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }

    void Movement()
    {
        //左右巡回
        if (FaceLeft)
        {
            if (collider2D.IsTouchingLayers(Ground))
            {
                animator.SetBool("IsJumping", true);
                rb2d.velocity = new Vector2(-Speed, JumpForce);
            }
            if (transform.position.x < leftx)
            {
                rb2d.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                FaceLeft = false;
            }

        }
        else
        {
            if (collider2D.IsTouchingLayers(Ground))
            {
                //解决青蛙转向跳跃不协调
                animator.SetBool("IsJumping", true);
                rb2d.velocity = new Vector2(Speed, JumpForce);

            }
            if (transform.position.x > rightx)
            {
                rb2d.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                FaceLeft = true;
            }
        }


    }

    void SwitchAnim()
    {
        if(animator.GetBool("IsJumping"))
        {
            if(rb2d.velocity.y < 0.1f)
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", true);
            }
            
        }
        if (collider2D.IsTouchingLayers(Ground) && animator.GetBool("IsFalling"))
        {
            animator.SetBool("IsFalling", false);
        }
    }



  
}
