using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Player_Spawn_Manager : MonoBehaviour
{
    private List<int> _activePlayers;

    [SerializeField] private TextMeshProUGUI _startGameText;

    private PlayerInput _playerOneAccess;

    private List<GameObject> _playerObjects = new List<GameObject>();

    private IEnumerator WaitForPlayer()
    {
        while (true)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
            _playerObjects = objectsWithTag.ToList();
            yield return new WaitForSeconds(1f); // update the list every 1 second
        }
    }

    private void Start()
    {
        StartCoroutine(WaitForPlayer());
        _activePlayers = new List<int>();
        _startGameText.enabled = false;
    }
    
    public void Update()
    {
        foreach(GameObject players in _playerObjects)
        {
            PlayerMovement playerMovement = players.GetComponent<PlayerMovement>();

            if (_activePlayers.Count > 0)
            {
                _startGameText.enabled = true;
                _startGameText.text = "PRESS 'X' OR 'START' TO START THE GAME: " + _activePlayers.Count + "/4 players";
                if (playerMovement.bIsGameStarted)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }            
        }        
    }
    void OnPlayerJoined(PlayerInput playerInput)
    {
        _playerOneAccess = (PlayerInput)playerInput;
        Debug.Log("Player Input ID: " + playerInput.playerIndex);
        _activePlayers.Add(playerInput.playerIndex);
        
    }    
}
