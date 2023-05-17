using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowing_Block_Spawner : MonoBehaviour
{
    [SerializeField] private float _startingTime;
    [SerializeField] private GameObject _slowingObject;

    private List<GameObject> _slowingObjectsList = new List<GameObject>();

    private float _currentTime = 0f;

    public void Start()
    {
        _currentTime = _startingTime;
        InstantiateNewSlowingObject();
    }

    public void Update()
    {
        if (_slowingObjectsList.Count == 0)
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0)
            {
                InstantiateNewSlowingObject();
                _currentTime = _startingTime;
            }
        }
    }

    private void InstantiateNewSlowingObject()
    {
        GameObject newSlowingObject = Instantiate(_slowingObject, transform.position, Quaternion.identity);
        newSlowingObject.transform.SetParent(transform); // Set the spawner as the parent of the spawned object
        _slowingObjectsList.Add(newSlowingObject);
    }

    public void RemoveFromList(GameObject slowingObject)
    {
        _slowingObjectsList.Remove(slowingObject);
    }
}


