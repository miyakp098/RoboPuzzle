using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour
{

    public Transform objectB; // 接続先のオブジェクト
    public float ropeWidth = 0.15f; // ロープの幅

    Vector3 posA;

    private SpriteRenderer spriteRenderer; // スプライトレンダラー

    void Start()
    {
        posA = this.transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 direction = objectB.position - posA; // Vector3型
        float distance = direction.magnitude;

        // ロープの方向と位置を設定
        transform.right = direction;
        transform.position = posA + direction / 2; // Vector3型で合致

        // スケールを距離に合わせて変更
        transform.localScale = new Vector3(distance, ropeWidth, -1);
    }
}
