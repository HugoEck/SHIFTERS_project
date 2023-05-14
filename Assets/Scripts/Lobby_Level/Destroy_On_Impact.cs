using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Destroy_On_Impact : MonoBehaviour
{
    private List<Shift_Shape> _playerObjects = new List<Shift_Shape>();
    private List<GameObject> _playerList = new List<GameObject>();
    private Shift_Shape[] _accessShapeEnums;   

    private PlayerMovement _thisPlayer;
    private GameObject _otherPlayer;

    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private bool _bShouldDestroy;
    public bool BShouldDestroy { get { return _bShouldDestroy; } }

    private bool _bBeingDestroyed = false;
    private bool _bHasAlreadyCollided = false;
    private bool _bIsDazed = false;

    private float _currentDestroyObjectTimer = 0f; // Used for falling dummy (timer)
    private const float _startingDestroyObjectTimer = 1f; // Used for falling dummy (timer)
    private float _currentDazedTimer = 0f; // Used for players to ignore collision when the two collide
    private const float _startingDazedTimer = 3; // Used for players to ignore collision when the two collide

    private void Start()
    {
        _thisPlayer = gameObject.GetComponent<PlayerMovement>();        
        StartCoroutine(FindPlayers());
    }
    private IEnumerator FindPlayers()
    {
        while (true)
        {
            _playerObjects.Clear();

            // Find all game objects with the tag "Player"
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
            _playerList = objectsWithTag.ToList();
            // Filter the game objects based on whether they have the "Shift_Shape" script attached
            foreach (GameObject obj in objectsWithTag)
            {
                 if (obj == gameObject) continue; // skip current game object
                PlayerMovement playerMovementValues= obj.GetComponent<PlayerMovement>();
                Shift_Shape shiftShape = obj.GetComponent<Shift_Shape>();
                if (shiftShape != null)
                {
                    _playerObjects.Add(shiftShape);
                }
            }

            // Assign _accessShapeEnums to found player
            if (_playerObjects.Count > 0)
            {
                _accessShapeEnums = new Shift_Shape[_playerObjects.Count];
                _playerObjects.CopyTo(_accessShapeEnums);
            }

            yield return new WaitForSeconds(1f); // update the list every 1 second
        }
    }
    private void Update() 
    {
        
        if(_bBeingDestroyed)   // Only used for falling lobby dummy
        {
             _currentDestroyObjectTimer -= 1 * Time.deltaTime;

             if(_currentDestroyObjectTimer <= 0)
             {
                   Destroy(gameObject);
             }
        }      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        // Check if the collision layer is included in the allowed collision layer mask
        if (((1 << collision.gameObject.layer) & _collisionLayer) != 0)
        {
            
            foreach (Shift_Shape shiftShape in _accessShapeEnums)
            {
                
                foreach (GameObject playerObject in _playerList)
                {
                    if (collision.gameObject.CompareTag("Ground"))
                    {
                        if (_bShouldDestroy && !_bHasAlreadyCollided)
                        {
                            _bBeingDestroyed = true;
                            _currentDestroyObjectTimer = _startingDestroyObjectTimer;
                            return;
                        }
                    }

                    if (shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Triangle)
                    {
                        if (shiftShape.gameObject == collision.gameObject)
                        {
                            _otherPlayer = collision.gameObject;
                            if (_bShouldDestroy && !_bHasAlreadyCollided)
                            {
                                _bBeingDestroyed = true;
                                _currentDestroyObjectTimer = _startingDestroyObjectTimer;
                            }

                            _thisPlayer.KnockbackCounter = _thisPlayer.KnockbackTotalTime;

                            if (collision.transform.position.x <= gameObject.transform.position.x)
                            {
                                _thisPlayer.KnockbackFromRight = true;                               
                            }
                            if (collision.transform.position.x > gameObject.transform.position.x)
                            {
                                _thisPlayer.KnockbackFromRight = false;                                
                            }                            
                            // Ignore collision between _thisPlayer and _otherPlayer
                            Physics2D.IgnoreCollision(_thisPlayer.GetComponent<CircleCollider2D>(), _otherPlayer.GetComponent<CircleCollider2D>(), true);

                             // Set a timer to turn off the collision ignoring after a certain amount of time
                            StartCoroutine(ResetCollisionIgnore());                         
                            return;                            
                        }
                    }
                    // If the other shape is a star that you collide with, you get the slowdown and knockback effect
                    if (shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Star)
                    {
                        if (shiftShape.gameObject == collision.gameObject)
                        {
                            _otherPlayer = collision.gameObject;
                            if (_bShouldDestroy && !_bHasAlreadyCollided)
                            {
                                _bBeingDestroyed = true;
                                _currentDestroyObjectTimer = _startingDestroyObjectTimer;
                            }

                            _thisPlayer.KnockbackCounter = _thisPlayer.KnockbackTotalTime;

                            if (collision.transform.position.x <= gameObject.transform.position.x)
                            {
                                _thisPlayer.KnockbackFromRight = true;
                                Debug.Log("Knock from right");
                            }
                            if (collision.transform.position.x > gameObject.transform.position.x)
                            {
                                _thisPlayer.KnockbackFromRight = false;
                                Debug.Log("Knock from left");
                            }
                            // Ignore collision between _thisPlayer and _otherPlayer
                            Physics2D.IgnoreCollision(_thisPlayer.GetComponent<CircleCollider2D>(), _otherPlayer.GetComponent<CircleCollider2D>(), true);

                            // Set a timer to turn off the collision ignoring after a certain amount of time
                            StartCoroutine(ResetCollisionIgnore());
                            return;                           
                        }
                        
                    }                     
                }                
            }
        }
    }   
    private IEnumerator ResetCollisionIgnore()
    {
        yield return new WaitForSeconds(2f); // Change this value to the desired amount of time
        Physics2D.IgnoreCollision(_thisPlayer.GetComponent<Collider2D>(), _otherPlayer.GetComponent<Collider2D>(), false);
    }
}
