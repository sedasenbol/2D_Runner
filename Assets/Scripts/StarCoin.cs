using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCoin : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Destroy(this.gameObject);
        }
    }
}
