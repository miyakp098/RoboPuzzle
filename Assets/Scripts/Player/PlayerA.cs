using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerA : MonoBehaviour
{
    [Header("spellの数")] public int spellCount = 0;
    private static int spellNum = 0;//他からspellCountにアクセス
    public static int SpellNum
    {
        get { return spellNum; }
        //set { spellNum = value; }
    }

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

    private static bool isJump;//ジャンプ力
    public static bool IsJump
    {
        get { return isJump; }
        set { isJump = value; }
    }

    public CameraSwitcher cameraSwitcher;

    //public Text textNum;//後で削除
    private static string textReadBook = "";
    public static string TextReadBook
    {
        get { return textReadBook; }
        set { textReadBook = value; }
    }

    public ObjInOut objInOut;//ObjInOutスクリプト

    public LayerMask groundLayer;//インスペクターからレイヤーマスクを選べるようにする
    public LayerMask objLayer;

    private Rigidbody2D rb2d;
    private Rigidbody2D playerBRb2D;//playerBのRigidbody

    public GameObject playerB;
    public GameObject childObj; // 子オブジェクトを参照するための変数

    public PlayerB playerBScript;

    private static bool playerMoveA = true;
    public static bool PlayerMoveA
    {
        get { return playerMoveA; }
        set { playerMoveA = value; }
    }
    

    private static bool playerMoveB = false;
    public static bool PlayerMoveB
    {
        get { return playerMoveB; }
        set { playerMoveB = value; }
    }

    

    private static int operable = 1;
    public static int Operable
    {
        get { return operable; }
        set { operable = value; }
    }


    //private int currentPlayerChangeIndex = 0;

    private Animator anim;//アニメーション
    private static bool isUsingSpell = false; // Spellアニメーションを使っているかどうかを追跡
    public static bool IsUsingSpell
    {
        get { return isUsingSpell; }
        set { isUsingSpell = value; }
    }

    private List<string> triggers = new List<string> { "useSpell1", "useSpell2", "useSpell3", "useSpell4"};
    private static int currentTriggerIndex = 0;
    public static int CurrentTriggerIndex
    {
        get { return currentTriggerIndex; }
        set { currentTriggerIndex = value; }
    }


    private bool isGround = false;//地面の判定

    private bool onObject = false;//物の上にいる判定

    [Header("魔法のプレハブ")] public GameObject magicShotPrefabLarge; // 弾のプレハブ
    [Header("魔法のプレハブ")] public GameObject magicShotPrefabSmall; // 弾のプレハブ

    //SE
    public AudioClip changeSpellAudio;
    public AudioClip shotSE;
    public AudioClip shotSE2;
    public AudioClip jumpSE;
    private bool hasPlayedAudio = false; // 追加: SEを再生したかのフラグ


    private static bool playBoxMoveAudio = false; // このbool値でAudioClipの再生・停止をコントロール
    public static bool PlayBoxMoveAudio
    {
        get { return playBoxMoveAudio; }
        set { playBoxMoveAudio = value; }
    }

    private bool canShoot = true; // 発射可能かどうかのフラグ
    private string spellKey = "f";//魔法を撃つキー
    private string spellChangeKey = "w";//魔法を変えるキー
    private string spellChangeKeyMinus = "s";//魔法を変えるキー
    private string playerChangeKeyL = "left shift";//playerの切り替えキー
    private string playerChangeKeyR = "right shift";
    private string pullKey = "return";//引っ張るときのキー

    

    private bool pushOrPull = false;

    public static bool isTouch = false;//Objを動かすときの接触判定
    float pullKeisuu = 1.0f;

    public GameObject prohibite_1;//バツマーク

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRendererB;

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

    private bool isChanging = false;

    void Start()
    {
        spellNum = spellCount;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRendererB = playerB.GetComponent<SpriteRenderer>();
        isUsingSpell = false;

        this.rb2d = GetComponent<Rigidbody2D>();

        this.anim = GetComponent<Animator>();

        playerDirectionL = false;
        currentTriggerIndex = 0;
        playerBRb2D = playerB.GetComponent<Rigidbody2D>();


        operable = 1;
        anim.SetTrigger(0);
    }
    

    void Update()
    {
        Cursor.visible = false;
        //色を変える
        if (PlayerMoveA)
        {
            spriteRenderer.color = new Color32(255, 255, 255, 255);
            spriteRendererB.color = new Color32(130, 130, 130, 255);
        }
        else if (PlayerMoveB)
        {
            spriteRenderer.color = new Color32(130, 130, 130, 255);
            spriteRendererB.color = new Color32(255, 255, 255, 255);
        }
        




        float x = Input.GetAxisRaw("Horizontal");//左-1・なにもしない0・右1
        playerHorizontal = x;
        
        float velY = rb2d.velocity.y;

        //操作Playerの切り替え
        //（キー入力）、playerが動いていない時かつ魔法を使ってない時playerBオブジェクトがアクティブの時かつmoveCameraが動いていない時
        if ((Input.GetKeyDown(playerChangeKeyL) || Input.GetKeyDown(playerChangeKeyR)) && !isUsingSpell && x == 0 && velY==0 && playerBRb2D.velocity.y==0 && playerB.activeInHierarchy && !cameraSwitcher.moveCamera.enabled)
        {
            cameraSwitcher.SwitchCameras();
            //ChangePrayer();

        }

        //アクティブなカメラのplayerの操作を可能にする、カメラの切り替え時は操作不可
        if (cameraSwitcher.mainCamera.enabled)
        {
            playerMoveA = true;
            playerMoveB = false;
        }
        else if(cameraSwitcher.subCamera.enabled)
        {
            playerMoveA = false;
            playerMoveB = true;
        }
        else
        {
            playerMoveA = false;
            playerMoveB = false;
        }

        

        //移動処理
        if (!isUsingSpell && playerMoveA && !PauseGame.IsPaused) // Spellアニメーションが再生されていないかつplayerMoveがtrueの場合移動を許可
        {
            

            

            //スプライトの向き、ジャンプ
            if (pushOrPull == false)//普通の時(pullKeyがfalseの時)
            {

                //スプライトの向きを変える
                if (x * operable < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    //spRenderer.flipX = true;
                    playerDirectionL = true;
                }
                else if (x * operable > 0)
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
            anim.SetFloat("Speed", Mathf.Abs(x * speed * operable));//歩くアニメーション
            

            //地面またはオブジェクトの上にいるときはジャンプモーションOFF
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






        //押し引きの操作とアニメーション(pullKeyがtrueの時)
        if (Input.GetKey(pullKey) && isTouch && PlayerMoveA && !PauseGame.IsPaused)
        {

            anim.SetBool("isCatch", true);
            pullKeisuu = 0.5f;
            pushOrPull = true;

            if (playerDirectionL && x > 0)//引く処理
            {
                anim.SetBool("isPull", true);
                playBoxMoveAudio = true;
            }
            else if(playerDirectionL && x < 0)//押す処理
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
            PlayerMoveA = false;

        }








        //魔法の切り替え
        if (!isChanging && PlayerMoveA && spellCount != 0 && !isUsingSpell && spellCount != 0 && !PauseGame.IsPaused)
        {
            if (Input.GetKeyDown(spellChangeKey))
            {
                
                isChanging = true;
                ChangeTrigger();
                StartCoroutine(WaitSeconds());
                
            }
            if (Input.GetKeyDown(spellChangeKeyMinus))
            {
                
                isChanging = true;
                ChangeTriggerMinus();
                StartCoroutine(WaitSeconds());
                
            }
        }
        
        
        //魔法を撃つ処理
        if(spellCount != 0)
        {
            //キー入力（playerAが動いていないときかつメインカメラがアクティブの時かつ魔法を使っていない時）
            if (Input.GetKeyDown(spellKey) && x == 0 && velY == 0 && cameraSwitcher.mainCamera.enabled && !isUsingSpell)
            {
                isUsingSpell = true; // Spellアニメーションが開始されたことを示す
                anim.SetTrigger(triggers[currentTriggerIndex]);

            }
        }
    }

    //大きさを変える時に待機時間を設ける
    private IEnumerator WaitSeconds()
    {
        
        yield return new WaitForSeconds(0.3f);  // 待機時間
        isChanging = false;
    }

    //Player周りの接触判定
    private void FixedUpdate()
    {
        if (!isUsingSpell && playerMoveA && !PauseGame.IsPaused) // Spellアニメーションが再生されていない場合のみ移動を許可
        {
            //移動処理
            float x = Input.GetAxisRaw("Horizontal");//左-1・なにもしない0・右1
            rb2d.velocity = new Vector2(x * speed * pullKeisuu * operable, rb2d.velocity.y);
        }

        //足場判定       

        Vector2 groundPos = new Vector2(transform.position.x,transform.position.y);

        Vector2 groundArea = new Vector2(0.235f, 0.1f);//足場判定エリア

        Debug.DrawLine(groundPos + groundArea, groundPos - groundArea, Color.red);

        //地面の上
        isGround = Physics2D.OverlapArea(groundPos + groundArea, groundPos - groundArea, groundLayer);
        //オブジェクトの上
        onObject = Physics2D.OverlapArea( groundPos + groundArea, groundPos - groundArea, objLayer);

        //Debug.Log(isGround);


        //つかみ判定
        if (playerDirectionL == false)
        {
            Vector2 touchPos = new Vector2(transform.position.x + 0.3f,transform.position.y + 0.7f);
            Vector2 touchArea = new Vector2(0.1f, 0.1f);//つかみ判定エリア

            Debug.DrawLine(touchPos + touchArea, touchPos - touchArea, Color.blue);

            isTouch = Physics2D.OverlapArea(touchPos + touchArea, touchPos - touchArea, objLayer);
        }
        else if (playerDirectionL == true)
        {
            Vector2 touchPos = new Vector2(transform.position.x - 0.3f,transform.position.y + 0.7f);
            Vector2 touchArea = new Vector2(0.1f, 0.1f);//つかみ判定エリア

            Debug.DrawLine(touchPos + touchArea, touchPos - touchArea, Color.blue);

            isTouch = Physics2D.OverlapArea(touchPos + touchArea, touchPos - touchArea, objLayer);
        }

        

    }

    //魔法の順番変更
    private void ChangeTrigger()
    {
        GameManager.instance.PlaySE(changeSpellAudio);
        currentTriggerIndex++;

        // トリガーリストの最後に達した場合、インデックスを最初に戻す
        if (currentTriggerIndex >= spellCount)
        {
            currentTriggerIndex = 0;
        }
    }

    //魔法の順番変更
    private void ChangeTriggerMinus()
    {
        GameManager.instance.PlaySE(changeSpellAudio);
        currentTriggerIndex--;

        // トリガーリストの0より小さくなった時、インデックスを3に戻す
        if (currentTriggerIndex < 0)
        {
            currentTriggerIndex = spellCount　-　1;
        }
    }


    //魔法4：箱の出し入れ
    public void SpellObjInOut()
    {
        //playerの手がmoveObjに触れている時そのオブジェクトを取得する
        if (!ObjInOut.InMoveObj)
        {
            objInOut.TakeInMoveObj();
            textReadBook = ObjInOut.TextReadBook;
        }
        else if (ObjInOut.InMoveObj && !CopyArea.ObjInArea)
        {
            Debug.Log($"{ObjInOut.InMoveObj},{!CopyArea.ObjInArea}");
            objInOut.GenerateMoveObj();
        }
        else
        {
            //textReadBook = ("前にオブジェクトがあるため生成できない");
            prohibite_1.SetActive(true);
        }
    }
    

    //魔法3：PLayerのコピーを生成するメソッド（spellのイベントトリガーに設定する）
    //PlayerBをPlayerの子オブジェクトの場所に持ってくるメソッド
    public void MovePlayerBToChildPosition()
    {
        //copyAreaに障害物がある時不発にする
        if (CopyArea.UseSpellCopy || playerB.activeSelf)
        {
            GameManager.instance.PlaySE(shotSE2);
            if (childObj != null) // childObjが設定されているか確認します。
            {
                if(playerB.activeSelf == false)//Playerのコピーが非アクティブの時
                {
                    playerB.transform.position = childObj.transform.position;
                }
                
            }
            if (playerB != null)
            {
                //playerBのアクティブ状態を切り替える
                playerBScript.TogglePlayerBActiveState();
            }
            playerMoveA = true;
            playerMoveB = false;
            //currentPlayerChangeIndex = 0;//ChangePrayer()メソッドのインデックスを0に初期化
        }
        else
        {
            //textReadBook = ("前に障害物があるか足場がない");
            prohibite_1.SetActive(true);
        }
        
    }

    // 魔法1：弾を発射するメソッド（大きくする）
    public void ShootLarge()
    {
        if (canShoot)
        {
            GameManager.instance.PlaySE(shotSE);
            //発射位置
            Vector2 playerPosForword = playerDirectionL ? new Vector2(this.transform.position.x - 0.6f, this.transform.position.y + 0.85f) : new Vector2(this.transform.position.x + 0.6f, this.transform.position.y + 0.85f);
            // 弾のプレハブから新しい弾オブジェクトを生成
            GameObject magicShot = Instantiate(magicShotPrefabLarge, playerPosForword, Quaternion.identity);

            // 弾を発射する方向を設定（ここでは上向きに設定）

            magicShot.transform.right = playerDirectionL ? -transform.right : transform.right;

            // 発射後、次の発射まで一時停止する
            canShoot = false;
            // 1秒後に再び発射可能にする
            Invoke("EnableShooting", 0.2f);
        }
    }

    // 魔法2：弾を発射するメソッド（小さくする）
    public void ShootSmall()
    {
        if (canShoot)
        {
            GameManager.instance.PlaySE(shotSE);
            //発射位置
            Vector2 playerPosForword = playerDirectionL ? new Vector2(this.transform.position.x - 0.6f, this.transform.position.y + 0.85f) : new Vector2(this.transform.position.x + 0.6f, this.transform.position.y + 0.85f);
            // 弾のプレハブから新しい弾オブジェクトを生成
            GameObject magicShot = Instantiate(magicShotPrefabSmall, playerPosForword, Quaternion.identity);

            // 弾を発射する方向を設定（ここでは上向きに設定）

            magicShot.transform.right = playerDirectionL ? -transform.right : transform.right;

            // 発射後、次の発射まで一時停止する
            canShoot = false;
            // 1秒後に再び発射可能にする
            Invoke("EnableShooting", 0.2f);
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
        prohibite_1.SetActive(false);
        //textReadBook = "";
    }

}
