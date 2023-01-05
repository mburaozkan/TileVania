using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float velocityC;
    [SerializeField] float jumpSpeed;
    [SerializeField] float gravScale;
    [SerializeField] Vector2 deadKick;   
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;    
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myCollider;
    BoxCollider2D feetCollider;
    Animator myAnimator;
    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive){ return;}
        Run();
        Climb();
        FlipSide();
        CheckDied();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive){ return;}
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive){ return;}
        if (value.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {    
                Vector2 velocitVec = new Vector2(myRigidbody.velocity.x, jumpSpeed);
                myRigidbody.velocity = velocitVec;
        }
    }
    
    void OnFire(InputValue value)
    {
        if (!isAlive){ return;}
        Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnMap(InputValue value)
    {
        myAnimator.SetBool("isMap", true);
    }

    void Run()
    {
        Vector2 velocitVec = new Vector2(moveInput.x*velocityC, myRigidbody.velocity.y);
        myRigidbody.velocity = velocitVec;

        if (Mathf.Abs(velocitVec.x) > Mathf.Epsilon)
        {    
            myAnimator.SetBool("isRunning", true);
            myAnimator.SetBool("isMap", false);
        } else {
            myAnimator.SetBool("isRunning", false);
        }
    }

    void Climb()
    {
        Vector2 velocitVec = new Vector2(myRigidbody.velocity.x, moveInput.y*velocityC);

        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        { 
            myRigidbody.gravityScale = 0f;
            myRigidbody.velocity = velocitVec;

            if (Mathf.Abs(velocitVec.y) > Mathf.Epsilon)
            {    
                myAnimator.SetBool("isClimbing", true);
                myAnimator.SetBool("isMap", false);
            } else {
                myAnimator.SetBool("isClimbing", false);
            }
        } else {
            myRigidbody.gravityScale = gravScale;
            myAnimator.SetBool("isClimbing", false);
        }
    }

    void FlipSide()
    {
        bool hasSpeed = (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);

        if (hasSpeed)
        {
             transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);

        }
    }

    void CheckDied()
    {
        isAlive = !myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"));

        if (!isAlive)
        {
            myAnimator.SetTrigger("Died");
            myAnimator.SetBool("isMap", false);
            myRigidbody.velocity = deadKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
