using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountains : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        MoveForward();
    }
    private void MoveForward()
    {
        this.transform.position = new Vector3(player.transform.position.x + 7.38f, player.transform.position.y + 2.6f, transform.position.z);
    }
}
