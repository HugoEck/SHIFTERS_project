using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public abstract class CharacterController2D : MonoBehaviour
{

    [Range(0, .3f)][SerializeField] protected float m_MovementSmoothing = .05f;   // How much to smooth out the movement
    [SerializeField] protected bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] protected LayerMask _whatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] protected Transform _wallCheck;                              // A position marking where to check for walls
    [SerializeField] protected LayerMask _wallLayer;                              // A mask determining what is walls to the character
    [SerializeField] protected Transform _groundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] protected Transform _ceilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] protected Rigidbody2D _rigidBody2D;

    protected bool _jump = false;
    protected float _jumpForce = 400f;                                           // Amount of force added when the player jumps.                
    protected float _accelerationForce;   
    protected float _maxSpeed;
    protected float _deaccelerationForce;    
    private const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded    
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up

    protected bool m_Grounded;            // Whether or not the player is grounded.
    protected bool _facingRight = true;  // For determining which way the player is currently facing.    

    private Vector3 _velocity = Vector3.zero;

    public float JumpForce { get { return _jumpForce; } set { _jumpForce = value; } }
    public float AccelerationForce { get { return _accelerationForce; } set { _accelerationForce = value; } }
    public float M_MaxSpeed { get { return _maxSpeed; } set { _maxSpeed = value; } }
    public float DeaccelerationForce { get { return _deaccelerationForce; } set { _deaccelerationForce = value; } }
    protected virtual void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }


    protected virtual void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, k_GroundedRadius, _whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(_ceilingCheck.position, k_CeilingRadius, _whatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            //Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            //m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

            // Move the character by adding clamped max speed and force
            _rigidBody2D.AddForce(Vector2.right * move * _accelerationForce);
            Vector2 clampedVelocity= _rigidBody2D.velocity;
            clampedVelocity.x = Mathf.Clamp(_rigidBody2D.velocity.x, -_maxSpeed, _maxSpeed);
            _rigidBody2D.velocity = clampedVelocity;
            if (move == 0)
            {
                //Deaccelerate player when not pressing any keys
                Vector2 deaccelerate = _rigidBody2D.velocity;
                deaccelerate.x -= _deaccelerationForce * _rigidBody2D.velocity.x * Time.deltaTime;
                _rigidBody2D.velocity = deaccelerate;
            }

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !_facingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && _facingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            //_jump = true;
            // Add a vertical force to the player.
            m_Grounded = false;
            _rigidBody2D.AddForce(new Vector2(0f, _jumpForce));
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public bool IsWalled()
    {
        return Physics2D.OverlapCircle(_wallCheck.position, 0.2f, _wallLayer);
    }
}

