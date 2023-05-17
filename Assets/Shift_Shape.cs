using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.InputSystem.XR;

public class Shift_Shape : Shift_Shape_base
{
    private float _countdownTimer = 0f;
    private float _cooldownTimer = 0f;
    public const float countdownDuration = 5f;
    public const float cooldownDuration = 15f;

    private bool _isCooldownActive = false;
    private bool _isStarActive = false;
    private bool _canShiftToStar = true;
    private bool _isCountdownActive = false;
    private bool _changedScene = false;

    public bool shiftSquare { get; private set; }
    public bool shiftCircle { get; private set; }
    public bool shiftTriangle { get; private set; }
    public bool shiftStar { get; private set; }
    
    protected override void Start()
    {
        currentShapeState = new Shape_Enum();
        
        shapes[2].SetActive(false);
        shapes[1].SetActive(false);
        shapes[0].SetActive(false);
        shapes[3].SetActive(false);

        randomShapeNumber = random.Next(0, 3);

        shapes[randomShapeNumber].SetActive(true);

        if (shapes[0].activeSelf)
        {
            currentShapeState.currentShapeState = Shape_Enum.ShapeState.Circle;
        }
        else if (shapes[1].activeSelf)
        { 
            currentShapeState.currentShapeState = Shape_Enum.ShapeState.Square; 
        }
        else if (shapes[2].activeSelf)
        {
            currentShapeState.currentShapeState = Shape_Enum.ShapeState.Triangle;
        }
        else if (shapes[3].activeSelf)
        {
            currentShapeState.currentShapeState = Shape_Enum.ShapeState.Star;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(_changedScene)
        {
            _isCountdownActive = false;
            _isCooldownActive = false;
            _isStarActive = false;
            _canShiftToStar = true;

            _changedScene = false;

            shapes[2].SetActive(false);
            shapes[1].SetActive(false);
            shapes[0].SetActive(false);
            shapes[3].SetActive(false);

            randomShapeNumber = random.Next(0, 3);

            shapes[randomShapeNumber].SetActive(true);

            if (shapes[0].activeSelf)
            {
                currentShapeState.currentShapeState = Shape_Enum.ShapeState.Circle;
            }
            else if (shapes[1].activeSelf)
            {
                currentShapeState.currentShapeState = Shape_Enum.ShapeState.Square;
            }
            else if (shapes[2].activeSelf)
            {
                currentShapeState.currentShapeState = Shape_Enum.ShapeState.Triangle;
            }

        }

        if (_isCountdownActive)
        {
            _countdownTimer += Time.deltaTime;
            if (_countdownTimer >= countdownDuration)
            {
                _countdownTimer = 0f;
                _isCountdownActive = false;
                ShiftToCircle();
                _isCooldownActive = true;
                _cooldownTimer = cooldownDuration;
                StartCoroutine(DisableCooldownAfterDelay());
            }
        }
        if (_isCooldownActive)
        {
            // Check if cooldown has ended
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0f)
            {
                _cooldownTimer = 0f;
                _isCooldownActive = false;
            }
        }

        if (shiftSquare)
        {
            currentShapeState.currentShapeState = Shape_Enum.ShapeState.Square;
            Square();
        }
        if (shiftTriangle)
        {
            currentShapeState.currentShapeState = Shape_Enum.ShapeState.Triangle;
            Triangle();
        }
        if (shiftCircle)
        {
            currentShapeState.currentShapeState = Shape_Enum.ShapeState.Circle;
            Circle();
        }
        if (shiftStar)
        {
            currentShapeState.currentShapeState = Shape_Enum.ShapeState.Star;
            Star();
        }
    }

    protected override void Square()
    {
        ActivateShape(1, 0, 2, 3);

    }
    protected override void Circle()
    {
        ActivateShape(0, 1, 2, 3);
    }
    protected override void Star()
    {
        ActivateShape(3, 2, 1, 0);
    }
    protected override void Triangle()
    {
        ActivateShape(2, 1, 0, 3);
    }
    private void ActivateShape(int active, int notActive1, int notActive2, int notActive3)
    {
        if (_isCountdownActive || (_isCooldownActive && _isStarActive) || (_isCooldownActive && active == 3 && !_canShiftToStar))
            return;

        shapes[active].SetActive(true);
        shapes[notActive1].SetActive(false);
        shapes[notActive2].SetActive(false);
        shapes[notActive3].SetActive(false);

        if (active == 3)
        {
            _isStarActive = true;
            _isCountdownActive = true;
            _canShiftToStar = false;
        }
    }


    //Methods for New input system inputACtions

    public void OnSquare(InputAction.CallbackContext context)
    {
        shiftSquare = context.action.triggered;
        //Debug.Log(shiftSquare);
    }
    public void OnCircle(InputAction.CallbackContext context)
    {
        shiftCircle = context.action.triggered;
        //Debug.Log(shiftCircle);
    }
    public void OnTriangle(InputAction.CallbackContext context)
    {
        shiftTriangle = context.action.triggered;
        //Debug.Log(shiftTriangle);
    }
    public void OnStar(InputAction.CallbackContext context)
    {
        shiftStar = context.action.triggered;

        //Debug.Log(shiftStar);
    }
    private void ShiftToCircle()
    {
        ActivateShape(0, 1, 2, 3);
        currentShapeState.currentShapeState = Shape_Enum.ShapeState.Circle;
        _isStarActive = false;
        _isCountdownActive = false;
        _countdownTimer = 0f;
        _cooldownTimer = cooldownDuration;

        // Reset other shift flags
        shiftSquare = false;
        shiftTriangle = false;
        shiftStar = false;
        

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
    private System.Collections.IEnumerator DisableCooldownAfterDelay()
    {
        yield return new WaitForSeconds(cooldownDuration);

        _isCooldownActive = false;

        yield break;
    }
    
}
