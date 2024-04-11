using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        // lets us use arrow keys for movement

        if (Input.GetKey(KeyCode.Space))
            body.velocity = new Vector2(body.velocity.x, speed);
        // to jump using space bar
        
        if (Input.GetKey(KeyCode.Space) && grounded) // only allows jumping when on the ground
            Jump();
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
        // to jump using space bar
    }
}