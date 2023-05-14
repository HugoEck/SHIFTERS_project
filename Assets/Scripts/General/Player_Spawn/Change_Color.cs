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

    private Color color = Color.white;
    private Color color2 = Color.white;
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
            color = Color.green;
            color2 = Color.black;
        }
        // Player 2
        else if (playerInput.playerIndex == 1)
        {
            color = Color.red;
            color2 = Color.Lerp(Color.red, Color.yellow, 50);
        }
        // Player 3
        else if (playerInput.playerIndex == 2)
        {
            color = Color.blue;
            color2 = Color.Lerp(Color.blue, Color.red, 50);
        }
        // Player 4
        else if (playerInput.playerIndex == 3)
        {
            color = Color.yellow;
            color2 = Color.Lerp(Color.black, Color.red, 50);
        }        
        gameObject.GetComponent<Renderer>().material.color = color;
        gameObject.GetComponent<TrailRenderer>().startColor = color;
        gameObject.GetComponent<TrailRenderer>().endColor = color2;
    }
}
