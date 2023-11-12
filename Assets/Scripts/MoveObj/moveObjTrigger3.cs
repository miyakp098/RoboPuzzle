using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObjTrigger3 : MonoBehaviour
{
    public bool isActive = false;//isScallingの時アクティブにする
    private bool backScale = false;//上にオブジェクトが載っているかどうか
    public bool BackScale
    {
        get { return backScale; }
        set { backScale = value; }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
            if ((collision.gameObject.CompareTag("moveFloor")) || (collision.gameObject.CompareTag("wall")) || (collision.gameObject.CompareTag("pushObj")))
            {
                backScale = true;
            }
        }
        
    }
}
