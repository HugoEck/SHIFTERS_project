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

    
    public PlayerMovement playerMovement;
    

    protected override void Update()
    {
        WallSlide();
        WallJump();
       
    }
    private void WallSlide()
    {
        if(Controller.IsWalled() && !Controller.M_Grounded)
        {
            _isWallSliding = true;

            Controller.M_RigidBody2D.velocity = new Vector2(Controller.M_RigidBody2D.velocity.x,
                Mathf.Clamp(Controller.M_RigidBody2D.velocity.y, -_wallSlidingSpeed, float.MaxValue));
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
        
        if(Input.GetButtonDown(playerMovement.inputWalljump) && _wallJumpingCounter > 0 )
        {
            _isWallJumping = true;
            Controller.M_RigidBody2D.velocity = new Vector2(_wallJumpingDirection * _wallJumpingPower.x, _wallJumpingPower.y);
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
    public void FixedUpdate()
    {
    }
}
