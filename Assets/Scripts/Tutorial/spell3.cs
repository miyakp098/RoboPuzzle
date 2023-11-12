using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell3 : MonoBehaviour
{
    private float speed = 2;//歩くスピード
    private float jumpForce = 400f;//ジャンプ力
    private Rigidbody2D rb2d;
    private Animator anim;//アニメーション
    public ObjInOutC objInOut;//ObjInOutスクリプト
    public CopyAreaC copyAreaC;//ObjInOutスクリプト
    private bool isUsingSpell = false; // Spellアニメーションを使っているかどうかを追跡
    public GameObject prohibite_1;//バツマーク
    public GameObject playerB;
    public GameObject childObj; // 子オブジェクトを参照するための変数
    public float sec = 3;
    public PlayerBC playerBScript;

    public bool playerMoveA = true;    
    public bool playerMoveB = false;
    

    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(SetSpellTrigger());
    }

    private IEnumerator SetSpellTrigger()
    {
        while (true)
        {
            anim.SetTrigger("useSpell3");
            yield return new WaitForSeconds(sec);

        }
    }

    //魔法3：PLayerのコピーを生成するメソッド（spellのイベントトリガーに設定する）
    //PlayerBをPlayerの子オブジェクトの場所に持ってくるメソッド
    public void MovePlayerBToChildPosition()
    {
        //copyAreaに障害物がある時不発にする
        if (copyAreaC.useSpellCopy || playerB.activeSelf)
        {
            if (childObj != null) // childObjが設定されているか確認します。
            {
                if (playerB.activeSelf == false)//Playerのコピーが非アクティブの時
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
    //魔法4：箱の出し入れ
    public void SpellObjInOut()
    {
        //playerの手がmoveObjに触れている時そのオブジェクトを取得する
        if (!objInOut.inMoveObj)
        {
            objInOut.TakeInMoveObj();

        }
        else if (objInOut.inMoveObj && !copyAreaC.objInArea)
        {
            objInOut.GenerateMoveObj();
        }
        else
        {
            //textReadBook = ("前にオブジェクトがあるため生成できない");
            prohibite_1.SetActive(true);

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

    // アニメーションイベントなどを使用してアニメーションの終了時に呼び出すメソッド
    public void OnSpellAnimationEnd()
    {
        isUsingSpell = false; // Spellアニメーションが終了したことを示す
        prohibite_1.SetActive(false);
        //textReadBook = "";
    }
}

