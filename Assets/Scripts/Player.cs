using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private readonly float velocity = 15f;
    public bool isJumping = false;
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
            isJumping = false;
            isGrounded = false;
            //StartCoroutine(Jump());
            rb.velocity = new Vector2(rb.velocity.x / 5, 25);
            anim.Play("Jump");
        }
        if (isGrounded)
        {
            MoveForward();
        }
        
        gameManager.IncreaseScore();
        ZeroVelocityCheck();
    }

    private void MoveForward()
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
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
