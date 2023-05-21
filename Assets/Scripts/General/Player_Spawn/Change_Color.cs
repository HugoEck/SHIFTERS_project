using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using Color = UnityEngine.Color;

public class Change_Color : MonoBehaviour
{
    [SerializeField] private PlayerInput _inputSystemAccess;
    [SerializeField] private TrailRenderer _trailRendererAccess;

    private Color _color = Color.white;
    private Color _color2 = Color.white;
    private void Start()
    {
        OnPlayerJoined(_inputSystemAccess);
        //StartCoroutine(AssignColorsToChildObjects(color));
    }
    void OnPlayerJoined(PlayerInput playerInput)
    {        
        Debug.Log("Player Input ID: " + playerInput.playerIndex);
       
        // Change the color of the player prefab's child objects based on the player index
        ChangePlayerColor(playerInput);

    }
    private void ChangePlayerColor(PlayerInput playerInput)
    {
        // Player 1
        if (playerInput.playerIndex == 0)
        {
            _color = Color.green;
            _color2 = Color.black;
        }
        // Player 2
        else if (playerInput.playerIndex == 1)
        {
            _color = Color.red;
            _color2 = Color.Lerp(Color.red, Color.yellow, 50);
        }
        // Player 3
        else if (playerInput.playerIndex == 2)
        {
            _color = Color.blue;
            _color2 = Color.Lerp(Color.blue, Color.red, 50);
        }
        // Player 4
        else if (playerInput.playerIndex == 3)
        {
            _color = Color.yellow;
            _color2 = Color.Lerp(Color.black, Color.red, 50);
        }        
        gameObject.GetComponent<Renderer>().material.color = _color;
        gameObject.GetComponent<TrailRenderer>().startColor = _color;
        gameObject.GetComponent<TrailRenderer>().endColor = _color2;
    }
}
