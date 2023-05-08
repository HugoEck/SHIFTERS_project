using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputManager : MonoBehaviour
{
    public static MenuInputManager instance;
    
    public bool MenuOpenCloseInput { get; set; }

    private PlayerInput playerInput;

    private InputAction menuOpenCloseAction;

    private void Awake()
    {
        instance = this;

        playerInput = GetComponent<PlayerInput>();
        menuOpenCloseAction = playerInput.actions["MenuOpenClose"];
    }

    private void Update()
    {
        MenuOpenCloseInput = menuOpenCloseAction.WasPerformedThisFrame();
    }

}
