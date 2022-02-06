using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //private中的组件需要在start中实例化
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool IsHurt;

    //声明变量后记得还要在uniy里面把组件拖拽进来，不然操作就没有对象了
    public float speed;
    public float jumpforce;
    public Collider2D collider2D;
    public Collider2D discollider2D;
    public Joystick joystick;
    //layerMask：只选定Layermask层内的碰撞器，其它层内碰撞器忽略。
    public Transform CellingCheck;
    public LayerMask Ground;

    public Text CherryNum;
    public int Cherry = 0;

    public Text GemNum;
    public int Gem = 0;

   


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();




    }

    // Update is called once per frame
    void Update()
    {
        if (!IsHurt)
        {
            Movement();
        }

        SwitchAnimator();
    }

    void Movement()
    {
        float HorizontalMove = Input.GetAxis("Horizontal");//-1往右 1往左;
        float FaceDirection = Input.GetAxisRaw("Horizontal");
        //float HorizontalMove = joystick.Horizontal;
        //float FaceDirection = joystick.Horizontal;

        //取消if判定能让移动停止更直接，没有漂移
        if (HorizontalMove != 0)
        {
            rb2d.velocity = new Vector2(HorizontalMove * speed, rb2d.velocity.y);
            animator.SetBool("IsRunning", true);

        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        if (FaceDirection < 0 )
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (FaceDirection > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //角色下蹲,然后禁用碰撞体
        if (!Physics2D.OverlapCircle(CellingCheck.position, 0.2f, Ground))
        {
            if (Input.GetButtonDown("Crouch"))
            //if (joystick.Vertical < -0.5f)
            {
                animator.SetBool("IsCrouch", true);
                discollider2D.enabled = false;
            }
            else
            {
                animator.SetBool("IsCrouch", false);
                discollider2D.enabled = true;
            }
        }


        if (Input.GetButtonDown("Jump") && collider2D.IsTouchingLayers(Ground))
        //if (joystick.Vertical > 0.5f && collider2D.IsTouchingLayers(Ground))
        {
            SoundManager.instance.JumpAudio();
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpforce);
            animator.SetBool("IsJumping", true);
        } 


    }

    void SwitchAnimator()
    {   if(rb2d.velocity.y < 0.1f && !collider2D.IsTouchingLayers(Ground))
        {
            animator.SetBool("IsFalling", true);
        }
        //将跳跃中动画切换为下落
        if(animator.GetBool("IsJumping"))
        {
            if(rb2d.velocity.y < 0 )
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", true);
            }
            
        }else if (IsHurt)
        {
            animator.SetBool("IsHurt", true);
            SoundManager.instance.HurtAudio();
            if (Mathf.Abs(rb2d.velocity.x) < 2f)
            {
                IsHurt = false;
                animator.SetBool("IsHurt", false);
            }
        }else if(collider2D.IsTouchingLayers(Ground))
        {
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsIdling", true);
        }
    }

    //收集物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //解决双碰撞体的问题
        // Collider2D collider = gameObject.GetComponent<Collider2D>();
        //collider.enabled = false;

        if (collision.tag == "Cherry")
        {
            SoundManager.instance.CherryAudio();
            Destroy(collision.gameObject);
            Cherry += 1;
            CherryNum.text = Cherry.ToString();
        }

        if (collision.tag == "Gem")
        {
            SoundManager.instance.CherryAudio();
            Destroy(collision.gameObject);
            Gem += 1;
            GemNum.text = Gem.ToString();
        }

        if (collision.tag == "Deadline")
        {
            Invoke("Restart", 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")      
        {
            //实例化
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (animator.GetBool("IsFalling"))
            {
                //用frog的函数来代替Destroy(collision.gameObject);
                //但是使用frog的函数跳老鹰就会报错
                enemy.JumpOn();
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpforce);
                animator.SetBool("IsJumping", true);
            }else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb2d.velocity = new Vector2(-5, rb2d.velocity.y);
                IsHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb2d.velocity = new Vector2(5, rb2d.velocity.y);
                IsHurt = true;
            }
        }
       
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
