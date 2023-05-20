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
    [SerializeField] private TextMeshProUGUI _startGameText;

    private PlayerInput[] _playerInputs;

    private bool isCoroutineStarted = false;
    
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("SHIFTERS_MENY");
        StartCoroutine(WaitForPlayer());        
        _startGameText.enabled = false;

        foreach (PlayerInput player in _playerInputs)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.PlayerRunSpeed = 200;
        }
    }
    
    public void Update()
    {       
        
        if (_playerInputs.Length > 1)
        {
            _startGameText.enabled = true;
            _startGameText.text = "PRESS 'X' (KEYBOARD) OR 'START' (GAMEPAD) TO START THE GAME: " + _playerInputs.Length + "/4 players";
            if (PlayerMovement.bIsGameStarted && !isCoroutineStarted)
            {
                StartCoroutine(DisablePlayerInputForTime(2));
                isCoroutineStarted = true;
                    
            }
        }
        
    }    
    private IEnumerator DisablePlayerInputForTime(float time)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex;

        foreach (PlayerInput player in _playerInputs)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.PlayerRunSpeed = 0;
        }
        yield return new WaitForSeconds(time);


        while (true)
        {
            nextSceneIndex = Random.Range(0, sceneCount);
            if (nextSceneIndex != currentSceneIndex && nextSceneIndex != 1 && nextSceneIndex != 0)
            {
                break;
            }
        }
        SceneManager.LoadScene(nextSceneIndex);
        
    }
    private IEnumerator WaitForPlayer()
    {
        while (true)
        {
            _playerInputs = FindObjectsOfType<PlayerInput>();
            yield return new WaitForSeconds(0.1f); // update the list every 1 second
        }
    }

}
