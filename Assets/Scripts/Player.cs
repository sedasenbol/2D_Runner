using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float FLYING_TIME = 5;
    private const float FLYING_HEIGHT= 100;
    private const float FORWARD_VELOCITY = 15f;
    private const float JUMP_VELOCITY = 30f;
    private readonly Vector3 startPos = new Vector3(-17f, -4f, -4f);
    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isFlying = false;
    private Rigidbody2D rb;
    private Animator anim;
    private GameManager gameManager;
    public void StartAgain()
    {
        transform.position = startPos;
        anim.Play("Run");
    }
    private void MoveForward()
    {
        rb.velocity = new Vector2(FORWARD_VELOCITY, rb.velocity.y);
    }
    private void Jump()
    {
        rb.velocity = new Vector2(FORWARD_VELOCITY, JUMP_VELOCITY);
        isJumping = false;
        anim.Play("Jump");
    }
    private void FlyingPowerUp()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.isKinematic = true;
        rb.position = new Vector3(transform.position.x, FLYING_HEIGHT, transform.position.z);
        rb.gravityScale = 0;
        anim.Play("Jump");
        anim.enabled = false;
        isFlying = true;
        rb.rotation = 30f;
        StartCoroutine(HitTheGround());
    }
    private IEnumerator HitTheGround()
    {
        yield return new WaitForSeconds(FLYING_TIME);
        rb.gravityScale = 1;
        rb.rotation = 0f;
        rb.isKinematic = false;
        isFlying = false;
    }
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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && gameManager.StateOfTheGame.IsAlive && !isJumping)
        {
            isJumping = true;
        }
        if(isGrounded && anim.enabled == false && !isFlying)
        {
            anim.enabled = true;
            anim.Play("Run");
        }
    }
    private void FixedUpdate()
    {
        if (!gameManager.StateOfTheGame.IsAlive)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (new List<int> { 8, 10 }.Contains(collision.gameObject.layer) && gameManager.StateOfTheGame.IsAlive)
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
        else if (collision.gameObject.layer == 9)
        {
            //isGrounded = true;
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
        if (collision.gameObject.layer == 12 && gameManager.StateOfTheGame.IsAlive)
        {
            gameManager.GetCoins();
        }
        if (collision.gameObject.layer == 13 && gameManager.StateOfTheGame.IsAlive)
        {
            gameManager.GetHearts();
        }
        if (collision.gameObject.layer == 14 && gameManager.StateOfTheGame.IsAlive)
        {
            FlyingPowerUp();
        }
    }
    public bool IsJumping { get { return isJumping; } }
    public bool IsFlying { get { return isFlying; } }
    public float FlyingHeight { get { return FLYING_HEIGHT; } }
}
