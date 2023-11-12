using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;//歩くスピード

    public float jumpForce = 200f;//ジャンプ力

    public LayerMask groundLayer;
    public LayerMask touchLayer;//インスペクターからレイヤーマスクを選べるようにする

    private Rigidbody2D rb2d;

    private Animator amin;//アニメーション

    private SpriteRenderer spRenderer;

    private bool isGround = false;//地面の判定

    private bool onObject = false;//物の上にいる判定

    public static bool isTouch = false;//cを動かすときの接触判定
    float pullKeisuu = 1.0f;

    private moveFloor1 moveObj = null;

    private string pullKey = "k";//引っ張るときのキー

    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.amin = GetComponent<Animator>();
        this.spRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");//左-1・なにもしない0・右1

        float velX = rb2d.velocity.x;
        float velY = rb2d.velocity.y;
        float maxSpeed = 5.0f;


        rb2d.AddForce(Vector2.right * x * speed * pullKeisuu);//横方向に力を加える

        amin.SetFloat("Speed", Mathf.Abs(x * speed));//歩くアニメーション

        if (velX > maxSpeed * pullKeisuu)
        {
            rb2d.velocity = new Vector2(maxSpeed * pullKeisuu, velY);
        }
        if (velX < -maxSpeed * pullKeisuu)
        {
            rb2d.velocity = new Vector2(-maxSpeed * pullKeisuu, velY);
        }


        if (Input.GetKey(pullKey) == false)//普通の時(pullKeyがfalseの時)
        {
            pullKeisuu = 1.0f;
            //スプライトの向きを変える
            if (x < 0)
            {
                spRenderer.flipX = true;
            }
            else if (x > 0)
            {
                spRenderer.flipX = false;
            }

            //ジャンプ
            if (Input.GetButtonDown("Jump") & isGround)//地面の上
            {
                rb2d.AddForce(Vector2.up * jumpForce);
            }
            if (Input.GetButtonDown("Jump") & onObject)//オブジェクトの上
            {
                rb2d.AddForce(Vector2.up * jumpForce);
            }
        }

        if (Input.GetKey(pullKey))//引っ張るときの時(pullKeyがtrueの時)
        {
            pullKeisuu = 0.5f;
        }


        //移動速度を設定
        Vector2 addVelocity = Vector2.zero;
        if (moveObj != null)
        {
            addVelocity = moveObj.GetVelocity();
        }
        rb2d.velocity = new Vector2(velX, velY);





        //落ちた時の処理
        if (transform.position.y < -2)
        {
            this.transform.position = new Vector2(0, 1);

        }

    }

    private void FixedUpdate()
    {
        //足場判定       

        Vector2 groundPos =
            new Vector2(
                transform.position.x,
                transform.position.y
            );

        Vector2 groundArea = new Vector2(0.3f, 0.2f);//足場判定エリア

        Debug.DrawLine(groundPos + groundArea, groundPos - groundArea, Color.red);

        //地面の上
        isGround =
            Physics2D.OverlapArea(
                groundPos + groundArea,
                groundPos - groundArea,
                groundLayer
            );
        //オブジェクトの上
        onObject =
            Physics2D.OverlapArea(
                groundPos + groundArea,
                groundPos - groundArea,
                touchLayer
            );

        //Debug.Log(isGround);


        //つかみ判定
        if (spRenderer.flipX == false)
        {
            Vector2 touchPos =
            new Vector2(
                transform.position.x + 0.45f,
                transform.position.y + 0.5f
            );
            Vector2 touchArea = new Vector2(0.2f, 0.5f);//つかみ判定エリア

            Debug.DrawLine(touchPos + touchArea, touchPos - touchArea, Color.blue);

            isTouch =
                Physics2D.OverlapArea(
                    touchPos + touchArea,
                    touchPos - touchArea,
                    touchLayer
                );
        }
        else if (spRenderer.flipX == true)
        {
            Vector2 touchPos =
            new Vector2(
                transform.position.x - 0.55f,
                transform.position.y + 0.5f
            );
            Vector2 touchArea = new Vector2(0.2f, 0.5f);//つかみ判定エリア

            Debug.DrawLine(touchPos + touchArea, touchPos - touchArea, Color.blue);

            isTouch =
                Physics2D.OverlapArea(
                    touchPos + touchArea,
                    touchPos - touchArea,
                    touchLayer
                );
        }

        

        //Debug.Log(isTouch);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "MoveFloor")
        {
            //動く床から離れた
            moveObj = null;
        }
    }
}
