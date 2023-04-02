using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float FollowSpeed = 2f;
    [SerializeField] Transform target;
    [SerializeField] float yOffset = 1f;


    void Update()
    {
        Vector3 newpos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newpos, FollowSpeed * Time.deltaTime);
    }
}
