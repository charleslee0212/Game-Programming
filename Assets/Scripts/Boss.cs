using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform target;
    private Transform myTransform;
    public static bool isAttacking = false;
    Rigidbody2D rb;
    Animator anim;

    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

   

    void Update()
    {
        //flip player 
        Vector3 localScale = transform.localScale;

        if (target.position.x < myTransform.position.x && (localScale.x < 0) || target.position.x > myTransform.position.x && (localScale.x > 0))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;

        if (isAttacking)
        {
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

}
