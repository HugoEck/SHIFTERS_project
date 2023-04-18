using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Jumping : PlayerMovement
{
    
    [SerializeField]private float _wallSlidingSpeed = 1f;
    
    private bool _isWallJumping;
    private bool _isWallSliding;

    private float _wallJumpingDirection;
    private float _wallJumpingTime = 0.2f;
    private float _wallJumpingCounter;
    private float _wallJumpingDuration = 0.4f;
    

    private Vector2 _wallJumpingPower = new Vector2(8f, 16f);

    protected override void Awake()
    {
       

    }
    protected override void Update()
    {
        Debug.Log(_isWallSliding);
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
        if(_isWallSliding)
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
        
        if(/*Input.GetButtonDown("Jump")*/ jumped && _wallJumpingCounter > 0 || spaceJump && _wallJumpingCounter > 0)
        {
            _isWallJumping = true;
            _rigidBody2D.velocity = new Vector2(_wallJumpingDirection * _wallJumpingPower.x, _wallJumpingPower.y);
            _wallJumpingCounter = 0f;

            if(transform.localScale.x != _wallJumpingDirection)
            {

            }
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
