using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameManager gameManager;
    private Player playerScript;
    private Transform playerTransform;
    private Camera cam;
    private float yMaxOfCam;
    private float yMinOfCam;
    private float yStep;
    private void Move()
    {
        yMaxOfCam = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        yMinOfCam = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        if (!gameManager.StateOfTheGame.IsAlive || playerScript.IsFlying)
        {
            transform.position = new Vector3(playerTransform.position.x + 1f, playerTransform.position.y + 0.5f, transform.position.z);
        }
        else if (gameManager.StateOfTheGame.IsAlive && playerTransform.position.y < yMaxOfCam - 3f && playerTransform.position.y > yMinOfCam + 3f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(playerTransform.position.x + 7f, transform.position.y, transform.position.z), 30 * Time.deltaTime);
        }
        else if (gameManager.StateOfTheGame.IsAlive && playerTransform.position.y >= yMaxOfCam - 3f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(playerTransform.position.x + 7f, transform.position.y + yStep, transform.position.z), 30 * Time.deltaTime);
        }
        else if (gameManager.StateOfTheGame.IsAlive && playerTransform.position.y <= yMinOfCam + 3f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(playerTransform.position.x + 7f, transform.position.y - yStep, transform.position.z), 30 * Time.deltaTime);
        }
        gameManager.GetCameraPosition(transform.position);
    }
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.GetCameraPosition(transform.position);
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        cam = GetComponent<Camera>();
        yStep = GameObject.FindObjectOfType<SpawnManager>().VerticalGap;
    }
    private void Update()
    {
        Move();
    }
}
