using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            transform.position = new Vector3(1f,50f,0.317f); 
        }
    }
}
