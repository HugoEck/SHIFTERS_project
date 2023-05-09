using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour
{
    [SerializeField] int lapCompleted = 0;
    [SerializeField] int checkpointsPassed = 0;
    [SerializeField] int totalCheckpoints = 0;
    [SerializeField] int lapsToWin = 0;
    public Text winText;
    [SerializeField] int numberFinishedPlayers = 0;
    private List<GameObject> finishedPlayers;

    void Start()
    {
        finishedPlayers = new List<GameObject>();
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FinishLine"))
        {
            if(checkpointsPassed == totalCheckpoints) 
            {
                lapCompleted++;
                checkpointsPassed = 0;
             


                Debug.Log("Lap Completed");

                if(lapCompleted == lapsToWin)
                {
                    numberFinishedPlayers++;
                    finishedPlayers.Add(gameObject);

                    //winText.gameObject.SetActive(true);
                    Debug.Log("You win!");
                    Debug.Log("Player " + numberFinishedPlayers + " finished in position " + finishedPlayers.Count);
                    
                    if (numberFinishedPlayers == 4)
                    {
                        Debug.Log("Race finished!");
                    }
                }

            }
            else 
            {
                Debug.Log("Must pass checkpoints to complete lap!");
            }
        }
        else if (collision.CompareTag("CheckPoint") && checkpointsPassed == 0) 
        { 
            checkpointsPassed++;
            Debug.Log("Checkpoint passed");
            
        }
        
    }
}

