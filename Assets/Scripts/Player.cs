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
        transform.position = new Vector3(-7,-3,-4);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.Play("Run");
        gameManager = FindObjectOfType<GameManager>();
        prePosition = transform.position;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded && gameManager.StateOfTheGame.isAlive)
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
        if (isJumping)
        {
            StartCoroutine(Jump());
            gameManager.IncreaseScore();
            anim.Play("Jump");
        }
        else
        {
            MoveForward();
            gameManager.IncreaseScore();
        }
        ZeroVelocityCheck();
    }

    private void MoveForward()
    {
        rb.velocity = new Vector2(forwardVelocity, rb.velocity.y);
    }
    private IEnumerator Jump()
    {
        for (int i=0; i< 10; i++)
        {
            transform.position = transform.position + new Vector3(0.01f, 0.5f,0);
            if (isGrounded)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        for (int i = 0; i < 10; i++)
        {
            if (isGrounded)
            {
                break;
            }
            transform.position = transform.position - new Vector3(-0.01f, 0.5f, 0);
            yield return new WaitForEndOfFrame();
        }
        isJumping = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 && gameManager.StateOfTheGame.isAlive && ZeroVelocityCheck())
        {
            Debug.Log("Dead");
            gameManager.IsPlayerDead();
        }
        else if (collision.gameObject.layer == 9)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (new List<int> {8, 10}.Contains(collision.gameObject.layer) && gameManager.StateOfTheGame.isAlive)
        {
            gameManager.IsPlayerDead();
            anim.Play("Dead");
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
