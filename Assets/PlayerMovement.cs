using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : CharacterController2D
{
    [SerializeField] protected float _runSpeed;
    [SerializeField] private float _knockbackForce;    
    [SerializeField] private float _knockbackTotalTime;


    protected float _horizontalMove;
    private float _knockbackCounter;

    protected bool _jump = false;
    private bool knockbackFromRight;
    public float KnockbackForce { get { return _knockbackForce; } set { _knockbackForce = value; } }  
    public float KnockbackCounter { get { return _knockbackCounter; } set { _knockbackCounter = value; } }  
    public float KnockbackTotalTime { get { return _knockbackTotalTime; } }    
    public bool KnockbackFromRight { get { return knockbackFromRight; } set { knockbackFromRight = value; } }


    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
       
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
            Debug.Log(_jump);
        }        
    }
    private void Jump(InputAction.CallbackContext value)
    {
        Debug.Log(value.phase);
    }
    protected override void FixedUpdate()
    {
        //Move our character
        
        base.FixedUpdate();        
        if (_knockbackCounter <= 0)
        {
            Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
            _jump = false;
        }        
        else
        {
            if (knockbackFromRight == true)
            {
                _rigidBody2D.velocity = new Vector2(-_knockbackForce, _knockbackForce);
            }
            if (knockbackFromRight == false)
            {
                _rigidBody2D.velocity = new Vector2(_knockbackForce, _knockbackForce);
            }
            _knockbackCounter -= Time.deltaTime;
            
        }
    }
    protected void ChangeMovementValues(float newMaxSpeed, float newAccelerationForce, float newJumpForce, float newDeaccelerationForce)
    {
        this.transform.parent.GetComponent<PlayerMovement>().M_MaxSpeed = newMaxSpeed;
        this.transform.parent.GetComponent<PlayerMovement>().AccelerationForce = newAccelerationForce;
        this.transform.parent.GetComponent<PlayerMovement>().JumpForce = newJumpForce;
        this.transform.parent.GetComponent<PlayerMovement>().DeaccelerationForce = newDeaccelerationForce;
    }  
}
