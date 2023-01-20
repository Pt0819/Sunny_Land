using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy_Slime : Enemy
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform leftPos, rightPos;
    [SerializeField] private float speed; 
    [SerializeField] private bool Faceleft = true;
    
    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;
    
    [SerializeField] private Collider2D collider;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        transform.DetachChildren();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        
        leftBound = leftPos.position.x;
        rightBound = rightPos.position.x;
        
        Destroy(leftPos.gameObject);
        Destroy(rightPos.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Faceleft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x <= leftBound)
            {
                Faceleft = false;
                transform.localScale = new Vector3(-1,1,1);
            }
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x >= rightBound)
            {
                Faceleft = true;
                transform.localScale = new Vector3(1,1,1);
            }
        }
    }
}
