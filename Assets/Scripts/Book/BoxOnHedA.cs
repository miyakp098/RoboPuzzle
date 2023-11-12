using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOnHedA : MonoBehaviour
{

    private static bool helpPlayer = false;
    public static bool HelpPlayerA
    {
        get { return helpPlayer; }
        set { helpPlayer = value; }
    }
    private static Vector2 onBovPos;
    public static Vector2 OnBovPos
    {
        get { return onBovPos; }
        set { onBovPos = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            onBovPos = collision.transform.position;
            float yVelocity = rb.velocity.y;
            //Debug.Log("Y方向の速さ: " + yVelocity);
            if (yVelocity <= -1)
            {
                helpPlayer = true;
            }
        }
        else
        {
            //Debug.Log("このオブジェクトには Rigidbody2D が付加されていません。");
        }
    }
}
