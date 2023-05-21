using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dummy_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnShape;

    private System.Random random = new System.Random();

    private float _currentTime = 0f;    
    private float _startingTime;
    
    public void Start()
    {
        _startingTime = random.Next(1,10);
        _currentTime = _startingTime;
    }

    public void Update()
    {        
        _currentTime -= 1 * Time.deltaTime;

        if(ResetTimer())
        {
            _currentTime = _startingTime;
        }
    }
    public bool ResetTimer()
    {        
        if(_currentTime <= 0)
        {
            Instantiate(_spawnShape, transform.position, new Quaternion(0,0,0,0));
            _startingTime = random.Next(1, 10);
            return true;
        }
        else
        {
            return false;
        }
    }   
    
}
