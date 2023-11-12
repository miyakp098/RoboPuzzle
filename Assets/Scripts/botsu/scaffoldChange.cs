using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaffoldChange : MonoBehaviour
{
    [Header("足場摩擦係数0")] public PhysicsMaterial2D materialA;
    [Header("足場摩擦係数1")] public PhysicsMaterial2D materialB;
  

    private Collider2D col;
    void Start()
    {
        this.col = GetComponent<Collider2D>();
    }

    //private void Update()
    //{
    //    if(PlayerMove.playerOnGround == false)
    //    {
    //        col.sharedMaterial = materialA;
    //    }

    //}

    //着地後0.02秒後Friction1メソッドを呼び出し摩擦係数を0.8に設定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke(nameof(Friction1), 0.02f);        
    }

    // 空中にいるとき摩擦係数を0に設定
    private void OnCollisionExit2D(Collision2D collision)
    {
        
        // 摩擦係数を0に設定
        col.sharedMaterial = materialA;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 摩擦係数を0.8に設定
        col.sharedMaterial = materialB;
    }

    // 摩擦係数を0.8に設定
    private void Friction1()
    {
        // 摩擦係数を0.8に設定
        col.sharedMaterial = materialB;
    }
}
