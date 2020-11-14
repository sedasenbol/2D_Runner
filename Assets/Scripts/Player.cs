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
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.Play("Run");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isAlive)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
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
        if (collision.gameObject.layer == 9 && Input.GetKey(KeyCode.Space) && isAlive)
        {
            isJumping = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (new List<int> {8, 10}.Contains(collision.gameObject.layer) && isAlive)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        isAlive = false;
        Application.Quit();
        anim.Play("Death");
        gameManager.GameOver();
    }
}
