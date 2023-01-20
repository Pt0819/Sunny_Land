using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Animator anime;
    protected Rigidbody2D rb;
    protected AudioSource DeathAudio;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        anime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        DeathAudio = GetComponent<AudioSource>();
    }
    
    public void JumpOn()
    {
        DeathAudio.Play();
        anime.SetTrigger("death");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    
    public void Death()
    {
        Destroy(gameObject);
    }
}
