using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyArea : MonoBehaviour
{
    private static bool useSpellCopy;
    public static bool UseSpellCopy
    {
        get { return useSpellCopy; }
        set { useSpellCopy = value; }
    }

    private static bool objInArea = false;
    public static bool ObjInArea
    {
        get { return objInArea; }
        set { objInArea = value; }
    }


    public LayerMask groundLayer;//インスペクターからレイヤーマスクを選べるようにする
    public LayerMask objLayer;

    private bool isGround;
    private bool onObject;

    private void Update()
    {
        //足場判定       

        Vector2 groundPos = new Vector2(transform.position.x, transform.position.y);

        Vector2 groundArea = new Vector2(0.25f, 0.15f);//足場判定エリア

        Debug.DrawLine(groundPos + groundArea, groundPos - groundArea, Color.red);

        //地面の上
        isGround = Physics2D.OverlapArea(groundPos + groundArea, groundPos - groundArea, groundLayer);
        //オブジェクトの上
        onObject = Physics2D.OverlapArea(groundPos + groundArea, groundPos - groundArea, objLayer);

        if((isGround || onObject) && !objInArea)
        {
            useSpellCopy = true;
        }
        else
        {
            useSpellCopy = false;
        }

        //Debug.Log($"前に足場があるか{(isGround || onObject)}.エリアに障害物がない{!objInArea}");
    }

    //playerのコピーを出す場所に障害物があるかどうかの判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerB") || collision.gameObject.CompareTag("button"))
        {
            objInArea = false;
        }
        else
        {
            objInArea = true;
            //Debug.Log("コピー生成できない");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objInArea = false;
        //Debug.Log("コピー生成できる");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerB")||collision.gameObject.CompareTag("button"))
        {
            objInArea = false;
        }
        else
        {
            objInArea = true;
            //Debug.Log("コピー生成できない");
        }
    }
}
