using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullhearts;
    public Sprite emptyhearts;
    public Rigidbody2D rigBody;
    public CircleCollider2D circleCollider2D;
    public bool dead = false;
    Animator anim;

    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        rigBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullhearts;
            }
            else
            {
                hearts[i].sprite = emptyhearts;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if(health <= 0)
        {
            dead = true;
            Destroy(rigBody);
            Destroy(circleCollider2D);
            anim.SetBool("isDead", true);
            GetComponent<Boss>().enabled = false;
        }
    }
}

