using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private readonly float forwardVelocity = 15f;
    private readonly float jumpVelocity = 30f;
    public bool isJumping = false;
    private bool isGrounded = true;
    private Animator anim;
    private GameManager gameManager;
    private Vector3 startPos = new Vector3(-17f, -4f, -4f);
    private bool justStarted = true;
    private void Start()
    {
        transform.position = startPos;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.Play("Run");
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && gameManager.StateOfTheGame.isAlive && !isJumping)
        {
            isJumping = true;
        }

    }

    private void FixedUpdate()
    {
        if (!gameManager.StateOfTheGame.isAlive)
        {
            return;
        }
        if (isJumping && isGrounded)
        {
            Jump();
        }
        if (isGrounded)
        {
            MoveForward();
        }
        gameManager.IncreaseScore();
    }

    private void MoveForward()
    {
        rb.velocity = new Vector2(forwardVelocity, rb.velocity.y);
    }
    private void Jump()
    {
        isJumping = false;
        isGrounded = false;
        rb.velocity = new Vector2(rb.velocity.x / 5, jumpVelocity);
        anim.Play("Jump");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
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
        else if(collision.gameObject.layer == 9 && !ZeroVelocityCheck())
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
            return false;
        }
        return true;
    }
    public void StartAgain()
    {
        justStarted = true;
        transform.position = startPos;
        anim.Play("Run");
    }
}
