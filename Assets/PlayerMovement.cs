using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 40f;
    [SerializeField] private CharacterController2D _controller;
    public CharacterController2D Controller { get { return _controller; } }
   
    [SerializeField] private string inputNameHorizontal;
   
    private float _horizontalMove;    
    public float HorizontalMove { get { return _horizontalMove; } }

    private bool _jump = false;
     
    // Update is called once per frame
    protected virtual void Update()
    {
        _horizontalMove = Input.GetAxisRaw(inputNameHorizontal) * _runSpeed;

        if (Mathf.Approximately(_horizontalMove, 0f))
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
        }        
    }
    private void Jump(InputAction.CallbackContext value)
    {
        Debug.Log(value.phase);
    }
    private void FixedUpdate()
    {
        //Move our character
        _controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
        _jump = false;
    }
}
