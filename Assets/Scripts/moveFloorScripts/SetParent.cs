using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour
{
    // プレイヤーが床の上に乗った時に呼ばれる    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerB"))
        {
            collision.transform.SetParent(transform);
            //player.gameObject.transform.parent = this.gameObject.transform;

        }
        if (collision.gameObject.CompareTag("moveObj"))
        {
            // プレイヤーを床の子オブジェクトにすることで、床と一緒に移動させる
            collision.transform.SetParent(transform);
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
        if (collision.gameObject.CompareTag("moveObj"))
        {
            // プレイヤーの親オブジェクトをリセットして、床との親子関係を解除
            collision.transform.parent = null;
        }
    }
}
