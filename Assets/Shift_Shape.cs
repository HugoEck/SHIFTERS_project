using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.InputSystem.XR;

public class Shift_Shape : Shift_Shape_base
{
    // Start is called before the first frame update

    public bool shiftSquare;
    public bool shiftCircle;
    public bool shiftTriangle;
    public bool shiftStar;

    private bool isCountdownActive = false;
    private float countdownTimer = 0f;
    private float cooldownTimer = 0f;
    public const float countdownDuration = 5f;
    public const float cooldownDuration = 15f;
    private bool isCooldownActive = false;
    private bool isStarActive = false;
    private bool canShiftToStar = true;

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
        if (isCountdownActive)
        {
            countdownTimer += Time.deltaTime;
            if (countdownTimer >= countdownDuration)
            {
                countdownTimer = 0f;
                isCountdownActive = false;
                ShiftToCircle();
                isCooldownActive = true;
                cooldownTimer = cooldownDuration;
                StartCoroutine(DisableCooldownAfterDelay());
            }
        }
        if (isCooldownActive)
        {
            // Check if cooldown has ended
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                cooldownTimer = 0f;
                isCooldownActive = false;
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
            currentShapeState.currentShapeState = Shape_Enum.ShapeState.Square;
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
        if (isCountdownActive || (isCooldownActive && isStarActive) || (isCooldownActive && active == 3 && !canShiftToStar))
            return;

        shapes[active].SetActive(true);
        shapes[notActive1].SetActive(false);
        shapes[notActive2].SetActive(false);
        shapes[notActive3].SetActive(false);

        if (active == 3)
        {
            isStarActive = true;
            isCountdownActive = true;
            canShiftToStar = false;
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

        isStarActive = false;
        isCountdownActive = false;
        countdownTimer = 0f;
        cooldownTimer = cooldownDuration;

        // Reset other shift flags
        shiftSquare = false;
        shiftTriangle = false;
        shiftStar = false;

    }
    private System.Collections.IEnumerator DisableCooldownAfterDelay()
    {
        yield return new WaitForSeconds(cooldownDuration);

        isCooldownActive = false;

        yield break;
    }
}
