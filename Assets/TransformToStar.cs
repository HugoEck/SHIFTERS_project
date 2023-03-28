using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformToStar : MonoBehaviour
{
    public GameObject circle;
    public GameObject square;
    public GameObject triangle;
    public GameObject star;
    public void TransformStar()
    {
        circle.SetActive(false);
        square.SetActive(false);
        triangle.SetActive(false);
        star.SetActive(true);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TransformStar();
        }
    }
}
