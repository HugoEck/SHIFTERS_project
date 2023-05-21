using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spike_Collision : MonoBehaviour
{
    [SerializeField] private bool _bAlwaysSearchForPlayers;

    private PlayerInput[] _playerInput;

    private void Start()
    {
        _playerInput = GameObject.FindObjectsOfType<PlayerInput>();

        // AssignPlayerCollisionIndex();

        if (_bAlwaysSearchForPlayers)
        {
            StartCoroutine(FindPlayers());
        }
    }   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (PlayerInput player in _playerInput)
        {
            if (collision.gameObject == player.gameObject)
            {
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.KnockbackCounter = playerMovement.KnockbackTotalTime;

                if (collision.transform.position.x <= gameObject.transform.position.x)
                {
                    playerMovement.KnockbackFromRight = true;
                }
                else
                {
                    playerMovement.KnockbackFromRight = false;
                }

                StartCoroutine(ResetCollisionForPlayerIgnore(player));
                return;
            }
        }
    }
    private IEnumerator ResetCollisionForPlayerIgnore(PlayerInput player)
    {
        Collider2D spikeCollider = gameObject.GetComponent<Collider2D>();
        Collider2D playerCollider = player.gameObject.GetComponent<CircleCollider2D>();

        Physics2D.IgnoreCollision(spikeCollider, playerCollider, true);
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreCollision(spikeCollider, playerCollider, false);
    }

    private IEnumerator FindPlayers()
    {
        while (_bAlwaysSearchForPlayers)
        {
            _playerInput = GameObject.FindObjectsOfType<PlayerInput>();

            //AssignPlayerCollisionIndex();
            yield return new WaitForSeconds(1f); // update the list every 1 second
        }
    }
}

