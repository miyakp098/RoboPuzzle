using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBC : MonoBehaviour
{
    public float speed = 0;//歩くスピード
    public float jumpForce = 0;//ジャンプ力
   
    public LayerMask groundLayer;//インスペクターからレイヤーマスクを選べるようにする
    public LayerMask objLayer;

    private Rigidbody2D rb2d;


    private Animator anim;//アニメーション
    private bool isUsingSpell = false; // Spellアニメーションを使っているかどうかを追跡
    private SpriteRenderer spRenderer;

    private bool isGround = false;//地面の判定

    private bool onObject = false;//物の上にいる判定


    private bool pushOrPull = false;

    public static bool isTouch = false;//Objを動かすときの接触判定
    


    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.spRenderer = GetComponent<SpriteRenderer>();

        
    }


    

    //Player周りの接触判定
    private void FixedUpdate()
    {
        //移動処理
        
        rb2d.velocity = new Vector2(speed , rb2d.velocity.y);
        //足場判定       

        Vector2 groundPos = new Vector2(transform.position.x, transform.position.y);

        Vector2 groundArea = new Vector2(0.25f, 0.15f);//足場判定エリア

        Debug.DrawLine(groundPos + groundArea, groundPos - groundArea, Color.red);

        //地面の上
        isGround = Physics2D.OverlapArea(groundPos + groundArea, groundPos - groundArea, groundLayer);
        //オブジェクトの上
        onObject = Physics2D.OverlapArea(groundPos + groundArea, groundPos - groundArea, objLayer);

    }

    public void TogglePlayerBActiveState()
    {
        if (this.gameObject.activeSelf)
        {
            anim.SetTrigger("notActive");
        }
        else
        {
            this.gameObject.SetActive(true);//bool値
        }

        //anim.SetTrigger("notActive");
    }

    //魔法終わりアニメーション呼び出し用
    public void PlayerActiveState()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);//bool値

        //anim.SetTrigger("notActive");
    }

}
