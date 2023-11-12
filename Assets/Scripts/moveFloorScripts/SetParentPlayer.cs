using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParentPlayer : MonoBehaviour
{
    // プレイヤーが床の上に乗った時に呼ばれる    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerB"))
        {
            collision.transform.SetParent(transform);
            //player.gameObject.transform.parent = this.gameObject.transform;

        }


    }
    // プレイヤーが床の上に離れた時に呼ばれる 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerB"))
        {
            collision.transform.parent = null;
            //player.gameObject.transform.parent = null;

        }

    }
}
