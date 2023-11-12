using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenDirX : MonoBehaviour
{
    public GameObject openObj;
    public GameObject buttonObj;

    // オブジェクトに他のオブジェクトが触れているかどうかを保持する変数
    private bool isCollidingPlayer = false;
    private bool isCollidingMoveObj = false;

    //SE
    public AudioClip clip;
    private bool hasPlayedAudio = false; // 追加: SEを再生したかのフラグ


    //スタート時、moveObjのY座標、高さを取得する    
    float openObjX, openObjHeight;
    void Start()
    {
        openObjX = openObj.transform.position.x;//moveObjのY座標
        openObjHeight = 2.5f; //moveObjの高さ
    }


    void Update()
    {

        if (isCollidingPlayer || isCollidingMoveObj)//isCollidingPlayerまたはisCollidingMoveObjがtrueのとき
        {
            // 追加: SEを再生していない場合に再生
            if (!hasPlayedAudio)
            {
                GameManager.instance.PlaySE(clip);
                hasPlayedAudio = true; // フラグを更新
            }
            //openObjの現在のY座標がopenObjのスタート時のY座標＋openObjのYの高さより小さいとき
            if (openObj.transform.position.x < openObjX + openObjHeight)
            {
                openObj.transform.position += new Vector3(3f,0) * Time.deltaTime;//openObjの処理

                //buttonの処理
                buttonObj.transform.localScale = new Vector3(1, 0.4f, 1);
                //this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x,buttonY - buttonHeight * 0.5f);//buttonの処理
            }

        }
        else if (isCollidingPlayer == false || isCollidingMoveObj == false)//isCollidingPlayerまたはisCollidingMoveObjがfalseのとき
        {
            if (openObj.transform.position.x > openObjX)
            {
                openObj.transform.position -= new Vector3(3f, 0) * Time.deltaTime;

                //buttonの処理
                buttonObj.transform.localScale = new Vector3(1, 1, 1);
                //this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x,buttonY);
            }
            hasPlayedAudio = false; // フラグをリセット
        }



    }

    // 他のオブジェクトがbuttonオブジェクトに触れたときに呼び出されるメソッド
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerB"))
        {
            isCollidingPlayer = true;
        }
        if (collision.gameObject.CompareTag("moveObj"))
        {
            isCollidingMoveObj = true;
        }
    }




    // 他のオブジェクトがbuttonオブジェクトから離れたときに呼び出されるメソッド
    private void OnTriggerExit2D(Collider2D collision)
    {
        //オブジェクトのタグがPlayerまたは、moveObjのとき
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerB"))
        {
            isCollidingPlayer = false;
        }
        if (collision.gameObject.CompareTag("moveObj"))
        {
            isCollidingMoveObj = false;
        }
    }
}
