using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    public GameObject player;// Playerオブジェクトを入れる

    public GameObject playerB;// Playerオブジェクトを入れる

    public moveObjTrigger moveObjTrigger;//moveObjの子Objを入れる
    public moveObjTrigger2 moveObjTrigger2;
    public moveObjTrigger3 moveObjTrigger3;

    //public LayerMask objLayer;//インスペクターからレイヤーマスクを選べるようにする

    private float offset = 0; // Playerとの相対座標のオフセット

    private string pullKey = "return";//引っ張るときのキー

    private bool isWall = false;

    private bool isPushObj = false;

    private bool onMoveFloor = false;

    private bool isTouch = false;

    private bool isTouchB = false;

    private Rigidbody2D rb2d;

    private float newMass = 1;

    private bool onObject;
    //private bool detachChildObj = false;
    public GameObject trigger1;
    public GameObject trigger2;

    private SpriteRenderer spriteRenderer;

    //大きさを変更するスクリプト
    private float lerpSpeed = 3f; // 補間の速度。この値を変更して、スケール変更の速さを調整することができる
    private Vector3 initialScale;  // 初期のスケール値
    private Vector3 targetScale;   // スケール値（目標）
    private bool isScaling = false;  // スケーリング中かどうかを示すフラグ

    //動かす時のPlayerとMoveObjの離れ
    private float distancePlayerMobeObj = 0.335f;

    Color onFloorColor = new Color32(230, 230, 230, 255);

    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();

        initialScale = new Vector3(1,1,1);

        spriteRenderer = GetComponent<SpriteRenderer>();


        if(transform.localScale.x == 1)
        {
            moveObjTrigger.isActive = true;
            moveObjTrigger2.isActive = false;
            moveObjTrigger3.isActive = false;
        }
        else if(transform.localScale.x == 2)
        {
            moveObjTrigger.isActive = false;
            moveObjTrigger2.isActive = true;
            moveObjTrigger3.isActive = false;
            trigger2.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1.1f);
            trigger1.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 1f);
        }
        
    }

    private void Update()
    {
        

        //大きさを変える
        if (isScaling)
        {
            moveObjTrigger3.isActive = true;
            //Playerが触れた時Xの固定を外す
            if (isWall || isPushObj)
            {
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            
            // 現在のスケールと目標のスケールとの間で補間する
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, lerpSpeed * Time.deltaTime);

            // 大きくなっているときに物体に挟まったら元の大きさに戻す
            if (moveObjTrigger3.BackScale)
            {
                targetScale = initialScale * 1f;
            }

            // 十分に近づいたらスケーリングを停止する
            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
            {
                transform.localScale = targetScale;
                isScaling = false;
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                spriteRenderer.color = Color.white;
                //detachChildObj = false;
                if(targetScale == initialScale * 2)
                {
                    moveObjTrigger2.isActive = true;
                    trigger2.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1.1f);
                    trigger1.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 1f);
                    moveObjTrigger3.BackScale = false; moveObjTrigger3.isActive = false;

                }
                else if(targetScale == initialScale)
                {
                    moveObjTrigger.isActive = true;
                    //trigger2の位置を変更
                    trigger2.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
                    trigger1.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
                    moveObjTrigger3.BackScale = false; moveObjTrigger3.isActive = false;
                }
                
            }
        }
        //isPushObjに当たったとき
        else if (isPushObj)
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }

        //魔法4で取り込んだ時
        if(transform.localScale.x < 0.5f)
        {
            moveObjTrigger.isActive = true;
            moveObjTrigger2.isActive = false;
            //trigger2の位置を変更
            trigger2.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            trigger1.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            moveObjTrigger3.BackScale = false; moveObjTrigger3.isActive = false;
        }

        //画面外へ落ちた時の処理
        if (transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }

        //上に重なったオブジェクトの重量を足す
        rb2d.mass = newMass + moveObjTrigger.TotalMass + moveObjTrigger2.TotalMass;



        //オブジェクトか地面の上にあるかの判定
        Vector2 groundPos = new Vector2(transform.position.x, transform.position.y - 0.65f * transform.localScale.y);
        Vector2 groundArea = new Vector2(0.5f * transform.localScale.x, 0.1f * transform.localScale.y); //地面判定エリア
        Debug.DrawLine(groundPos + groundArea, groundPos - groundArea, Color.red);
        onObject = Physics2D.OverlapArea(groundPos + groundArea, groundPos - groundArea);

        //上にオブジェクトが重なっている場合動けない
        if (onObject && !moveObjTrigger.OnMoveObj && !moveObjTrigger2.OnMoveObj)
        {


            float x = this.transform.position.x - player.transform.position.x;
            float y = this.transform.position.y - player.transform.position.y - 0.5f;//playerとMobeObjのyの位置の差
            if (x > 0)//Player⇔Obj
            {
                offset = transform.localScale.x / 2 + distancePlayerMobeObj;//プレイヤーの大きさによって変える
            }
            else if (x < 0)//Obj⇔Player
            {
                offset = -(transform.localScale.x / 2 + distancePlayerMobeObj);//プレイヤーの大きさによって変える
            }

            
            //playerとMobeObjのyの位置の差が以下の時
            if ((-0.1f < y) && (y < 0.6f))
            {
                //キーを押しているときかつPlayerがオブジェクトに触れたときかつPlayerA.PlayerMoveAがtrueの時
                if (Input.GetKey(pullKey) && isTouch && PlayerA.PlayerMoveA)
                {

                    if (isWall == true)//Objを引く処理のみ（壁に当たっているとき）
                    {

                        //Playerが右を向いていてかつPlayer⇔Objの位置関係にある時かつPlayerの動きが左に向かっているとき
                        if (x > 0 && PlayerA.PlayerHorizontal == -1)
                        {
                            this.transform.position = new Vector3(player.transform.position.x + offset, this.transform.position.y);
                        }
                        //Playerが左を向いていてかつObj⇔Playerの位置関係にある時かつPlayerの動きが右に向かっているとき
                        if (x < 0 && PlayerA.PlayerHorizontal == 1)
                        {
                            this.transform.position = new Vector3(player.transform.position.x + offset, this.transform.position.y);
                        }
                        // Playerの動きに追従する
                        //this.transform.position = new Vector3(player.transform.position.x + offset, this.transform.position.y);

                    }
                    else if (isWall == false)//Objを押す、引く処理（壁に当たっていないとき）
                    {
                        //Playerが右を向いていてかつPlayer⇔Objの位置関係にある時
                        if (x > 0)
                        {
                            this.transform.position = new Vector3(player.transform.position.x + offset, this.transform.position.y);
                        }
                        //Playerが左を向いていてかつObj⇔Playerの位置関係にある時
                        if (x < 0)
                        {
                            this.transform.position = new Vector3(player.transform.position.x + offset, this.transform.position.y);
                        }
                    }


                }

            }

            float xB = this.transform.position.x - playerB.transform.position.x;
            float yB = this.transform.position.y - playerB.transform.position.y - 0.5f;
            if (xB > 0)//Player⇔Obj
            {
                offset = transform.localScale.x / 2 + distancePlayerMobeObj;//プレイヤーの大きさによって変える
            }
            else if (xB < 0)//Obj⇔Player
            {
                offset = -(transform.localScale.x / 2 + distancePlayerMobeObj);//プレイヤーの大きさによって変える
            }



            //playerとMobeObjのyの位置の差が以下の時
            if ((-0.1f < yB) && (yB < 0.6f))
            {
                //キーを押しているときかつPlayerBがオブジェクトに触れたとき
                if (Input.GetKey(pullKey) && isTouchB && PlayerA.PlayerMoveB)
                {

                    if (isWall == true)//Objを引く処理のみ（壁に当たっているとき）
                    {

                        //Playerが右を向いていてかつPlayer⇔Objの位置関係にある時かつPlayerの動きが左に向かっているとき
                        if (xB > 0 && PlayerB.PlayerHorizontal == -1)
                        {
                            this.transform.position = new Vector3(playerB.transform.position.x + offset, this.transform.position.y);
                        }
                        //Playerが左を向いていてかつObj⇔Playerの位置関係にある時かつPlayerの動きが右に向かっているとき
                        if (xB < 0 && PlayerB.PlayerHorizontal == 1)
                        {
                            this.transform.position = new Vector3(playerB.transform.position.x + offset, this.transform.position.y);
                        }
                        // Playerの動きに追従する
                        //this.transform.position = new Vector3(player.transform.position.x + offset, this.transform.position.y);

                    }
                    else if (isWall == false)//Objを押す、引く処理（壁に当たっていないとき）
                    {
                        //Playerが右を向いていてかつPlayer⇔Objの位置関係にある時
                        if (xB > 0)
                        {
                            this.transform.position = new Vector3(playerB.transform.position.x + offset, this.transform.position.y);
                        }
                        //Playerが左を向いていてかつObj⇔Playerの位置関係にある時
                        if (xB < 0)
                        {
                            this.transform.position = new Vector3(playerB.transform.position.x + offset, this.transform.position.y);
                        }
                    }


                }

            }
        }
        
    }

    //大きさを変える時に待機時間を設ける
    private IEnumerator StartScalingAfterDelay(float s)
    {
        yield return new WaitForSeconds(s);  // 1秒待機
        isScaling = true;
        
    }


    // この関数を呼び出すことで、スケールを徐々に2倍にします
    private void DoubleSize()
    {
        if (onMoveFloor || this.spriteRenderer.color == onFloorColor)
        {
            return;
        }
        moveObjTrigger.isActive = false;
        moveObjTrigger2.isActive = false;
        //detachChildObj = true;
        //色をかえる
        spriteRenderer.color = new Color32(255, 200, 200, 255);
        moveObjTrigger.DetachAllChildren();
        moveObjTrigger2.DetachAllChildren();
        
        targetScale = initialScale * 2;
        StartCoroutine(StartScalingAfterDelay(1.5f));
    }

    // この関数を呼び出すことで、スケールを徐々に1倍にします
    private void NormalSize()
    {
        if (onMoveFloor || this.spriteRenderer.color == onFloorColor)
        {
            return;
        }
        moveObjTrigger.isActive = false;
        moveObjTrigger2.isActive = false;
        //detachChildObj = true;
        //色をかえる
        spriteRenderer.color = new Color32(200, 200, 255, 255);
        moveObjTrigger.DetachAllChildren();
        moveObjTrigger2.DetachAllChildren();

        targetScale = initialScale * 1f;
       
        StartCoroutine(StartScalingAfterDelay(0.5f));


    }

    // この関数を呼び出すことで、スケールを徐々に0倍にします
    private void ZeroSize()
    {
        targetScale = initialScale * 0f;
        isScaling = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // このオブジェクトの子オブジェクトとの接触を無視する
        if (collision.transform.IsChildOf(transform))
        {
            return;
        }
        

        if (collision.gameObject.CompareTag("moveObj"))
        {
            isWall = true;
            //Debug.Log(this.gameObject.name);
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            isWall = true;
        }

        if (collision.gameObject.CompareTag("moveFloor"))
        {
            onMoveFloor = true;
            spriteRenderer.color = onFloorColor;
            moveObjTrigger.colorChange = true;
        }
        if (collision.gameObject.CompareTag("pushObj"))
        {
            isPushObj = true; 
        }

        // Playerというタグのオブジェクトのcircleコライダーが触れたときの処理を追加
        if (collision.gameObject.CompareTag("Player") && collision is CircleCollider2D)
        {
            isTouch = true;
        }

        // Playerというタグのオブジェクトのcircleコライダーが触れたときの処理を追加
        if (collision.gameObject.CompareTag("PlayerB") && collision is CircleCollider2D)
        {
            isTouchB = true;
            
        }

        //// Playerというタグのオブジェクトのcircleコライダーが触れたときの処理を追加
        //if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerB"))
        //{
        //    //Playerが触れた時Xを固定する
        //    rb2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            
        //}

        if (collision.gameObject.CompareTag("ballLarge"))//衝突したオブジェクトの大きさを２倍にする
        {
            DoubleSize();
            
        }

        if (collision.gameObject.CompareTag("ballSmall"))//衝突したオブジェクトの大きさを元の大きさにする
        {
            NormalSize();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // このオブジェクトの子オブジェクトとの接触を無視する
        if (collision.transform.IsChildOf(transform))
        {
            return;
        }

        if (collision.gameObject.CompareTag("moveObj"))
        {
            isWall = false;
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            isWall = false;
        }
        if (collision.gameObject.CompareTag("pushObj"))
        {
            isPushObj = false;
        }
        if (collision.gameObject.CompareTag("moveFloor"))
        {
            onMoveFloor = false;
            spriteRenderer.color = new Color32(255, 255, 255, 255);
            moveObjTrigger.colorChange = false;
        }
        // Playerというタグのオブジェクトのcircleコライダーが離れたときの処理を追加
        if (collision.gameObject.CompareTag("Player") && collision is CircleCollider2D)
        {
            isTouch = false;
            
        }

        // Playerというタグのオブジェクトのcircleコライダーが離れたときの処理を追加
        if (collision.gameObject.CompareTag("PlayerB") && collision is CircleCollider2D)
        {
            isTouchB = false;
            
        }

        //if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerB"))
        //{
        //    //Playerが触れた時Xを固定する
        //    rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Playerというタグのオブジェクトのcircleコライダーが触れたときの処理を追加
        if (collision.gameObject.CompareTag("Player") && collision is CircleCollider2D)
        {
            isTouch = true;
            
        }
        // Playerというタグのオブジェクトのcircleコライダーが触れたときの処理を追加
        if (collision.gameObject.CompareTag("PlayerB") && collision is CircleCollider2D)
        {
            isTouchB = true;
            
        }
        if (collision.gameObject.CompareTag("pushObj"))
        {
            isPushObj = true;
        }
    }
}
