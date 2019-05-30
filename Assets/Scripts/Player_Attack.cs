using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    private float timeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public bool canAttack = true;
    private float timer = 0.0f;
    public float waitingTime = 0.1f;
    public AudioSource sound;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        timeBtwAttack += Time.deltaTime;
        if(timeBtwAttack > 0.5)
        {
            canAttack = true;
        }
        anim.SetTrigger("stopAttack");
        if (canAttack)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                canAttack = false;
                timeBtwAttack = 0;
                anim.SetTrigger("attack");
                sound.Play();
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<BossHealth>().health -= 1;
                }
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
