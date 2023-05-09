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

    private Shift_Shape _accessThisShapeEnum;

    private PlayerMovement _thisPlayer;
    private GameObject _otherPlayer;

    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private bool _bShouldDestroy;
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
        _accessThisShapeEnum = gameObject.GetComponent<Shift_Shape>();
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
        if(_otherPlayer != null)
        {
            IgnoreCollision();
        }
        
        if(_bBeingDestroyed)   // Only used for falling lobby dummy
        {
             _currentDestroyObjectTimer -= 1 * Time.deltaTime;

             if(_currentDestroyObjectTimer <= 0)
             {
                   Destroy(gameObject);
             }
        }

        if(_bIsDazed)
        {
           _currentDazedTimer -= 1 * Time.deltaTime;
            
           if (_currentDazedTimer <= 0)
           {
                _bIsDazed = false;
                _bHasAlreadyCollided = false;               
           }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        // Check if the collision layer is included in the allowed collision layer mask
        if (((1 << collision.gameObject.layer) & _collisionLayer) != 0)
        {

            Vector2 raycastOrigin = transform.position + new Vector3(0f, transform.localScale.y, 0f);

            // Cast a raycast directly downwards, with a length of 20.0 units (NOT USED FOR STAR SHAPE)
            RaycastHit2D normalHit = Physics2D.Raycast(raycastOrigin, Vector2.up, 20.0f);

            // Check if the raycast hits object from below
            if(_bShouldDestroy && !_bHasAlreadyCollided)
            {
                if (normalHit.collider != null && normalHit.collider.gameObject == collision.gameObject)
                {
                    _bHasAlreadyCollided = true;
                    _bBeingDestroyed = true;
                    _currentDestroyObjectTimer = _startingDestroyObjectTimer;
                }
            }
            
            foreach (Shift_Shape shiftShape in _accessShapeEnums)
            {
                
                foreach (GameObject playerObject in _playerList)
                {

                    PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
                    if (playerMovement == null || playerObject == this.gameObject)
                    {
                        continue;
                    }

                    // If the other shape is a star that you collide with, you get the slowdown and knockback effect
                    if (shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Star)
                    {
                        if (shiftShape.gameObject == collision.gameObject)
                        {
                            GameObject otherPlayer = collision.gameObject;
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
                            StartDazedTimer();
                            _otherPlayer = otherPlayer;
                            break;
                        }
                    }
                    
                    // Only collide if the collision happens from underneath
                    if (normalHit.collider != null && normalHit.collider.gameObject == collision.gameObject)
                    {                        
                        if (shiftShape.gameObject == collision.gameObject)
                        {   
                            // If this.gameobject isn't star or triangle. this.gameobject recieves a knockback and slow
                            if (_accessThisShapeEnum.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Triangle 
                                || _accessThisShapeEnum.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Star)
                            {
                                GameObject otherPlayer = collision.gameObject;
                                PlayerMovement otherPlayerMovement = shiftShape.gameObject.GetComponent<PlayerMovement>();
                                if (_bShouldDestroy && !_bHasAlreadyCollided)
                                {
                                    _bBeingDestroyed = true;
                                    _currentDestroyObjectTimer = _startingDestroyObjectTimer;
                                }

                                otherPlayerMovement.KnockbackCounter = otherPlayerMovement.KnockbackTotalTime;

                                if (collision.transform.position.x <= gameObject.transform.position.x)
                                {
                                    otherPlayerMovement.KnockbackFromRight = true;
                                }
                                if (collision.transform.position.x > gameObject.transform.position.x)
                                {
                                    otherPlayerMovement.KnockbackFromRight = false;
                                }
                                StartDazedTimer();
                                _otherPlayer = otherPlayer;
                                break;
                            }
                            else // If this.Gameobject is star or triangle the other player recieves a knockback
                            {
                                GameObject otherPlayer = collision.gameObject;
                                _thisPlayer.KnockbackCounter = _thisPlayer.KnockbackTotalTime;
                                if (collision.transform.position.x <= playerObject.transform.position.x)
                                {
                                    _thisPlayer.KnockbackFromRight = true;
                                    Debug.Log("Hit From Right");
                                }
                                if (collision.transform.position.x > playerObject.transform.position.x)
                                {
                                    _thisPlayer.KnockbackFromRight = false;
                                    Debug.Log("Hit From Left");
                                }
                                StartDazedTimer();
                                _otherPlayer = otherPlayer;
                                break;
                            }                            
                        }                        
                    }                    
                }                
            }
        }
    }
    private void IgnoreCollision()
    {
        if(_bIsDazed)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), _otherPlayer.GetComponent<CircleCollider2D>());
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), _otherPlayer.GetComponent<BoxCollider2D>());
        }
        else
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), _otherPlayer.GetComponent<CircleCollider2D>(), false);
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), _otherPlayer.GetComponent<BoxCollider2D>(), false);
        }
    }
    private void StartDazedTimer()
    {
        if (!_bHasAlreadyCollided)
        {
            _currentDazedTimer = _startingDazedTimer;
            _bIsDazed = true;
            _bHasAlreadyCollided = true;
        }
    }
}
