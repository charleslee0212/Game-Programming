using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullhearts;
    public Sprite emptyhearts;

    public GameObject otherObject;
    Animator otherAnim;

    void Start()
    {
        otherAnim = otherObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
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

            if (health <= 0)
            {
                otherAnim.SetTrigger("idle");
                otherAnim.SetBool("isAttacking", false);
                //Destroy(gameObject);

                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("attack"))
        {
            health -= 1;
        }
    }

}
