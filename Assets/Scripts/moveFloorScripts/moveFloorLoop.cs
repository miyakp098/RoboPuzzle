using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFloorLoop : MonoBehaviour
{
    [Header("移動経路")] public GameObject[] positions; // 移動先となる位置
    [Header("速さ")] public float speed = 2.0f; // 床の移動速度

    //[Header("Playerオブジェクトを入れる")] public GameObject player;// Playerオブジェクトを入れる

    private int currentTargetIndex = 0;
    private Vector3 targetPosition;

    private void Start()
    {
        if (positions.Length > 0)
        {
            // 初期の目的地を設定します
            targetPosition = positions[currentTargetIndex].transform.position;
        }
    }

    private void Update()
    {
        // 床を目的地へ移動させます
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 床が目的地に到着したかどうかをチェックします
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // 次の位置へ移動します
            currentTargetIndex = (currentTargetIndex + 1) % positions.Length;
            targetPosition = positions[currentTargetIndex].transform.position;
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
