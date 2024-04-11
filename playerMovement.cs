using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float speed; 
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

     private void Awake()
    {
        // get references for rigidbody and animator from game object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
{
    horizontalInput = Input.GetAxis("Horizontal");
    // lets us use arrow keys for movement

    if (horizontalInput > 0.01f)
        transform.localScale = Vector3.one;
    // faces right if moving right

    else if (horizontalInput < -0.01f)
        transform.localScale = new Vector3(-1, 1, 1);
    // faces left if moving left

    if (Input.GetKey(KeyCode.Space) && isGrounded()) // only allows jumping when on the ground
        Jump();
        

    anim.SetBool("run", horizontalInput != 0); // ie if keys aren't pressed then false, if they are then true!
    anim.SetBool("grounded", isGrounded());
    // sets animator parameters i.e. for transitions

    if (wallJumpCooldown > 0.2f)
    {

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        // wall jumping

        if (onWall() && !isGrounded())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        // lets player attach to wall
        else
            body.gravityScale = 5;

        if (Input.GetKey(KeyCode.Space))
            Jump();
    }
    else
        wallJumpCooldown += Time.deltaTime; 
            // time between jumps
}

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
        // to jump using space bar
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}