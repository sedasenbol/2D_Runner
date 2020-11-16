using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private readonly float forwardVelocity = 10f;
    private bool isJumping = false;
    private bool isGrounded = true;
    private Animator anim;
    private GameManager gameManager;
    private Vector3 prePosition;
    private void Start()
    {
        transform.position = new Vector3(-17,-4,-4);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.Play("Run");
        gameManager = FindObjectOfType<GameManager>();
        prePosition = transform.position;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded && gameManager.StateOfTheGame.isAlive && !isJumping)
        {
            isGrounded = false;
            isJumping = true;
        }

    }

    private void FixedUpdate()
    {
        if (!gameManager.StateOfTheGame.isAlive)
        {
            return;
        }
        if (isJumping)
        {
            rb.velocity = new Vector2(0, 0);
            StartCoroutine(Jump());
            isGrounded = false;
            //rb.AddForce(new Vector2(0, rb.velocity.x*4));
            //isJumping = false;
            anim.Play("Jump");
        }
        else if (isGrounded)
        {
            MoveForward();
        }
        gameManager.IncreaseScore();
        ZeroVelocityCheck();
    }

    private void MoveForward()
    {
        rb.velocity = new Vector2(forwardVelocity, rb.velocity.y);
    }
    private IEnumerator Jump()
    {
        float startingYPos = transform.position.y;
        while (!isGrounded && isJumping)
        {
            for (int i = 0; i<30; i++)
            {
                rb.velocity = new Vector2(rb.velocity.x, 5);
                //rb.AddForce(new Vector2(0, -20));
                yield return new WaitForEndOfFrame();
            }
            rb.velocity = new Vector2(0,0);
            for (int i=1; i<30; i++)
            {
                rb.velocity = new Vector2(rb.velocity.x, -5);
                //rb.AddForce(new Vector2(0, -20));
                yield return new WaitForEndOfFrame();
            }
        }    
        isJumping = false;   
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        /*if (collision.gameObject.layer == 9 && gameManager.StateOfTheGame.isAlive && ZeroVelocityCheck())
        {
            anim.Play("Death");
            gameManager.IsPlayerDead();
        }
        else*/ if (collision.gameObject.layer == 9)
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (new List<int> {8, 10}.Contains(collision.gameObject.layer) && gameManager.StateOfTheGame.isAlive)
        {
            gameManager.IsPlayerDead();
            anim.Play("Death");
        }
        if(collision.gameObject.layer == 9 && !ZeroVelocityCheck())
        {
            isGrounded = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12 && gameManager.StateOfTheGame.isAlive)
        {
            gameManager.GetCoins();
        }
        if (collision.gameObject.layer == 13 && gameManager.StateOfTheGame.isAlive)
        {
            gameManager.GetHearts();
        }
    }
    private bool ZeroVelocityCheck()
    {
        if (rb.velocity != new Vector2(0,0))
        {
            prePosition = transform.position;
            return false;
        }
        else if (!isJumping && !isGrounded)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void StartAgain()
    {
        transform.position = new Vector3(-7, -3, -4);
        anim.Play("Run");
    }
}
