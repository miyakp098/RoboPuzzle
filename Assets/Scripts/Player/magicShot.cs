using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicShot : MonoBehaviour
{
    public float speed = 10f; // 弾の速度

    private bool isMoving = true; // 弾が移動中かどうかのフラグ


    void Update()
    {
        if (isMoving)
        {
            // 弾を前進させる
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    // 画面外に出たら自動的に削除する
    //void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("moveObj") || collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("moveFloor") || collision.gameObject.CompareTag("pushObj"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("moveObj")|| collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("moveFloor") || collision.gameObject.CompareTag("pushObj"))
        {
            Destroy(gameObject);
        }
    }

    



    // 弾の移動を一時停止するメソッド
    public void StopMoving()
    {
        isMoving = false;
    }
}
