using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    public PlayerMovement playerMovement;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            playerMovement.KnockbackCounter = playerMovement.KnockbackTotalTime;

            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockbackFromRight = true;
                Debug.Log("Hit");


            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockbackFromRight = false;
                Debug.Log("Hit");

            }
        }
    }
    
}
