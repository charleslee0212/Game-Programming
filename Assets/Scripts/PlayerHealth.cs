using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullhearts;
    public Sprite emptyhearts;

    public Rigidbody2D rigBody;
    public CircleCollider2D circleCollider2D;

    float time = 0f;
    public int index;

    public GameObject otherObject;
    Animator otherAnim;
    public Animator myAnim;

    void Start()
    {
        otherAnim = otherObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (myAnim.GetBool("hit"))
        {
            time += Time.deltaTime;

            if (time > 0.5)
            {
                GetComponent<Player_Movement>().enabled = true;
                GetComponent<Player_Attack>().enabled = true;
                GetComponent<PlayerDash>().enabled = true;

                myAnim.SetBool("hit", false);

                time = 0;
            }
        }
        if (health > numOfHearts)
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
                time += Time.deltaTime;
                otherAnim.SetTrigger("idle");
                otherAnim.SetBool("isAttacking", false);
                //Destroy(gameObject);
                myAnim.SetBool("isDead", true);

                Destroy(rigBody);
                Destroy(circleCollider2D);

                GetComponent<Player_Movement>().enabled = false;
                GetComponent<Player_Attack>().enabled = false;
                GetComponent<PlayerDash>().enabled = false;

                if(time > 3)
                {
                    SceneManager.LoadScene(index);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("attack"))
        {
            if(health > 1)
            {
                myAnim.SetBool("hit", true);

                GetComponent<Player_Movement>().enabled = false;
                GetComponent<Player_Attack>().enabled = false;
                GetComponent<PlayerDash>().enabled = false;
            }

            health -= 1;
        }
    }
}
