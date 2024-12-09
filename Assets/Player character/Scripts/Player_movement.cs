using System.Collections;
using UnityEngine;

public class Player_movement : MonoBehaviour
{

    public float acceleration;

    public float groundSpeed;
    public float jumpSpeed;

    [Range(0f, 1f)]
    public float groundDecay;
    public Rigidbody2D body;

    public bool grounded;
    public bool touchingWall;

    public Animator animator;

    public BoxCollider2D groundCheck;
    public BoxCollider2D wallCheck;

    public LayerMask groundMask;
    public LayerMask wallMask;

    private float xInput;
    private float yInput;

    private bool canDash = true;
    private bool isDashing;
    private bool isWallJumping;
    private float wallJumpingDuration = 0.4f;
    private float wallJumpingCounter;
    private float dashPower = 5f;
    private float dashTime = 0.2f;
    private float dashCooldown = 0.35f;

    [SerializeField] private TrailRenderer trail;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        HandleWallJump();

        if (!isWallJumping)
        {
            getInput();
            handleDash();
        }
        
        handleJump();

        checkForAnimations();

    }

    private void checkForAnimations()
    {
        checkForRunAnimations();
    }

    private void checkForRunAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(body.linearVelocity.x));
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        checkGround();
        checkWall();

        if (!isWallJumping)
        {
            moveWithInput();
        }

        applyFriction();

    }

    void applyFriction()
    {
        if (grounded && xInput == 0 && body.linearVelocity.y <=0)
        {
            body.linearVelocity *= groundDecay;
        }
    }

    void getInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    void moveWithInput()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(body.linearVelocity.x + increment,-groundSpeed,groundSpeed);
            body.linearVelocity = new Vector2(newSpeed, body.linearVelocity.y);

            

            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction, 1, 1);
        }

        
    }
    
    void handleJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
            animator.SetBool("isJumping", true);
        }
    }

    void HandleWallJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && !grounded && touchingWall)
        {
            float wallJumpingDirection = -transform.localScale.x;
            body.linearVelocity = new Vector2(wallJumpingDirection * groundSpeed, jumpSpeed);
            transform.localScale = new Vector3(wallJumpingDirection, 1, 1);
            isWallJumping = true;
            wallJumpingCounter = wallJumpingDuration;
            animator.SetBool("isJumping", true);
        }

        if (isWallJumping && wallJumpingCounter > 0f)
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        else if(isWallJumping)
        {
            isWallJumping = false;
        }
    }

        void handleDash()
    {
        if (canDash)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Dash());
            }
            else if (Input.GetKeyDown(KeyCode.Space)&&grounded)
            {
                StartCoroutine(Dash(true));
            }
        }
        
    }

    

    void checkGround()
    {
        bool previousGrounded = grounded;
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
        if (!previousGrounded && grounded)
        {
            animator.SetBool("isJumping", false);
        }
    }

    void checkWall()
    {
        touchingWall = Physics2D.OverlapAreaAll(wallCheck.bounds.min, wallCheck.bounds.max, wallMask).Length > 0;
        
    }

    private IEnumerator Dash(bool backwards = false)
    {
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0;
        float direction = Mathf.Sign(xInput) * (backwards ? -1 : 1);
        body.linearVelocity = new Vector2(direction * dashPower, 0f);

        trail.emitting = true;

        yield return new WaitForSeconds(dashTime);
        trail.emitting = false;
        body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
