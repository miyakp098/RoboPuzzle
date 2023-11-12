using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell4 : MonoBehaviour
{
    private float speed = 2;//歩くスピード
    private float jumpForce = 400f;//ジャンプ力
    private Rigidbody2D rb2d;
    private Animator anim;//アニメーション
    public ObjInOutC objInOut;//ObjInOutスクリプト
    public CopyAreaC copyAreaC;//ObjInOutスクリプト
    private bool isUsingSpell = false; // Spellアニメーションを使っているかどうかを追跡
    public GameObject prohibite_1;//バツマーク


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
            anim.SetTrigger("useSpell4");
            yield return new WaitForSeconds(4.0f);

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
    // アニメーションイベントなどを使用してアニメーションの終了時に呼び出すメソッド
    public void OnSpellAnimationEnd()
    {
        isUsingSpell = false; // Spellアニメーションが終了したことを示す
        prohibite_1.SetActive(false);
        //textReadBook = "";
    }
}
