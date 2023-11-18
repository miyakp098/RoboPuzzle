using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerB : MonoBehaviour
{
    private static float speed = 4;//歩くスピード
    public static float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private static float jumpForce = 400f;//ジャンプ力
    public static float JumpForce
    {
        get { return jumpForce; }
        set { jumpForce = value; }
    }

    public CameraSwitcher cameraSwitcher;
    //public PlayerA playerAScript;

    public LayerMask groundLayer;//インスペクターからレイヤーマスクを選べるようにする
    public LayerMask objLayer;

    private Rigidbody2D rb2d;


    private Animator anim;//アニメーション
    private bool isUsingSpell = false; // Spellアニメーションを使っているかどうかを追跡
    private SpriteRenderer spRenderer;

    private bool isGround = false;//地面の判定

    private bool onObject = false;//物の上にいる判定

    //SE
    public AudioClip jumpSE;

    private bool pushOrPull = false;

    public static bool isTouch = false;//Objを動かすときの接触判定
    float pullKeisuu = 1.0f;

    private string pullKey = "return";//引っ張るときのキー

    private static bool playerDirectionL;//プレイヤーの向きが左を向いているか
    public static bool PlayerDirectionL
    {
        get { return playerDirectionL; }
        set { playerDirectionL = value; }
    }

    private static float playerHorizontal;//プレイヤーの動き+か-か
    public static float PlayerHorizontal
    {
        get { return playerHorizontal; }
        set { playerHorizontal = value; }
    }

    private static bool playBoxMoveAudio = false; // このbool値でAudioClipの再生・停止をコントロール
    public static bool PlayBoxMoveAudio
    {
        get { return playBoxMoveAudio; }
        set { playBoxMoveAudio = value; }
    }
    private static bool isJump;
    public static bool IsJump
    {
        get { return isJump; }
        set { isJump = value; }
    }

    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.spRenderer = GetComponent<SpriteRenderer>();

        this.gameObject.SetActive(false);//bool値
    }


    void Update()
    {
        
        float x = Input.GetAxisRaw("Horizontal");//左-1・なにもしない0・右1
        playerHorizontal = x;

        float velX = rb2d.velocity.x;
        float velY = rb2d.velocity.y;


        

        //移動処理
        if (!isUsingSpell && PlayerA.PlayerMoveB && !PauseGame.IsPaused) // Spellアニメーションが再生されていない場合のみ移動を許可
        {


            //スプライトの向き、ジャンプ
            if (pushOrPull == false)//普通の時(pullKeyがfalseの時)
            {

                //スプライトの向きを変える
                if (x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    //spRenderer.flipX = true;
                    playerDirectionL = true;
                }
                else if (x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    //spRenderer.flipX = false;
                    playerDirectionL = false;
                }

                //ジャンプ
                if (Input.GetButtonDown("Jump") && (isGround || onObject)) // 地面またはオブジェクトの上
                {
                    rb2d.AddForce(Vector2.up * jumpForce);
                    GameManager.instance.PlaySE(jumpSE);
                }
            }

            //アニメーションの処理
            anim.SetFloat("Speed", Mathf.Abs(x * speed));//歩くアニメーション
            if (isGround || onObject)
            {
                anim.SetBool("isJump", false);
                anim.SetBool("isFall", false);
                isJump = false;
            }

            if (velY > 0.5f)
            {
                anim.SetBool("isJump", true);
                isJump = true;
            }
            if (velY < -0.1f)
            {
                anim.SetBool("isFall", true);
                isJump = true;
            }
        }




        
        


        //引っ張るときのスピード(pullKeyがtrueの時)
        if (Input.GetKey(pullKey) && isTouch && PlayerA.PlayerMoveB && !PauseGame.IsPaused)
        {

            anim.SetBool("isCatch", true);
            pullKeisuu = 0.5f;
            pushOrPull = true;

            if (playerDirectionL && x > 0)//引く処理
            {
                anim.SetBool("isPull", true);
                playBoxMoveAudio = true;
            }
            else if (playerDirectionL && x < 0)//押す処理
            {
                anim.SetBool("isPush", true);
                playBoxMoveAudio = true;
            }

            else if (!playerDirectionL && x < 0)//引く処理
            {
                anim.SetBool("isPull", true);
                playBoxMoveAudio = true;
            }
            else if (!playerDirectionL && x > 0)//押す処理
            {
                anim.SetBool("isPush", true);
                playBoxMoveAudio = true;
            }
            else
            {
                anim.SetBool("isPull", false);
                anim.SetBool("isPush", false);
                playBoxMoveAudio = false;
            }



        }
        else
        {
            anim.SetBool("isCatch", false);
            anim.SetBool("isPull", false);
            anim.SetBool("isPush", false);
            pullKeisuu = 1f;
            pushOrPull = false;
            playBoxMoveAudio = false;
        }

        //落ちた時の処理
        if (transform.position.y < -2)
        {
            //playerAScript.ChangePrayer();
            if (PlayerA.PlayerMoveB)
            {
                cameraSwitcher.SwitchCameras();
            }
            
            this.gameObject.SetActive(false);
        }

        //頭に箱が当たったらコピーがなくなる
        if(velY == 0 && BoxOnHed.DiePlayerB)
        {
            //playerAScript.ChangePrayer();
            if (PlayerA.PlayerMoveB)
            {
                cameraSwitcher.SwitchCameras();
            }
            this.transform.SetParent(null);
            this.gameObject.SetActive(false);
            BoxOnHed.DiePlayerB = false;
        }

    }

    //Player周りの接触判定
    private void FixedUpdate()
    {
        if (!isUsingSpell && PlayerA.PlayerMoveB && !PauseGame.IsPaused) // Spellアニメーションが再生されていない場合のみ移動を許可
        {
            //移動処理
            float x = Input.GetAxisRaw("Horizontal");//左-1・なにもしない0・右1
            rb2d.velocity = new Vector2(x * speed * pullKeisuu, rb2d.velocity.y);
        }
            

        //足場判定       

        Vector2 groundPos = new Vector2(transform.position.x, transform.position.y);

        Vector2 groundArea = new Vector2(0.235f, 0.1f);//足場判定エリア

        Debug.DrawLine(groundPos + groundArea, groundPos - groundArea, Color.red);

        //地面の上
        isGround = Physics2D.OverlapArea(groundPos + groundArea, groundPos - groundArea, groundLayer);
        //オブジェクトの上
        onObject = Physics2D.OverlapArea(groundPos + groundArea, groundPos - groundArea, objLayer);

        //Debug.Log(isGround);


        //つかみ判定
        if (playerDirectionL == false)
        {
            Vector2 touchPos = new Vector2(transform.position.x + 0.3f, transform.position.y + 0.7f);
            Vector2 touchArea = new Vector2(0.1f, 0.1f);//つかみ判定エリア

            Debug.DrawLine(touchPos + touchArea, touchPos - touchArea, Color.blue);

            isTouch = Physics2D.OverlapArea(touchPos + touchArea, touchPos - touchArea, objLayer);
        }
        else if (playerDirectionL == true)
        {
            Vector2 touchPos = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.7f);
            Vector2 touchArea = new Vector2(0.1f, 0.1f);//つかみ判定エリア

            Debug.DrawLine(touchPos + touchArea, touchPos - touchArea, Color.blue);

            isTouch = Physics2D.OverlapArea(touchPos + touchArea, touchPos - touchArea, objLayer);
        }



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
