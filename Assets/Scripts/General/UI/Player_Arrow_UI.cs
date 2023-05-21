using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Arrow_UI : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI _arrowTextPlayer1, _arrowTextPlayer2, _arrowTextPlayer3, _arrowTextPlayer4;
    [SerializeField] private Vector3 _arrowTextOffset;
    
    private PlayerInput[] _players;
    
    private void Start()
    {
        // Disable the arrow text gameobjects
        _arrowTextPlayer1.enabled = false;
        _arrowTextPlayer2.enabled = false;
        _arrowTextPlayer3.enabled = false;
        _arrowTextPlayer4.enabled = false;


        StartCoroutine(FindPlayers());            
    }
    private void Update()
    {
        foreach(var player in _players)
        {
            if(_players.Length >= 1)
            {
                if(player.playerIndex == 0)
                {
                    Color color = Color.green;                   

                    _arrowTextPlayer1.text = "P1";
                    _arrowTextPlayer1.transform.position = player.gameObject.transform.position + _arrowTextOffset;
                    _arrowTextPlayer1.color = color;

                    _arrowTextPlayer1.enabled = true;                   
                    
                }
            }
            if(_players.Length >= 2)
            {
                if(player.playerIndex == 1)
                {
                    Color color = Color.red;
                   
                    _arrowTextPlayer2.text = "P2";
                    _arrowTextPlayer2.transform.position = player.gameObject.transform.position + _arrowTextOffset;
                    _arrowTextPlayer2.color = color;

                    _arrowTextPlayer2.enabled = true;
                   
                }
            }
            if(_players.Length >= 3)
            {
                if(player.playerIndex == 2)
                {
                    Color color = Color.blue;
                    
                    _arrowTextPlayer3.text = "P3";
                    _arrowTextPlayer3.transform.position = player.gameObject.transform.position + _arrowTextOffset;
                    _arrowTextPlayer3.color = color;

                    _arrowTextPlayer3.enabled = true;                   
                }

            }
            if(_players.Length >= 4)
            {
                if(player.playerIndex == 3)
                {
                    Color color = Color.yellow;                   

                    _arrowTextPlayer4.text = "P4";
                    _arrowTextPlayer4.transform.position = player.gameObject.transform.position + _arrowTextOffset;
                    _arrowTextPlayer4.color = color;

                    _arrowTextPlayer4.enabled = true;                   
                }
            }
        }
    }
    private IEnumerator FindPlayers()
    {
        while(true)
        {
            _players = FindObjectsOfType<PlayerInput>();

            yield return new WaitForSeconds(0.5f);
        }        
    }
}
