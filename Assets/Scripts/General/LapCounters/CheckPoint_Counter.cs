using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CheckPoint_Counter : MonoBehaviour
{

    private PlayerInput[] _playerInput;
    // Player 1
    public static int checkpointsPassedPlayer1 = 0;
    // Player 2
    public static int checkpointsPassedPlayer2 = 0;
    // Player 3 
    public static int checkpointsPassedPlayer3 = 0;
    // Player 4
    public static int checkpointsPassedPlayer4 = 0;
    
    void Start()
    {
        _playerInput = GameObject.FindObjectsOfType<PlayerInput>();

        checkpointsPassedPlayer1 = 0;
        checkpointsPassedPlayer2 = 0;
        checkpointsPassedPlayer3 = 0;
        checkpointsPassedPlayer4 = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (PlayerInput player in _playerInput)
        {
            if (collision.gameObject == player.gameObject)
            {
                if (player.playerIndex == 0)
                {
                    checkpointsPassedPlayer1 += 1;
                    Debug.Log("Player 1 passed checkpoint");
                }
                if (player.playerIndex == 1)
                {
                    checkpointsPassedPlayer2 += 1;
                    Debug.Log("Player 2 passed checkpoint");
                }
                if (player.playerIndex == 2)
                {
                    checkpointsPassedPlayer3 += 1;
                    Debug.Log("Player 3 passed checkpoint");
                }
                if (player.playerIndex == 3)
                {
                    checkpointsPassedPlayer4 += 1;
                    Debug.Log("Player 4 passed checkpoint");
                }
            }
        }        
    }
    
}
