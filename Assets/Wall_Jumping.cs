using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wall_Jumping : PlayerMovement
{
    [SerializeField] PlayerInput input;
    [SerializeField]private float _wallSlidingSpeed = 1f;
    
    private float _wallJumpingDirection;
    private float _wallJumpingTime = 0.2f;
    private float _wallJumpingCounter;
    private float _wallJumpingDuration = 0.4f;

    private Vector2 _wallJumpingPower = new Vector2(8f, 16f);

    private bool _isWallJumping;
    private bool _isWallSliding;
    public bool HasJumped { get; set; }
    
    protected override void Awake()
    {
        
    }
    protected override void Update()
    {
        //Debug.Log(_isWallSliding);
        base.Update();
        
        WallSlide();
        WallJump();
       
    }
    private void WallSlide()
    {
        if(IsWalled() && !m_Grounded)
        {
            _isWallSliding = true;

            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x,
                Mathf.Clamp(_rigidBody2D.velocity.y, -_wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            _isWallSliding = false;
        }
    }

    private void WallJump()
    {
        // Determine wall direction
        int wallDirection = 0;
        if (IsWalled() && !m_Grounded)
        {
            wallDirection = (int)Mathf.Sign(_wallCheck.transform.position.x - transform.position.x);
        }

        // Perform wall jump
        if (_isWallSliding)
        {
            _isWallJumping = false;
            _wallJumpingDirection = -transform.localScale.x;
            _wallJumpingCounter = _wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            _wallJumpingCounter -= Time.deltaTime;
        }

        if (HasJumped && _wallJumpingCounter > 0)
        {
            _isWallJumping = true;
            float wallJumpingPowerX = _wallJumpingPower.x * wallDirection * -1f; // Reverse X power if wallDirection is negative
            _rigidBody2D.velocity = new Vector2(wallJumpingPowerX, _wallJumpingPower.y);
            _wallJumpingCounter = 0f;

            Invoke(nameof(StopWallJumping), _wallJumpingDuration);
        }
    }
    private void StopWallJumping()
    {
        _isWallJumping = false;
    }
    protected override void FixedUpdate()
    {
        
    }
   

}
