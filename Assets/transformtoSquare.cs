using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformtoSquare : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject circle;
    public GameObject square;
    public GameObject triangle;
    public GameObject star;
    public void TransformToSquare()
    {
        circle.SetActive(false);
        square.SetActive(true);
        triangle.SetActive(false);
        star.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            TransformToSquare();
        }
    }
}
