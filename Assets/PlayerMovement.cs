using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : CharacterController2D
{    
    [SerializeField] private float _knockbackForce;    
    [SerializeField] private float _knockbackTotalTime;
    // Wall jumping
    private Wall_Jumping _wallJumping;
    // Collision
    private Destroy_On_Impact _collision;
    // Camera
    private CinemachineTargetGroup targetGroup;
    // Checkpoints in level
    private Transform currentCheckpoint;
    // Check which shape is currently active
    private Shift_Shape _accessThisShape;

    protected float _runSpeed = 200;
    protected float _horizontalMove;
    private float _knockbackCounter;
    private float _currentPlayerDazedTime = 0f;
    private float _startingPlayerDazedTime = 2f;

    private Vector2 inputMovement = Vector2.zero;

    private bool _changedScene = false;   
    private bool _jumped;
    private bool _moveLeft;
    private bool _moveRight;
    private bool _GamepadLeft;
    private bool _GamepadRight;
    private bool _spaceJump;
    private bool _knockbackFromRight;

    public bool SpaceJump { get { return _spaceJump; } }
    public float PlayerRunSpeed { get { return _runSpeed; } set { _runSpeed = value; } } // only used for setting the _runSpeed in the lobby    
    public float KnockbackForce { get { return _knockbackForce; } set { _knockbackForce = value; } }  
    public float KnockbackCounter { get { return _knockbackCounter; } set { _knockbackCounter = value; } }  
    public float KnockbackTotalTime { get { return _knockbackTotalTime; } }    
    public bool KnockbackFromRight { get { return _knockbackFromRight; } set { _knockbackFromRight = value; } }


    public static bool bIsGameStarted = false;  // Only used for starting the game from lobby scene
    void Start()
    {
        _accessThisShape = gameObject.GetComponent<Shift_Shape>();
        // Find the Target Group in the scene
        targetGroup = GameObject.FindObjectOfType<CinemachineTargetGroup>();
        _collision = gameObject.GetComponent<Destroy_On_Impact>();

        // Add this player to the Target Group
        if (!_collision.BShouldDestroy)
        {
            targetGroup.AddMember(this.transform, 1f, 10f);
        }
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
        // If the current scene isn't the lobby scene and if scenemanager recently detected a change in scenes, then disable movement for x seconds
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Lobby_Scene") && _changedScene)
        {          
            _changedScene = false;
            
            StartCoroutine(DisablePlayerInputForTime(5f));            
        }

        _horizontalMove = inputMovement.x * _runSpeed;          
        //inputMovement = Gamepad.current != null ? Gamepad.current.leftStick.ReadValue() : Vector2.zero;
    }    
    protected override void FixedUpdate()
    {
        //Move our character
        _wallJumping = GetComponentInChildren<Wall_Jumping>();
        base.FixedUpdate();
        
        if (_knockbackCounter <= 0)
        {
            _currentPlayerDazedTime -= 1 * Time.deltaTime;

            if(_currentPlayerDazedTime > 0) // If player has collided with another player they are slowed for 2 seconds by maxSpeed / 3
            {
                _maxSpeed /= 3;
            }

            if (!_spaceJump)
            {
                Move(_horizontalMove * Time.fixedDeltaTime, false, _jumped);
            }
            else
            {
                Move(_horizontalMove * Time.fixedDeltaTime, false, _spaceJump);
            }

        }        
        else
        {
            _currentPlayerDazedTime = _startingPlayerDazedTime;

            if (_knockbackFromRight == true)
            {
                _rigidBody2D.velocity = new Vector2(-_knockbackForce, _knockbackForce);
            }
            if (_knockbackFromRight == false)
            {
                _rigidBody2D.velocity = new Vector2(_knockbackForce, _knockbackForce);
            }
            _knockbackCounter -= Time.deltaTime;
            
        }
    }
    public bool BShouldPlayerBeSlowed(bool bShouldSlow, float forDuration)
    {
        _currentPlayerDazedTime = forDuration;
        if(bShouldSlow && _currentPlayerDazedTime > 0)
        {
            _currentPlayerDazedTime -= 1 * Time.deltaTime;
                       
            _maxSpeed /= 3;
            
        }
        else
        {
            _currentPlayerDazedTime = forDuration;
        }
        return bShouldSlow;
    }
    /// <summary>
    /// Change movement values from Shape_Movement_Values script
    /// </summary>
    /// <param name="newMaxSpeed"></param>
    /// <param name="newAccelerationForce"></param>
    /// <param name="newJumpForce"></param>
    /// <param name="newDeaccelerationForce"></param>
    protected void ChangeMovementValues(float newMaxSpeed, float newAccelerationForce, float newJumpForce, float newDeaccelerationForce)
    {
        this.transform.parent.GetComponent<PlayerMovement>().M_MaxSpeed = newMaxSpeed;
        this.transform.parent.GetComponent<PlayerMovement>().AccelerationForce = newAccelerationForce;
        this.transform.parent.GetComponent<PlayerMovement>().JumpForce = newJumpForce;
        this.transform.parent.GetComponent<PlayerMovement>().DeaccelerationForce = newDeaccelerationForce;
    }

    //Metoder till nya input systemets inputActions
    //public void OnMove(InputAction.CallbackContext context)
    //{
    //    inputMovement = context.ReadValue<Vector2>();
    //}

    public void OnLeftStickLeft(InputAction.CallbackContext context)
    {
        _GamepadLeft = context.ReadValueAsButton();
        Debug.Log("Left");
        inputMovement.x = -1;
    }

    public void OnLeftStickRight(InputAction.CallbackContext context)
    {
        _GamepadRight = context.ReadValueAsButton();
        Debug.Log("Right");
        inputMovement.x = 1;
    }

    public void OnLeftStickLeftRelease(InputAction.CallbackContext context)
    {
        if (_GamepadLeft == false)
        {
            inputMovement.x = 0;
        }
    }
    
    public void OnLeftStickRightRelease(InputAction.CallbackContext context)
    {
        if (_GamepadRight == false)
        {
            inputMovement.x = 0;
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        _moveLeft = context.action.triggered;
        if (_moveLeft)
        {
            inputMovement.x = -1;
        }
        else if (_moveRight)
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
        _moveRight = context.action.triggered;
        if (_moveRight)
        {
            inputMovement.x = 1;
        }
        else if (_moveLeft)
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
        if(_accessThisShape.currentShapeState.currentShapeState != Shape_Enum.ShapeState.Circle)
        {
            _jumped = context.action.triggered;
            _wallJumping.HasJumped = context.action.triggered;
        }
        
    }
    
    public void OnSpaceJump(InputAction.CallbackContext context)
    {
        if (_accessThisShape.currentShapeState.currentShapeState != Shape_Enum.ShapeState.Circle)
        {
            _spaceJump = context.action.triggered;
            _wallJumping.HasJumped = context.action.triggered;
        }        
    }

    public void StartGame(InputAction.CallbackContext context)
    {        
        bIsGameStarted = context.action.triggered;
    }    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Set the boolean variable to true when a scene is loaded
        _changedScene = true;
    }
    /// <summary>
    /// Called when a new scene has begun (set the runspeed to 0 for time seconds)
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator DisablePlayerInputForTime(float time)
    {
        _runSpeed = 0;

        // Wait for the specified time
        yield return new WaitForSeconds(time);

        _runSpeed = 200;
    }
}
