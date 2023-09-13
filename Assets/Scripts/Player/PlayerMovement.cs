using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; // speed for horizontal movement
    [SerializeField] private float jumpPower;// power for jumping
    [SerializeField] private LayerMask groundLayer; // layer mask to check if player is on the ground
    [SerializeField] private LayerMask wallLayer; // layer mask to check if player is on the wall
    private Rigidbody2D body; // reference to the Rigidbody2D component of the player object
    private Animator anim; // reference to the Animator component of the player object
    private CapsuleCollider2D capsuleCollider; // reference to the CapsuleCollider2D component of the player object
    private float wallJumpCooldown; // cooldown for wall jumping
    private float horizontalInput; // value for horizontal input from the player

    //Initialises

    private void Awake()
    {
        // get references to components on the player object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        // get horizontal input from player
        horizontalInput = Input.GetAxis("Horizontal");

        // check if player is moving left or right
        // and change the player's local scale accordingly
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*-1, transform.localScale.y, transform.localScale.z);


        // set animator parameters for run and grounded
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        //Wall Jump Logic
        if (wallJumpCooldown > 0.2f)
        {

            // update player velocity based on horizontal input and speed
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {

                // if player is on wall, set gravity scale to 0
                // otherwise, set gravity scale to 7
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;
            // check if player presses space, and if so, call the Jump() function
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

        }
        else
            wallJumpCooldown += Time.deltaTime;

    }

    // function to handle jumping
    private void Jump()
    {
        // check if player is on the ground
        // if so, set player velocity for jumping
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            // check if player is on the wall
            // if so, set player velocity for wall jumping
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
            
        }
        
        
    }



    //The "isGrounded" function checks if the player is touching the ground by using a box cast to check for collisions with the ground layer.

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size,0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    //The "onWall" function checks if the player is touching a wall by using a box cast to check for collisions with the wall layer.
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

}
