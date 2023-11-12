using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFloorTrigger : MonoBehaviour
{
    public moveFloor moveFloor;

    //下にオブジェクトがあり、移動方向を反転するための判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("moveObj"))
        {
            this.moveFloor.onEnterObj = true;
        }        
    }
    
}
