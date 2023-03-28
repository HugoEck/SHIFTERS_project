using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformToTriangle : MonoBehaviour
{
    public GameObject circle;
    public GameObject square;
    public GameObject triangle;
    public GameObject star;
    public void TransformTriangle()
    {
        circle.SetActive(false);
        square.SetActive(false);
        triangle.SetActive(true);
        star.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TransformTriangle();
        }
    }
}
