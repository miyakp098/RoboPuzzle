using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOnHed : MonoBehaviour
{

    
    private static bool diePlayer = false;
    public static bool DiePlayerB
    {
        get { return diePlayer; }
        set { diePlayer = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float yVelocity = rb.velocity.y;
            //Debug.Log("Y方向の速さ: " + yVelocity);
            if (yVelocity <= -1)
            {
                diePlayer = true;
            }
        }
        else
        {
            //Debug.Log("このオブジェクトには Rigidbody2D が付加されていません。");
        }
    }
    
}
