using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seesaw : MonoBehaviour
{
    [Header("回転の減衰係数")] public float rotationDamping = 5f; // 回転の減衰係数
    [Header("最大角速度")] public float maxAngularVelocity = 25f; // 最大角速度

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // オブジェクトの回転を取得
        float zRotation = rb.rotation;

        // 角速度を制限する
        float clampedAngularVelocity = Mathf.Clamp(rb.angularVelocity, -maxAngularVelocity, maxAngularVelocity);
        rb.angularVelocity = clampedAngularVelocity;

        // Z軸の回転を0に近づけるように力を加える
        rb.AddTorque(-zRotation * rotationDamping);
    }
}
