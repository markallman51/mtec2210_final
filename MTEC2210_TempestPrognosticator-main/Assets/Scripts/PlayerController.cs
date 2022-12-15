using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


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
    public bool doubleJumpActive = false;

    public float groundCheckDistance;
    public float landCheckDistance;
    public Vector3 groundCheckOffset;

    //phasing
    private bool movingLeft = false;
    private bool movingRight = false;
    private bool phasing = false;
    public float phaseForce = 6;
    public Collider2D col;
    public bool phaseActive = false; 

    //sprite
    public SpriteRenderer ghost;

    //animation
    public Animator anime;
    public bool grounded;
    
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
        grounded = GroundCheck();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*
            On Space press:
            -if on ground: reset previous double jump, then jump
            -if not on ground, but double jump available: mark double jump as used, jump again
             */

            if (grounded)
            {
                doubleJumpUsed = false;
                jumped = true;
                //anime.SetTrigger("Jumping");
                Debug.Log(anime.GetBool("Jumping"));
                //Landing();
            }

            else if (!doubleJumpUsed && doubleJumpActive)
            {
                doubleJumpUsed = true;
                jumped = true;
                //anime.SetTrigger("Jumping");
                Debug.Log(anime.GetBool("Jumping"));
                //Landing();
            }
        }

        //PHASING
        if (Input.GetMouseButtonDown(0) && phaseActive)
        {
            phasing = true;
        }

        //ANIMATION
        if (xVel == 0)
            anime.SetBool("Moving", false);
        else
            anime.SetBool("Moving", true);

        if (grounded)
            anime.SetBool("Jumping", false);

        else
            anime.SetBool("Jumping", true);




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

    private void Landing()
    {
        Debug.Log("check for landing");

        bool landed = (
        Physics2D.Raycast(transform.position + groundCheckOffset, Vector3.down, landCheckDistance, Ground) ||
        Physics2D.Raycast(transform.position - groundCheckOffset, Vector3.down, landCheckDistance, Ground));
        Debug.Log(landed);

        if(landed == false)
        {
            landed = (
            Physics2D.Raycast(transform.position + groundCheckOffset, Vector3.down, landCheckDistance, Ground) ||
            Physics2D.Raycast(transform.position - groundCheckOffset, Vector3.down, landCheckDistance, Ground));
        }
            

        if (landed == true)
        {
            anime.SetBool("Jumping", false);
            Debug.Log("i've landed");
        }
            

        Debug.Log(anime.GetBool("Jumping"));

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DoubleJump")
            doubleJumpActive = true;

        if (col.gameObject.tag == "Phase")
            phaseActive = true;
    }


}
