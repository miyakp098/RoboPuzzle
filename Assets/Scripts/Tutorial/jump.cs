using System.Collections;
using UnityEngine;

public class jump : MonoBehaviour
{
    public LayerMask groundLayer;//インスペクターからレイヤーマスクを選べるようにする
    private float speed = 4;//歩くスピード
    private float jumpForce = 350f;//ジャンプ力
    private Rigidbody2D rb2d;
    private Animator anim;//アニメーション
    private bool isGround = false;//地面の判定
    private float interval = 1.0f; // 出力の間隔を設定する変数。デフォルトは1秒。
    public float v = 0;

    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(SetJumpValue());
    }

    

    private IEnumerator SetJumpValue()
    {
        while (true)
        {
            v = 1; // vに1をセット
            rb2d.AddForce(new Vector2(0, v) * jumpForce); // ジャンプの力を加える
            yield return new WaitForSeconds(2); // 1秒待つ
            v = 0; // vを0にセット
        }
    }
    void Update()
    {
        float velY = rb2d.velocity.y;
        //地面またはオブジェクトの上にいるときはジャンプモーションOFF
        if (isGround)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
        }

        if (velY > 0.5f)
        {
            anim.SetBool("isJump", true);
        }
        if (velY < -0.1f)
        {
            anim.SetBool("isFall", true);
        }
        Vector2 groundPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 groundArea = new Vector2(0.25f, 0.15f);//足場判定エリア
        isGround = Physics2D.OverlapArea(groundPos + groundArea, groundPos - groundArea, groundLayer);
    }


}
