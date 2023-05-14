using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Spawn_Position : MonoBehaviour
{
    private PlayerInput[] _playerInputs;

    [SerializeField] private float _player1OffsetPosition;
    [SerializeField] private float _player2OffsetPosition;
    [SerializeField] private float _player3OffsetPosition;
    [SerializeField] private float _player4OffsetPosition;
           
    private void Start()
    {        
        _playerInputs = FindObjectsOfType<PlayerInput>();       
        foreach (PlayerInput playerInput in _playerInputs)
        {
            ChangePlayerSpawnPosition(playerInput);
        }
        StartCoroutine(DisablePlayerInputForTime(5f));
    }

    private void ChangePlayerSpawnPosition(PlayerInput playerInput)
    {

        if (playerInput.playerIndex == 0)
        {
            playerInput.gameObject.transform.position = this.gameObject.transform.position - new Vector3(_player1OffsetPosition,0,0);
        }
        else if (playerInput.playerIndex == 1)
        {
            playerInput.gameObject.transform.position = this.gameObject.transform.position - new Vector3(_player2OffsetPosition, 0, 0);
        }
        else if (playerInput.playerIndex == 2)
        {
            playerInput.gameObject.transform.position = this.gameObject.transform.position - new Vector3(_player3OffsetPosition, 0, 0);
        }
        else if (playerInput.playerIndex == 3)
        {
            playerInput.gameObject.transform.position = this.gameObject.transform.position - new Vector3(_player4OffsetPosition, 0, 0);
        }
        
    }
    private IEnumerator DisablePlayerInputForTime(float time)
    {
        foreach(PlayerInput input in _playerInputs)
        {
            // Disable the PlayerInput component
            input.enabled = false;

            // Wait for the specified time
            yield return new WaitForSeconds(time);

            // Enable the PlayerInput component again
            input.enabled = true;
        }        
    }
}

