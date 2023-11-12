using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellOnMoveObj : MonoBehaviour
{
    private float speed = 4;//歩くスピード
    private float jumpForce = 400f;//ジャンプ力
    private Rigidbody2D rb2d;
    private Animator anim;//アニメーション
    public LayerMask groundLayer;//インスペクターからレイヤーマスクを選べるようにする

    private bool isGround = false;//地面の判定
    private float interval = 1.0f; // 出力の間隔を設定する変数。デフォルトは1秒。
    public float v = 0;
    private bool move = true;
    private Vector3 originalPosition;


    [Header("魔法のプレハブ")] public GameObject magicShotPrefabLarge; // 弾のプレハブ
    [Header("魔法のプレハブ")] public GameObject magicShotPrefabSmall; // 弾のプレハブ


    private bool isUsingSpell = false; // Spellアニメーションを使っているかどうかを追跡
    private bool canShoot = true; // 発射可能かどうかのフラグ
    private bool playerDirectionL = false;//プレイヤーの向きが左を向いているか


    void Start()
    {

        this.rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalPosition = transform.position;
    }

    

    float x = 0;

    private IEnumerator SetJumpValue()
    {
        
            v = 1; // vに1をセット
            x = 1;
            rb2d.AddForce(new Vector2(0, v) * jumpForce); // ジャンプの力を加える
            yield return new WaitForSeconds(0.6f); // 1秒待つ
            v = 0; // vを0にセット
            x = 0;
        StartCoroutine(ResetSet());

    }
    private IEnumerator ResetSet()
    {

        yield return new WaitForSeconds(3.2f); // 1秒待つ
        transform.position = originalPosition;    // 1秒後にポジションも元に戻す
        move = true;

    }

    void Update()
    {
        if (move)
        {
            StartCoroutine(SetSpellTrigger());
            //StartCoroutine(SetJumpValue());
            move = false;
            
        }
        

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
        

        //アニメーションの処理
        anim.SetFloat("Speed", Mathf.Abs(x * speed));//歩くアニメーション

        

    }



    //Player周りの接触判定
    private void FixedUpdate()
    {

        {
            
            rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);
        }

    }
    private IEnumerator SetSpellTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("useSpell1");
            yield return new WaitForSeconds(1f);
            StartCoroutine(SetJumpValue());


    }
    // 魔法1：弾を発射するメソッド（大きくする）
    public void ShootLarge()
    {
        if (canShoot)
        {

            //発射位置
            Vector2 playerPosForword = playerDirectionL ? new Vector2(this.transform.position.x - 0.6f, this.transform.position.y + 0.85f) : new Vector2(this.transform.position.x + 0.6f, this.transform.position.y + 0.85f);
            // 弾のプレハブから新しい弾オブジェクトを生成
            GameObject magicShot = Instantiate(magicShotPrefabLarge, playerPosForword, Quaternion.identity);

            // 弾を発射する方向を設定（ここでは上向きに設定）

            magicShot.transform.right = playerDirectionL ? -transform.right : transform.right;

            // 発射後、次の発射まで一時停止する
            canShoot = false;
            // 1秒後に再び発射可能にする
            Invoke("EnableShooting", 1f);
        }
    }

    // 魔法2：弾を発射するメソッド（小さくする）
    public void ShootSmall()
    {
        if (canShoot)
        {

            //発射位置
            Vector2 playerPosForword = playerDirectionL ? new Vector2(this.transform.position.x - 0.6f, this.transform.position.y + 0.85f) : new Vector2(this.transform.position.x + 0.6f, this.transform.position.y + 0.85f);
            // 弾のプレハブから新しい弾オブジェクトを生成
            GameObject magicShot = Instantiate(magicShotPrefabSmall, playerPosForword, Quaternion.identity);

            // 弾を発射する方向を設定（ここでは上向きに設定）

            magicShot.transform.right = playerDirectionL ? -transform.right : transform.right;

            // 発射後、次の発射まで一時停止する
            canShoot = false;
            // 1秒後に再び発射可能にする
            Invoke("EnableShooting", 1f);
        }
    }


    // 発射を再び可能にするメソッド
    private void EnableShooting()
    {
        canShoot = true;
    }

    // アニメーションイベントなどを使用してアニメーションの終了時に呼び出すメソッド
    public void OnSpellAnimationEnd()
    {
        isUsingSpell = false; // Spellアニメーションが終了したことを示す

        //textReadBook = "";
    }
}
