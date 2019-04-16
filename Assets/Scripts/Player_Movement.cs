using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Movement : MonoBehaviour
{

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    const float groundedRadius = .2f;
    public float speed = 1.5f;
    public float jumpForce = 10f;
    float velX;
    float velY;
    bool facingRight = true;
    Rigidbody2D rigBody;
    private CircleCollider2D circleCollider2d;
    private bool grounded;
    public UnityEvent OnLandEvent;
    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        rigBody = GetComponent<Rigidbody2D>();

        if(OnLandEvent == null)
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
        rigBody.velocity = new Vector2(velX * speed, velY);

        //use space to jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rigBody.velocity = Vector2.up * jumpForce;
            animator.SetBool("isJumping", true);
        }
    }

    public void onLanding()
    {
        animator.SetBool("isJumping", false);
    }

    void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);

        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    void LateUpdate()
    {
        //flip player 
        Vector3 localScale = transform.localScale;
        if(velX > 0)
        {
            facingRight = true;
        }
        else if(velX < 0)
        {
            facingRight = false;
        }
        if(((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }
}
