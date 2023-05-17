using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Slowing_Blocks : MonoBehaviour
{
    [SerializeField] private bool _bAlwaysSearchForPlayers;
    private PlayerInput[] _playerInput;
    private Slowing_Block_Spawner _spawner;

    private void Start()
    {
        _playerInput = GameObject.FindObjectsOfType<PlayerInput>();

        if (_bAlwaysSearchForPlayers)
        {
            StartCoroutine(FindPlayers());
        }

        // Get reference to the Slowing_Block_Spawner script
        _spawner = GetComponentInParent<Slowing_Block_Spawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (PlayerInput player in _playerInput)
        {
            if (collision.gameObject == player.gameObject)
            {
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.BShouldPlayerBeSlowed(true, 2);

                StartCoroutine(DisableAndDestroyBlock());
            }
        }
    }

    private IEnumerator FindPlayers()
    {
        while (_bAlwaysSearchForPlayers)
        {
            _playerInput = GameObject.FindObjectsOfType<PlayerInput>();
            yield return new WaitForSeconds(1f); // update the list every 1 second
        }
    }

    private IEnumerator DisableAndDestroyBlock()
    {
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;

        yield return new WaitForSeconds(1f);

        // Remove the object from the list in the Spawner script
        if (_spawner != null)
        {
            _spawner.RemoveFromList(gameObject);
        }

        Destroy(gameObject);
    }
}

