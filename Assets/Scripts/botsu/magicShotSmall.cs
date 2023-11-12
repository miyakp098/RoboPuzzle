using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicShotSmall : MonoBehaviour
{
    public float speed = 10f; // 弾の速度

    private bool isMoving = true; // 弾が移動中かどうかのフラグ

    private bool sizeLarge = false;//0.5倍にする


    void Update()
    {
        if (isMoving)
        {
            // 弾を前進させる
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    // 画面外に出たら自動的に削除する
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //衝突したオブジェクトの大きさを２倍にすr
        //collision.transform.localScale *= 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.CompareTag("moveObj"))
        {

            if (sizeLarge)//衝突したオブジェクトの大きさを２倍にする
            {
                
                // 衝突したオブジェクトの位置を、そのオブジェクトの大きさの0.5倍Y方向に移動
                collision.transform.position += new Vector3(0, collision.transform.localScale.y * 0.5f, 0);
                collision.transform.localScale *= 2;
            }
            else if (!sizeLarge)//衝突したオブジェクトの大きさを0.5倍にする
            {
                collision.transform.localScale *= 0.5f;
                // 衝突したオブジェクトの位置を、そのオブジェクトの大きさの0.5倍Y方向に移動
                //collision.transform.position += new Vector3(0, collision.transform.localScale.y * 0.5f, 0);
            }
            // 魔法オブジェクトを破壊する
            Destroy(gameObject);
        }
        
        
        
    }



    // 弾の移動を一時停止するメソッド
    public void StopMoving()
    {
        isMoving = false;
    }
}
