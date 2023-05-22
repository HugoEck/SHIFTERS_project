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

    private bool _bIsCooldownActive = false;
    private bool _bIsStarActive = false;
    private bool _bCanShiftToStar = true;
    private bool _bIsCountdownActive = false;
    private bool _changedScene = false;

    public float CountdownTimer { get { return _countdownTimer; } }
    public float CooldownTimer { get { return _cooldownTimer; } }
    
    public bool BIsCooldownActive { get { return _bIsCooldownActive; } }
    public bool BIsStarActive { get { return _bIsStarActive; } }
    public bool BIsCountdownActive { get { return _bIsCountdownActive; } }
    public bool BShiftSquare { get; private set; }
    public bool BShiftCircle { get; private set; }
    public bool BShiftTriangle { get; private set; }
    public bool BShiftStar { get; private set; }
    public bool BChangedScene { get { return _changedScene; } }
    
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
            _bIsCountdownActive = false;
            _bIsCooldownActive = false;
            _bIsStarActive = false;
            _bCanShiftToStar = true;

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

        if (_bIsCountdownActive)
        {
            
            _countdownTimer -= Time.deltaTime;
            if (_countdownTimer <= 0)
            {
                _countdownTimer = 0f;
                _bIsCountdownActive = false;
                ShiftToCircle();
                _bIsCooldownActive = true;
                _cooldownTimer = cooldownDuration;
                StartCoroutine(DisableCooldownAfterDelay());
            }
        }
        if (_bIsCooldownActive)
        {
            // Check if cooldown has ended
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0f)
            {
                _cooldownTimer = 0f;
                _bIsCooldownActive = false;
            }
        }

        if (BShiftSquare)
        {
            
            Square();
        }
        if (BShiftTriangle)
        {
            
            Triangle();
        }
        if (BShiftCircle)
        {
            
            Circle();
        }
        if (BShiftStar)
        {
            
            Star();
        }
    }

    protected override void Square()
    {
        if (_bIsStarActive)
            return;
        ActivateShape(1, 0, 2, 3);
        currentShapeState.currentShapeState = Shape_Enum.ShapeState.Square;

    }
    protected override void Circle()
    {
        if (_bIsStarActive)
            return;
        ActivateShape(0, 1, 2, 3);
        currentShapeState.currentShapeState = Shape_Enum.ShapeState.Circle;
    }
    protected override void Star()
    {
        ActivateShape(3, 2, 1, 0);
        _countdownTimer = countdownDuration;
        currentShapeState.currentShapeState = Shape_Enum.ShapeState.Star;
    }
    protected override void Triangle()
    {
        if (_bIsStarActive)
            return;
        ActivateShape(2, 1, 0, 3);
        currentShapeState.currentShapeState = Shape_Enum.ShapeState.Triangle;
    }
    private void ActivateShape(int active, int notActive1, int notActive2, int notActive3)
    {
        if (_bIsCountdownActive || (_bIsCooldownActive && _bIsStarActive) || (_bIsCooldownActive && active == 3 && !_bCanShiftToStar))
            return;

        shapes[active].SetActive(true);
        shapes[notActive1].SetActive(false);
        shapes[notActive2].SetActive(false);
        shapes[notActive3].SetActive(false);

        if (active == 3)
        {
            _bIsStarActive = true;
            _bIsCountdownActive = true;
            _bCanShiftToStar = false;
        }
    }

    //Methods for New input system inputACtions

    public void OnSquare(InputAction.CallbackContext context)
    {
        BShiftSquare = context.action.triggered;
        //Debug.Log(shiftSquare);
    }
    public void OnCircle(InputAction.CallbackContext context)
    {
        BShiftCircle = context.action.triggered;
        //Debug.Log(shiftCircle);
    }
    public void OnTriangle(InputAction.CallbackContext context)
    {
        BShiftTriangle = context.action.triggered;
        //Debug.Log(shiftTriangle);
    }
    public void OnStar(InputAction.CallbackContext context)
    {
        BShiftStar = context.action.triggered;

        //Debug.Log(shiftStar);
    }
    private void ShiftToCircle()
    {
        ActivateShape(0, 1, 2, 3);
        currentShapeState.currentShapeState = Shape_Enum.ShapeState.Circle;
        _bIsStarActive = false;
        _bIsCountdownActive = false;
        _countdownTimer = 0f;
        _cooldownTimer = cooldownDuration;

        // Reset other shift flags
        BShiftSquare = false;
        BShiftTriangle = false;
        BShiftStar = false;
        

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

        _bIsCooldownActive = false;

        yield break;
    }
    
}
