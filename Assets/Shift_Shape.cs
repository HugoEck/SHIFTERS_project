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
    protected override void Start()
    {
        shapes[2].SetActive(false);
        shapes[1].SetActive(false);
        shapes[0].SetActive(false);
        shapes[3].SetActive(false);

        randomShapeNumber = random.Next(0, 3);

        shapes[randomShapeNumber].SetActive(true);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (shiftSquare)
        {

            Square();
        }
        if (shiftTriangle)
        {

            Triangle();
        }
        if (shiftCircle)
        {

            Circle();
        }
        if (shiftStar)
        {

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
        if (/*Input.GetKeyDown(key)*/shiftSquare || shiftCircle || shiftTriangle || shiftStar)
        {
            shapes[active].SetActive(true);
            shapes[notActive1].SetActive(false);
            shapes[notActive2].SetActive(false);
            shapes[notActive3].SetActive(false);
        }
    }


    //Methods for New input system inputACtions

    public void OnSquare(InputAction.CallbackContext context)
    {
        shiftSquare = context.action.triggered;
        Debug.Log(shiftSquare);
    }
    public void OnCircle(InputAction.CallbackContext context)
    {
        shiftCircle = context.action.triggered;
        Debug.Log(shiftCircle);
    }
    public void OnTriangle(InputAction.CallbackContext context)
    {
        shiftTriangle = context.action.triggered;
        Debug.Log(shiftTriangle);
    }
    public void OnStar(InputAction.CallbackContext context)
    {
        shiftStar = context.action.triggered;
        Debug.Log(shiftStar);
    }
}
