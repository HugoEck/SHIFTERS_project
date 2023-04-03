using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 40f;
    [SerializeField] private CharacterController2D _controller;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;

    public CharacterController2D Controller { get { return _controller; } }
   
    private float _horizontalMove;    
    public float HorizontalMove { get { return _horizontalMove; } }

    private bool _jump = false;
     
    // Update is called once per frame
    protected virtual void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;

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
        
        

        if (KBCounter <= 0)
        {
            _controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
            _jump = false;
        }

        else
        {
            if (KnockFromRight == true)
            {
                Controller.M_RigidBody2D.velocity = new Vector2(-KBForce, KBForce);
            }
            if (KnockFromRight == false)
            {
                Controller.M_RigidBody2D.velocity = new Vector2(KBForce, KBForce);
            }
            KBCounter -= Time.deltaTime;

        }
    }
}
