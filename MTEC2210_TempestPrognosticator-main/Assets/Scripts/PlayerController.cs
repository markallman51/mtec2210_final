using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movement
    public float speed;

    //jumping
    public Rigidbody2D rb;
    public float jumpForce = 10;
    private bool jumped = false;
    private bool doubleJumpUsed = false;
    public LayerMask Ground;

    public float groundCheckDistance;
    public Vector3 groundCheckOffset;

    //phasing
    private bool movingLeft = false;
    private bool movingRight = false;
    private bool phasing = false;
    public float phaseForce = 6;
    public Collider2D col;

    //sprite
    public SpriteRenderer ghost;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float xVel = Input.GetAxisRaw("Horizontal");
        transform.Translate(xVel * speed * Time.deltaTime, 0, 0);

        //SPRITE FLIP FOR MOVEMENT
        if (xVel > 0)//right
        {
            ghost.flipX = false;
            movingLeft = false;
            movingRight = true;
        }
            

        if (xVel < 0)//left
        {
            ghost.flipX = true;
            movingLeft = true;
            movingRight = false ;
        }
            


        //JUMPING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*
            On Space press:
            -if on ground: reset previous double jump, then jump
            -if not on ground, but double jump available: mark double jump as used, jump again
             */
            
            if (GroundCheck())
            {
                doubleJumpUsed = false;
                jumped = true;
                //Debug.Log("I jumped");
            }

            else if (!doubleJumpUsed)
            {
                doubleJumpUsed = true;
                jumped = true;
                //Debug.Log("I jumped AGAIN");
            }
        }

        //PHASING
        if (Input.GetMouseButtonDown(0))
        {
            phasing = true;
        }


    }

    private void FixedUpdate()
    {
        if (jumped)
        {
            rb.velocity = Vector3.up * jumpForce;
            jumped = false;
        }

        if (phasing)
        {
            col.isTrigger = true;
            if (movingLeft)
                rb.velocity = Vector3.left * phaseForce;

            if (movingRight)
                rb.velocity = Vector3.right * phaseForce;

            col.isTrigger = false;
            phasing = false;

        }
    }

    private bool GroundCheck()
    {
        /*
        LAYERS!!! 
        Similar to tagging, but with different applications.
        Using the Collision Matrix(Edit > Project Settings > Physics 2D > scroll down) we can have object physics(collision, trigger) only
         interact with specific objects using layers.
        (i.e players can interact with the ground, and enemies, but not other players)
        Here, we're using the layer to identify what the ray is detecting. the function will only return true if it collides with an object 
         with the ground layer.
        */

        bool check = (
            Physics2D.Raycast(transform.position + groundCheckOffset, Vector3.down, groundCheckDistance, Ground) ||
            Physics2D.Raycast(transform.position - groundCheckOffset, Vector3.down, groundCheckDistance, Ground));
        
        return check;
    }

  
}
