using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Shift_Shape_base : MonoBehaviour
{
    [SerializeField] protected GameObject[] shapes = new GameObject[4];

    public Shape_Enum currentShapeState { get; protected set; }

    protected System.Random random = new System.Random();  
    
    protected int randomShapeNumber;

    

    protected virtual void Start()
    {
        
    }
    protected abstract void Square();
    protected abstract void Circle();
    protected abstract void Triangle();
    protected abstract void Star();

    // Update is called once per frame
    protected abstract void Update();
   
}
