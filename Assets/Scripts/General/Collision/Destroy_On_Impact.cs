using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Destroy_On_Impact : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private bool _bShouldDestroy;

    private List<Shift_Shape> _playerObjects = new List<Shift_Shape>();
    private List<GameObject> _playerList = new List<GameObject>();
    private Shift_Shape[] _accessShapeEnums;   

    private PlayerMovement _thisPlayer;
    private GameObject _otherPlayer;

    private const float _startingDazedTimer = 3; // Used for players to ignore collision when the two collide
    private const float _startingDestroyObjectTimer = 1f; // Used for falling dummy (timer)
    private float _currentDestroyObjectTimer = 0f; // Used for falling dummy (timer)    
    private float _currentDazedTimer = 0f; // Used for players to ignore collision when the two collide
   
    
    private bool _bBeingDestroyed = false;
    private bool _bHasAlreadyCollided = false;
    private bool _bIsDazed = false;

    public bool BShouldDestroy { get { return _bShouldDestroy; } }

    private void Start()
    {
        _thisPlayer = gameObject.GetComponent<PlayerMovement>();        
        StartCoroutine(FindPlayers());
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
            Shift_Shape shiftShape = collision.gameObject.GetComponent<Shift_Shape>();

            if (collision.gameObject.CompareTag("Ground"))
            {
                if (_bShouldDestroy && !_bHasAlreadyCollided)
                {
                    _bBeingDestroyed = true;
                    _currentDestroyObjectTimer = _startingDestroyObjectTimer;
                    return;
                }
            }

            if (shiftShape != null)
            {
                Shape_Enum.ShapeState shapeState = shiftShape.currentShapeState.currentShapeState;

                if (shapeState == Shape_Enum.ShapeState.Triangle || shapeState == Shape_Enum.ShapeState.Star)
                {
                    GameObject otherPlayer = collision.gameObject;

                    if (_bShouldDestroy && !_bHasAlreadyCollided)
                    {
                        _bBeingDestroyed = true;
                        _currentDestroyObjectTimer = _startingDestroyObjectTimer;
                    }

                    _thisPlayer.KnockbackCounter = _thisPlayer.KnockbackTotalTime;
                    FindObjectOfType<AudioManager>().Play("HIT");

                    if (collision.transform.position.x <= gameObject.transform.position.x)
                    {
                        _thisPlayer.KnockbackFromRight = true;
                    }
                    if (collision.transform.position.x > gameObject.transform.position.x)
                    {
                        _thisPlayer.KnockbackFromRight = false;
                    }

                    // Ignore collision between _thisPlayer and _otherPlayer
                    Physics2D.IgnoreCollision(_thisPlayer.GetComponent<CircleCollider2D>(), otherPlayer.GetComponent<CircleCollider2D>(), true);

                    // Set a timer to turn off the collision ignoring after a certain amount of time
                    StartCoroutine(ResetCollisionIgnore(otherPlayer));
                }
            }
        }
    }


    private IEnumerator ResetCollisionIgnore(GameObject otherPlayer)
    {
        yield return new WaitForSeconds(6f); // Change this value to the desired amount of time
        Physics2D.IgnoreCollision(_thisPlayer.GetComponent<Collider2D>(), otherPlayer.GetComponent<Collider2D>(), false);
    }
    private IEnumerator FindPlayers()
    {
        while (true)
        {
            _playerObjects.Clear();
            _playerList.Clear();

            // Find all game objects with the tag "Player"
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
            _playerList = objectsWithTag.ToList();

            // Filter the game objects based on whether they have the "Shift_Shape" script attached
            foreach (GameObject obj in objectsWithTag)
            {
                if (obj == gameObject) continue; // skip current game object

                PlayerMovement playerMovementValues = obj.GetComponent<PlayerMovement>();
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

}
