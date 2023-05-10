using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : CharacterController2D
{
    [SerializeField] protected float _runSpeed;
    [SerializeField] private float _knockbackForce;    
    [SerializeField] private float _knockbackTotalTime;
    
    private Wall_Jumping _wallJumping;

    protected float _horizontalMove;
    private float _knockbackCounter;
    private float _currentPlayerDazedTime = 0f;
    private float _startingPlayerDazedTime = 2f;

    public Vector2 inputMovement = Vector2.zero;
    private bool jumped;
    private bool moveLeft;
    private bool moveRight;
    private bool spaceJump;
    public bool SpaceJump { get { return spaceJump; } }

    public bool bIsGameStarted { get; set; } = false;  // Only used for starting the game from lobby scene

    private Wall_Jumping _wallJumpingReference;
    //CAMERA
    private CinemachineTargetGroup targetGroup;
    private List<Wall_Jumping> _wallJumpingList = new List<Wall_Jumping>();


    private bool knockbackFromRight;
    public float KnockbackForce { get { return _knockbackForce; } set { _knockbackForce = value; } }  
    public float KnockbackCounter { get { return _knockbackCounter; } set { _knockbackCounter = value; } }  
    public float KnockbackTotalTime { get { return _knockbackTotalTime; } }    
    public bool KnockbackFromRight { get { return knockbackFromRight; } set { knockbackFromRight = value; } }

    //CAMERA
    void Start()
    {
        // Find the Target Group in the scene
        targetGroup = GameObject.FindObjectOfType<CinemachineTargetGroup>();

        // Add this player to the Target Group
        targetGroup.AddMember(this.transform, 1f, 10f);
    }
    //CAMERA
    void OnDestroy()
    {
        // Remove this player from the Target Group when it's destroyed
        if (targetGroup != null)
        {
            targetGroup.RemoveMember(this.transform);
        }
    }


    protected override void Awake()
    {        
        base.Awake();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        _wallJumpingReference = GetComponentInChildren<Wall_Jumping>();

        _horizontalMove = /*Input.GetAxisRaw("Horizontal")*/inputMovement.x * _runSpeed;
        Vector2 move = new Vector2(inputMovement.x, inputMovement.y) * _runSpeed;
        //Debug.Log(jumped);
        if (/*Input.GetButtonDown("Jump")*/  jumped || spaceJump)
        {
            
            
            //Debug.Log(_jump);
        }        
    }
    public void Jump(InputAction.CallbackContext value)
    {
        //Debug.Log(value.phase);
        
    }
    protected override void FixedUpdate()
    {
        //Move our character
        
        base.FixedUpdate();
        
        if (_knockbackCounter <= 0)
        {
            _currentPlayerDazedTime -= 1 * Time.deltaTime;

            if(_currentPlayerDazedTime > 0) // If player has collided with another player they are slowed for 2 seconds by maxSpeed / 3
            {
                _maxSpeed /= 3;
            }

            if (!spaceJump)
            {
                Move(_horizontalMove * Time.fixedDeltaTime, false, jumped);
            }
            else
            {
                Move(_horizontalMove * Time.fixedDeltaTime, false, spaceJump);
            }

        }        
        else
        {
            _currentPlayerDazedTime = _startingPlayerDazedTime;

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

    //Metoder till nya input systemets inputActions
    public void OnMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        moveLeft = context.action.triggered;
        if (moveLeft)
        {
            inputMovement.x = -1;
        }
        else if (moveRight)
        {
            inputMovement.x = 1;
        }
        else
        {
            inputMovement.x = 0;
        }
    }
    public void OnRight(InputAction.CallbackContext context)
    {
        moveRight = context.action.triggered;
        if (moveRight)
        {
            inputMovement.x = 1;
        }
        else if (moveLeft)
        {
            inputMovement.x = -1;
        }
        else
        {
            inputMovement.x = 0;
        }
    }

    public virtual void OnJump(InputAction.CallbackContext context)
    {
        
        jumped = context.action.triggered;

        _wallJumpingReference.HasJumped = context.action.triggered;
    }
    
    public void OnSpaceJump(InputAction.CallbackContext context)
    {
        spaceJump = context.action.triggered;

        _wallJumpingReference.HasJumped = context.action.triggered;
    }

    public void StartGame(InputAction.CallbackContext context)
    {
        bIsGameStarted = context.action.triggered;
    }

}
