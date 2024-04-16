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
     if(isGrounded())
     {
         body.velocity = new Vector2(body.velocity.x, jumpPower);
         anim.SetTrigger("jump");
         // to jump using space bar
     }
     // handle jump if on ground

     else if (onWall() && !isGrounded())
     {
         if (horizontalInput == 0)
         {
             body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
             transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
         }
         else
             body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
         // Mathf returns 1 when facing right, -1 when facing left, -Mathf makes player move away from wall
         // 3 is x movement from wall
         // 6 is y movement from wall

         wallJumpCooldown = 0;
         
     }
     // handle jump if on wall
 }

     private void OnCollisionEnter2D(Collision2D collision)
{
}

    private bool isGrounded()
{
    RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
    return raycastHit.collider != null;
}
// different way of seeing if player on ground

private bool onWall()
{
    RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
    return raycastHit.collider != null;
}
// see if player on the wall/in vicinity of the wall

    private bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
    // dictates when player can attack
}