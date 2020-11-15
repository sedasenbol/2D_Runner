using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private readonly float forwardVelocity = 10f;
    private bool isJumping = false;
    private float jumpForce = 7.5f;
    private Animator anim;
    private bool isAlive = true;
    private GameManager gameManager;
    private Vector3 prePosition;
    private void Start()
    {
        transform.position = new Vector3(-7,-3,-4);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.Play("Run");
        gameManager = FindObjectOfType<GameManager>();
        prePosition = transform.position;
    }
    private void Update()
    {
        if (new List<GameState.State>{GameState.State.OnPlay, GameState.State.Restarted,GameState.State.Replaying}.Contains(gameManager.StateOfTheGame.state))
        {
            isAlive = true;
        }
        else
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }
        MoveForward();
        if (isJumping)
        {
            Jump();
            isJumping = false;
        }
        ZeroVelocityCheck();
    }

    private void MoveForward()
    {
        rb.velocity = new Vector2(forwardVelocity, rb.velocity.y);
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        anim.Play("Jump");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 && isAlive && ZeroVelocityCheck())
        {
            Die();
        }
        else if (collision.gameObject.layer == 9 && Input.GetKey(KeyCode.Space) && isAlive)
        {
            isJumping = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (new List<int> {8, 10}.Contains(collision.gameObject.layer) && isAlive)
        {
            Die();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12 && isAlive)
        {
            gameManager.GetCoins();
        }
    }
    private void Die()
    {
        anim.Play("Death");
        isAlive = false;
        gameManager.IsPlayerDead();
    }
    private bool ZeroVelocityCheck()
    {
        if (transform.position != prePosition)
        {
            prePosition = transform.position;
            return false;
        }
        else
        {
            return true;
        }
    }
}
