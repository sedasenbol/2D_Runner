using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private readonly float forwardVelocity = 10f;
    private bool isJumping = false;
    private float jumpForce = 7f;
    private Animator anim;
    private bool isAlive = true;
    private CameraFollow cameraFollow;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.Play("Run");
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    private void Update()
    {

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
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
        if (collision.gameObject.layer == 8 && isAlive)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        isAlive = false;
        Application.Quit();
        anim.Play("Death");
        cameraFollow.GameOver();
    }
}
