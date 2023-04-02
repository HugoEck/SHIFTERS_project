using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Shift_Shape : Shift_Shape_base
{
    // Start is called before the first frame update
    protected override void Start()
    {
        shapes[2].SetActive(false);
        shapes[1].SetActive(false);
        shapes[0].SetActive(false);
        shapes[3].SetActive(false);

        randomShapeNumber = random.Next(0, 3);

        shapes[randomShapeNumber].SetActive(true);
    }

    // Update is called once per frame
    protected override void Update()
    {
        Square();
        Triangle();
        Circle();
        Star();
    }
    
    protected override void Square()
    {
        ActivateShape(KeyCode.F, 1,0,2,3);                        
    }
    protected override void Circle()
    {
        ActivateShape(KeyCode.G, 0,1,2,3);                      
    }
    protected override void Star()
    {
        ActivateShape(KeyCode.T, 3, 2,1,0);                         
    }
    protected override void Triangle()
    {
        ActivateShape(KeyCode.R, 2, 1,0,3);             
    }
    private void ActivateShape(KeyCode key, int active, int notActive1, int notActive2, int notActive3)
    {
        if(Input.GetKeyDown(key))
        {            
            shapes[active].SetActive(true);
            shapes[notActive1].SetActive(false);
            shapes[notActive2].SetActive(false);
            shapes[notActive3].SetActive(false);
        }       
    }
}
