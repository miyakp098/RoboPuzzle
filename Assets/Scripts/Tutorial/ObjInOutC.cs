using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInOutC : MonoBehaviour
{
    public GameObject prefabMoveObj;//箱のプレハブ

    private GameObject toMoveObj = null;

    private GameObject storageMoveObj = null;

    public bool inMoveObj = false;
    public GameObject prohibite_1;

    private static bool moveObjInArea = false;
    public static bool MoveObjInArea
    {
        get { return moveObjInArea; }
        set { moveObjInArea = value; }
    }

    //大きさを変更するスクリプト
    private float lerpSpeed = 5f; // 補間の速度。この値を変更して、スケール変更の速さを調整することができる
    private Vector3 initialScale;  // 初期のスケール値
    private Vector3 targetScale;   // スケール値（目標）
    private bool isScaling = false;  // スケーリング中かどうかを示すフラグ


    private float speed = 0.5f;  // 移動速度

    private Rigidbody2D rb2d;
    private Rigidbody2D rb2dCheck;

    private static string textReadBook = "";
    public static string TextReadBook
    {
        get { return textReadBook; }
        set { textReadBook = value; }
    }

    

    private void Update()
    {
        if (storageMoveObj != null)
        {
            //大きさを変える
            if (isScaling)
            {

                //このスクリプトにアタッチしているオブジェクトの中心に移動
                Vector3 targetPos = transform.position;
                Vector3 direction = (targetPos - storageMoveObj.transform.position).normalized;
                Vector3 moveSpeed = direction * speed * Time.deltaTime;

                float proximityThreshold = 0.01f;
                if ((targetPos - storageMoveObj.transform.position).sqrMagnitude > moveSpeed.sqrMagnitude + proximityThreshold)
                {
                    storageMoveObj.transform.position += moveSpeed;
                }
                else
                {
                    //storageMoveObj.transform.position = targetPos;
                }



                // 現在のスケールと目標のスケールとの間で補間する
                storageMoveObj.transform.localScale = Vector3.Lerp(storageMoveObj.transform.localScale, targetScale, lerpSpeed * Time.deltaTime);

                // 十分に近づいたらスケーリングを停止する
                if (Vector3.Distance(storageMoveObj.transform.localScale, targetScale) < 0.01f)
                {
                    storageMoveObj.transform.localScale = targetScale;
                    isScaling = false;
                    if (storageMoveObj.transform.localScale.x < 0.2f)
                    {
                        //storageMoveObj.SetActive(false);
                    }
                    else
                    {
                        //storageMoveObj.SetActive(true);

                        if (rb2d != null)
                        {
                            rb2d.isKinematic = false;
                        }


                        storageMoveObj = null;
                        return;
                    }
                    //storageMoveObj.SetActive(!storageMoveObj.activeSelf);//bool値
                }
            }

            if (storageMoveObj.transform.localScale.x < 0.2f)
            {
                storageMoveObj.SetActive(false);
                storageMoveObj.transform.position = transform.position;
            }
            else
            {
                storageMoveObj.SetActive(true);
            }
        }

    }

    //playerのコピーを出す場所に障害物があるかどうかの判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //moveObjがエリアにある時
        if (collision.gameObject.CompareTag("moveObj"))
        {
            toMoveObj = collision.gameObject;
            moveObjInArea = true;
            rb2dCheck = collision.GetComponent<Rigidbody2D>();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("moveObj"))
        {
            toMoveObj = null;
            moveObjInArea = false;
        }
    }

    //箱を取り入れる
    public void TakeInMoveObj()
    {
        if (toMoveObj != null && !inMoveObj)//箱があるかどうか
        {
            //Debug.Log(toMoveObj.transform.localScale.x);
            // 箱が大きなったら取り込めない
            if (toMoveObj.transform.localScale.x <= 1.3f)
            {
                if (rb2dCheck.mass == 1)
                {
                    storageMoveObj = toMoveObj;

                    toMoveObj = null;
                    foreach (Transform firstLevelChild in storageMoveObj.transform)
                    {
                        foreach (Transform secondLevelChild in firstLevelChild)
                        {
                            secondLevelChild.SetParent(null);
                        }
                    }

                    initialScale = storageMoveObj.transform.localScale;// 取り入れ時のスケール値の記憶
                    rb2d = storageMoveObj.GetComponent<Rigidbody2D>();// 取り入れ時のrb2dの記憶
                    if (rb2d != null)
                    {
                        rb2d.isKinematic = true;
                    }
                    ZeroSize();
                    //storageMoveObj.SetActive(false);//bool値
                    //objectToDestroy = null;
                    inMoveObj = true;
                }
                else
                {
                    prohibite_1.SetActive(true);
                    Debug.Log("箱が重なっているため取り込めない");
                }

            }
            else
            {
                textReadBook = ("箱が大きすぎて取り込めない");
                prohibite_1.SetActive(true);
            }

        }
        else
        {
            textReadBook = ("オブジェクトなし");
            prohibite_1.SetActive(true);
        }



    }

    //箱を出す
    public void GenerateMoveObj()
    {
        if (!storageMoveObj.activeSelf)
        {
            storageMoveObj.transform.position = this.transform.position;

            NormalSize();

            inMoveObj = false;
            //storageMoveObj = null;
        }
    }

    // この関数を呼び出すことで、スケールを徐々に1倍にします
    private void NormalSize()
    {
        targetScale = initialScale * 1f;
        isScaling = true;

    }

    // この関数を呼び出すことで、スケールを徐々に0倍にします
    private void ZeroSize()
    {

        targetScale = initialScale * 0.1f;
        isScaling = true;

    }



}
