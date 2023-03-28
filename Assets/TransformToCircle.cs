using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformToCircle : MonoBehaviour
{
    public GameObject circle;
    public GameObject square;
    public GameObject triangle;
    public GameObject star;

    public void TransformCircle()
    {
        circle.SetActive(true);
        square.SetActive(false);
        triangle.SetActive(false) ;
        star.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TransformCircle();
        }
    }
}
