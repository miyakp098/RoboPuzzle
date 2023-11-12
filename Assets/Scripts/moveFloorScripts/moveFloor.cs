using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFloor : MonoBehaviour
{
    [Header("床の移動速度")] public float speed = 2f;    // 床の移動速度
    [Header("床のX方向の移動幅")] public float widthX = 3f;
    [Header("床のY方向の移動幅")] public float widthY = 0f;

    private Vector3 startPos;   // 初期位置
    private Vector3 endPos;     // 目標位置
    
    private bool movingToEnd = true; // 目標位置へ移動中かどうか
    //[Header("Playerオブジェクトを入れる")] public GameObject player;// Playerオブジェクトを入れる
    [HideInInspector]
    public bool onEnterObj = false;


    private static Vector3 moveSpeed;
    public static Vector3 FloorMoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    private void Start()
    {
        
        
        startPos = transform.position;
        endPos = transform.position + Vector3.right * widthX + Vector3.up * widthY; // 移動する範囲を指定（右に3ユニット移動）
    }

    

    private void Update()
    {
        // 移動方向を決定
        Vector3 direction = movingToEnd ? (endPos - startPos).normalized : (startPos - endPos).normalized;

        // 下降中にObjに接触したら移動方向を反転
        if (widthY != 0 && onEnterObj)
        {
            movingToEnd = !movingToEnd;
            onEnterObj = false;
        }

        // 床を移動
        moveSpeed = direction * speed * Time.deltaTime;
        transform.Translate(moveSpeed);

        // 目標位置に到達したら移動方向を反転
        if (Vector3.Distance(transform.position, movingToEnd ? endPos : startPos) < 0.1f)
        {
            movingToEnd = !movingToEnd;
        }
    }

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

    private void OnTriggerStay2D(Collider2D collision)
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
