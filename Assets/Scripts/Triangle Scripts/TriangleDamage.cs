using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleDamage : MonoBehaviour
{
    public CircleMovement circleMovement;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Weak Point" && circleMovement.KBCounter <= 0)
        {
            circleMovement.KBCounter = circleMovement.KBTotalTime;
            if (other.transform.position.x <= transform.position.x)
            {
                circleMovement.KnockFromRight = true;
            }
            if (other.transform.position.x >= transform.position.x)
            {
                circleMovement.KnockFromRight = false;
            }
        }
    }
}
