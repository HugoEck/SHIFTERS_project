using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shape_Movement_Values : PlayerMovement
{
    [SerializeField] private float _newMaxSpeed;
    [SerializeField] private float _newAccelerationForce;
    [SerializeField] private float _newJumpForce;
    [SerializeField] private float _newDeaccelerationForce;
    
    protected override void FixedUpdate()
    {               
        base.ChangeMovementValues(_newMaxSpeed, _newAccelerationForce, _newJumpForce, _newDeaccelerationForce);
        
    }

    protected override void Update()
    {
        base.Update();
    }
    
}
