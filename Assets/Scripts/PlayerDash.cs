using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    float velX;
    float velY;
    public float coolDown = 1;
    public float coolDownTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }
        if (coolDownTimer < 0)
        {
            coolDownTimer = 0;
        }

        if (direction == 0)
        {
            if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift) && coolDownTimer == 0)
            {
                direction = 1;
                coolDownTimer = coolDown;
            }

            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift) && coolDownTimer == 0)
            {
                direction = 2;
                coolDownTimer = coolDown;
            }
        }
        else
        {
            if(dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                velX = Input.GetAxisRaw("Horizontal");
                velY = rb.velocity.y;

                if (direction == 1)
                {
                    rb.AddForce(new Vector2(velX * dashSpeed, 0));
                }
                else
                {
                    if(direction == 2)
                    {
                        rb.AddForce(Vector2.right * dashSpeed);
                    }
                }
            }
        }
    }
}
