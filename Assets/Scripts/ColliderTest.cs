using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerMovement playerMovement2;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player" || collision.collider.gameObject.tag == "Player2") 
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            playerMovement2.KBCounter = playerMovement2.KBTotalTime;

            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
                playerMovement2.KnockFromRight = true;
                Debug.Log("Hit");


            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockFromRight = false;
                playerMovement2.KnockFromRight = false;
                Debug.Log("Hit");

            }
        }
    }
}
