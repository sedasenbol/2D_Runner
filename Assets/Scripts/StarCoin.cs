using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Destroy(this.gameObject);
        }
    }
}
