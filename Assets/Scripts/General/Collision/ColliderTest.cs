using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    private List<GameObject> playerObjects = new List<GameObject>();

    private IEnumerator WaitForPlayer()
    {
        while (true)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
            playerObjects = objectsWithTag.ToList();
            yield return new WaitForSeconds(1f); // update the list every 1 second
        }
    }

    private void Start()
    {
        // Start the coroutine to wait for the player objects to become active
        StartCoroutine(WaitForPlayer());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (GameObject playerObject in playerObjects)
        {
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();

            if (playerMovement == null)
            {
                continue;
            }

            if (collision.collider.gameObject == playerObject)
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
}
