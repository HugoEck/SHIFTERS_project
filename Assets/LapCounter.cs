using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour
{   
    public int lapsToWin = 0;

    private PlayerInput[] _playerInput;
    // Player 1
    public static int lapCompletedPlayer1 = 0;
    public static bool bPlayer1WonRace = false;
    public static int player1WonARace;
    public static bool bPlayer1HasWonGrandPrix = false;
    // Player 2
    public static int lapCompletedPlayer2 = 0;
    public static bool  bPlayer2WonRace = false;
    public static int player2WonARace;
    public static bool bPlayer2HasWonGrandPrix = false;
    // Player 3 
    public static int lapCompletedPlayer3 = 0;
    public static bool bPlayer3WonRace = false;
    public static int player3WonARace;
    public static bool bPlayer3HasWonGrandPrix = false;
    // Player 4
    public static int lapCompletedPlayer4 = 0;
    public static bool bPlayer4WonRace = false;  
    public static int player4WonARace;
    public static bool bPlayer4HasWonGrandPrix = false;

    public static List<GameObject> finishedPlayers;

    public static bool bIsRaceFinished = false;

    public static int sceneCounter;
   
    void Start()
    {
        bIsRaceFinished = false;

        sceneCounter = PlayerPrefs.GetInt("SceneCounter");
        player1WonARace = PlayerPrefs.GetInt("Player1WonARace");
        player2WonARace = PlayerPrefs.GetInt("Player2WonARace");
        player3WonARace = PlayerPrefs.GetInt("Player3WonARace");
        player4WonARace = PlayerPrefs.GetInt("Player4WonARace");

        bPlayer1WonRace = false;
        bPlayer2WonRace = false;
        bPlayer3WonRace = false;
        bPlayer4WonRace = false;

        lapCompletedPlayer1 = 0;
        lapCompletedPlayer2 = 0;
        lapCompletedPlayer3 = 0;
        lapCompletedPlayer4 = 0;

        finishedPlayers = new List<GameObject>();
       
        _playerInput = GameObject.FindObjectsOfType<PlayerInput>();
            
    }
    private void Update()
    {               
        AnnounceWinner();  
        
        StopFinishedPlayer();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(PlayerInput player in _playerInput)
        {
            if(collision.gameObject == player.gameObject)
            {  
                if(CheckPoint_Counter.checkpointsPassedPlayer1 > 0)
                {
                    if (player.playerIndex == 0)
                    {
                        if(lapCompletedPlayer1 == lapsToWin)
                        {
                            finishedPlayers.Add(player.gameObject);
                            Debug.Log("Player 1 finished the race");
                        }
                        lapCompletedPlayer1 += 1;
                        Debug.Log("Player 1 completed a lap");
                        CheckPoint_Counter.checkpointsPassedPlayer1 = 0;
                    }
                }
                if(CheckPoint_Counter.checkpointsPassedPlayer2 > 0)
                {
                    if (player.playerIndex == 1)
                    {
                        if (lapCompletedPlayer2 == lapsToWin)
                        {
                            finishedPlayers.Add(player.gameObject);
                            Debug.Log("Player 2 finished the race");
                        }
                        lapCompletedPlayer2 += 1;
                        Debug.Log("Player 2 completed a lap");
                        CheckPoint_Counter.checkpointsPassedPlayer2 = 0;
                    }
                }
                if(CheckPoint_Counter.checkpointsPassedPlayer3 > 0)
                {
                    if (player.playerIndex == 2)
                    {
                        if (lapCompletedPlayer3 == lapsToWin)
                        {
                            finishedPlayers.Add(player.gameObject);
                            Debug.Log("Player 3 finished the race");
                        }
                        lapCompletedPlayer3 += 1;
                        Debug.Log("Player 3 completed a lap");
                        CheckPoint_Counter.checkpointsPassedPlayer3 = 0;
                    }
                }
                if(CheckPoint_Counter.checkpointsPassedPlayer4 > 0)
                {
                    if (player.playerIndex == 3)
                    {
                        if (lapCompletedPlayer4 == lapsToWin)
                        {
                            finishedPlayers.Add(player.gameObject);
                            Debug.Log("Player 4 finished the race");
                        }
                        lapCompletedPlayer4 += 1;
                        Debug.Log("Player 4 completed a lap");
                        CheckPoint_Counter.checkpointsPassedPlayer4 = 0;
                    }
                }                
            }           
        }                
    }    
    private void AnnounceWinner()
    {
        foreach(PlayerInput player in _playerInput)
        {            
            if(player.playerIndex == 0)
            {
                if(finishedPlayers.Count >= 1)
                {
                    if (player.gameObject == finishedPlayers[0])
                    {
                        if (!bPlayer1WonRace)
                        {
                            Debug.Log("Player 1 is the winner!!");
                            bPlayer1WonRace = true;
                            player1WonARace += 1;

                            PlayerPrefs.SetInt("Player1WonARace", player1WonARace);
                            PlayerPrefs.Save();
                        }
                    }
                }
                if(finishedPlayers.Count >= 2)
                {
                    if (player.gameObject == finishedPlayers[1])
                    {
                        Debug.Log("Player 1 finished in second place");
                        
                    }
                }
                if(finishedPlayers.Count >= 3)
                {
                    if (player.gameObject == finishedPlayers[2])
                    {
                        Debug.Log("Player 1 finished in third place");
                    }
                }
                if(finishedPlayers.Count >= 4)
                {
                    if (player.gameObject == finishedPlayers[3])
                    {
                        Debug.Log("Player 1 finished in fourth place");
                    }
                }                            
            }
            if (player.playerIndex == 1)
            {
                if (finishedPlayers.Count >= 1)
                {
                    if (player.gameObject == finishedPlayers[0])
                    {
                        if(!bPlayer2WonRace)
                        {
                            Debug.Log("Player 2 is the winner!!");
                            bPlayer2WonRace = true;
                            player2WonARace += 1;

                            PlayerPrefs.SetInt("Player2WonARace", player2WonARace);
                            PlayerPrefs.Save();
                        }
                    }
                }
                if (finishedPlayers.Count >= 2)
                {
                    if (player.gameObject == finishedPlayers[1])
                    {
                        Debug.Log("Player 2 finished in second place");
                    }
                }
                if (finishedPlayers.Count >= 3)
                {
                    if (player.gameObject == finishedPlayers[2])
                    {
                        Debug.Log("Player 2 finished in third place");
                    }
                }
                if (finishedPlayers.Count >= 4)
                {
                    if (player.gameObject == finishedPlayers[3])
                    {
                        Debug.Log("Player 2 finished in fourth place");
                    }
                }
            }
            if (player.playerIndex == 2)
            {
                if (finishedPlayers.Count >= 1)
                {
                    if (player.gameObject == finishedPlayers[0])
                    {
                        if (!bPlayer3WonRace)
                        {
                            Debug.Log("Player 3 is the winner!!");
                            bPlayer3WonRace = true;
                            player3WonARace += 1;

                            PlayerPrefs.SetInt("Player3WonARace", player3WonARace);
                            PlayerPrefs.Save();
                        }
                    }
                }
                if (finishedPlayers.Count >= 2)
                {
                    if (player.gameObject == finishedPlayers[1])
                    {
                        Debug.Log("Player 3 finished in second place");
                    }
                }
                if (finishedPlayers.Count >= 3)
                {
                    if (player.gameObject == finishedPlayers[2])
                    {
                        Debug.Log("Player 3 finished in third place");
                    }
                }
                if (finishedPlayers.Count >= 4)
                {
                    if (player.gameObject == finishedPlayers[3])
                    {
                        Debug.Log("Player 3 finished in fourth place");
                    }
                }
            }
            if (player.playerIndex == 3)
            {
                if (finishedPlayers.Count >= 1)
                {
                    if (player.gameObject == finishedPlayers[0])
                    {
                        if (!bPlayer4WonRace)
                        {
                            Debug.Log("Player 4 is the winner!!");
                            bPlayer4WonRace = true;
                            player4WonARace += 1;

                            PlayerPrefs.SetInt("Player4WonARace", player4WonARace);
                            PlayerPrefs.Save();
                        }
                    }
                }
                if (finishedPlayers.Count >= 2)
                {
                    if (player.gameObject == finishedPlayers[1])
                    {
                        Debug.Log("Player 4 finished in second place");
                    }
                }
                if (finishedPlayers.Count >= 3)
                {
                    if (player.gameObject == finishedPlayers[2])
                    {
                        Debug.Log("Player 4 finished in third place");
                    }
                }
                if (finishedPlayers.Count >= 4)
                {
                    if (player.gameObject == finishedPlayers[3])
                    {
                        Debug.Log("Player 4 finished in fourth place");
                    }
                }
            }
        }
    }   
    private void StopFinishedPlayer()
    {
        foreach(GameObject player in finishedPlayers)
        {
            PlayerMovement playerRunValue = player.GetComponent<PlayerMovement>();

            playerRunValue.PlayerRunSpeed = 0;
        }
        if(finishedPlayers.Count == _playerInput.Length - 1 && !bIsRaceFinished)
        {  
            foreach(PlayerInput player in _playerInput)
            {
                PlayerMovement playerRunValue = player.GetComponent<PlayerMovement>();

                playerRunValue.PlayerRunSpeed = 0;
            }
            bIsRaceFinished = true;
            StartCoroutine("SwitchLevel", 5f);            
        }
    }
    private IEnumerator SwitchLevel(float time)
    {
        yield return new WaitForSeconds(time);

        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;      

        int nextSceneIndex;
        if(player1WonARace >= 3 || player2WonARace >= 3 || player3WonARace >= 3 || player4WonARace >= 3)
        {
            if(player1WonARace >= 3)
            {
                bPlayer1HasWonGrandPrix = true;
                int intValue = bPlayer1HasWonGrandPrix ? 1 : 0;
                PlayerPrefs.SetInt("Player1WonGrandPrix", intValue);
            }
            else if(player2WonARace >= 3)
            {
                bPlayer2HasWonGrandPrix = true;
                int intValue = bPlayer2HasWonGrandPrix ? 1 : 0;
                PlayerPrefs.SetInt("Player2WonGrandPrix", intValue);
            }
            else if(player3WonARace >= 3)
            {
                bPlayer3HasWonGrandPrix = true;
                int intValue = bPlayer3HasWonGrandPrix ? 1 : 0;
                PlayerPrefs.SetInt("Player3WonGrandPrix", intValue);
            }
            else if(player4WonARace >= 3)
            {
                bPlayer4HasWonGrandPrix = true;
                int intValue = bPlayer4HasWonGrandPrix ? 1 : 0;
                PlayerPrefs.SetInt("Player4WonGrandPrix", intValue);
            }
            PlayerPrefs.Save();
            SceneManager.LoadScene(1);
        }
        else 
        {
                       
            while (true)
            {
                nextSceneIndex = Random.Range(0, sceneCount);
                if (nextSceneIndex != currentSceneIndex && nextSceneIndex != 1 && nextSceneIndex != 0)
                {
                    break;
                }
            }
            sceneCounter += 1;

            // Save the updated sceneCounter value to PlayerPrefs (the value will persist throughout all the scenes)
            PlayerPrefs.SetInt("SceneCounter", sceneCounter);
            PlayerPrefs.Save();
            SceneManager.LoadScene(nextSceneIndex);

        }       
    }    
}

