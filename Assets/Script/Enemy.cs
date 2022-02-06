using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //protected：protected对于子女、朋友来说，就是public的，可以自由使用，没有任何限制，而对于其他的外部class，protected就变成private。
    protected Animator animator;
    protected AudioSource deathAudio;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    public void JumpOn()
    {
        deathAudio.Play();
        animator.SetTrigger("death");
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
