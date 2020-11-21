using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private const float FORWARD_VELOCITY = 15f;
    private const float JUMP_VELOCITY = 30f;
    public bool isJumping = false;
    private bool isGrounded = true;
    private Animator anim;
    private GameManager gameManager;
    private readonly Vector3 startPos = new Vector3(-17f, -4f, -4f);
    private const int FLYING_TIME = 5;
    private SpawnManager spawnManager;
    private void Start()
    {
        transform.position = startPos;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.Play("Run");
        gameManager = FindObjectOfType<GameManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && gameManager.StateOfTheGame.isAlive && !isJumping)
        {
            isJumping = true;
        }
        if(isGrounded && anim.enabled == false)
        {
            anim.enabled = true;
            anim.Play("Run");
            spawnManager.isPlayerFlying = false;
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
        rb.velocity = new Vector2(FORWARD_VELOCITY, rb.velocity.y);
    }
    private void Jump()
    {
        isJumping = false;
        isGrounded = false;
        rb.velocity = new Vector2(FORWARD_VELOCITY / 5, JUMP_VELOCITY);
        anim.Play("Jump");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (new List<int> { 8, 10 }.Contains(collision.gameObject.layer) && gameManager.StateOfTheGame.isAlive)
        {
            rb.velocity = new Vector2(0,0);
            gameManager.IsPlayerDead();
            anim.Play("Death");
            if (collision.gameObject.layer == 10)
            {
                collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = true; 
            }
        }
        else if (collision.gameObject.layer == 9 && ZeroVelocityCheck())
        {
            isGrounded = true;
        }
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
        if (collision.gameObject.layer == 14 && gameManager.StateOfTheGame.isAlive)
        {
            FlyingPowerUp();
        }
    }
    private void FlyingPowerUp()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.isKinematic = true;
        rb.position = new Vector3(transform.position.x, spawnManager.playerFlyingHeight, transform.position.z);
        rb.gravityScale = 0;
        anim.Play("Jump");
        anim.enabled = false;
        spawnManager.isPlayerFlying = true;
        rb.rotation = 30f;
        StartCoroutine(HitTheGround());
    }
    IEnumerator HitTheGround()
    {
        yield return new WaitForSeconds(FLYING_TIME);
        rb.gravityScale = 1;
        rb.rotation = 0f;
        rb.isKinematic = false;
    }
    private bool ZeroVelocityCheck()
    {
        if (rb.velocity.sqrMagnitude <= 1E-3f)
        {
            return true;
        }
        return false;
    }
    public void StartAgain()
    {
        transform.position = startPos;
        anim.Play("Run");
    }
}
