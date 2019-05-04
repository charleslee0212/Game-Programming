using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Movement : MonoBehaviour
{

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    const float groundedRadius = .2f;
    public float speed = 10f;
    public float jumpForce = 10f;
    public float velX;
    float velY;
    bool facingRight = true;
    Rigidbody2D rigBody;
    private CircleCollider2D circleCollider2d;
    private bool grounded;
    public UnityEvent OnLandEvent;
    public Animator animator;
    public Vector2 playerVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        rigBody = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rigBody.velocity.y;

        animator.SetFloat("speed", Mathf.Abs(velX));

        //use left and right arrow keys to move
        rigBody.AddForce(new Vector2(velX * speed, 0));
        //rigBody.velocity = new Vector2(velX * speed, velY);
        playerVelocity = rigBody.velocity;
        //use space to jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rigBody.velocity = Vector2.up * jumpForce;
            animator.SetBool("isJumping", true);
        }

        //flip player 
        Vector3 localScale = transform.localScale;
        if (velX > 0)
        {
            facingRight = true;
        }
        else if (velX < 0)
        {
            facingRight = false;
        }
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;

        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }

    }

    public void onLanding()
    {
        animator.SetBool("isJumping", false);
    }
}