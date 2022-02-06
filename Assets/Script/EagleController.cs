using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : Enemy
{

    public float Speed = 1;
    public Transform Uppoint, Downpoint;
    public float JumpForce = 10;
    public LayerMask Ground;


    private Rigidbody2D rb2d;
    //private Collider2D collider2D;
    private float upy, downy;
    private bool FaceTop = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb2d = GetComponent<Rigidbody2D>();
        //collider2D = GetComponent<Collider2D>();

        transform.DetachChildren();
        upy = Uppoint.position.y;
        downy = Downpoint.position.y;
        Destroy(Uppoint.gameObject);
        Destroy(Downpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //左右巡回
        if (FaceTop)
        {
 
            rb2d.velocity = new Vector2(0, Speed);
            if (transform.position.y > upy)
            {
                FaceTop = false;
            }

        }
        else
        {
            rb2d.velocity = new Vector2(0, -Speed);
            if (transform.position.y < downy)
            {
                FaceTop = true;
            }
        }


    }
}

 