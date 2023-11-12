using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorMoveByWeight : MonoBehaviour
{
    [Header("床の移動速度")] public float speed = 2f; // 床の移動速度
    [Header("床のY方向の移動幅")] public float widthY = 2f;
    public partnerFloor partonerFloor;

    private Vector3 posUp;   // 上の位置
    private Vector3 posMiddle;  // 中間位置
    private Vector3 posDown;     // 下の位置
    private Vector3 targetPosition;

    private Vector3 posUp2;   // 上の位置
    private Vector3 posMiddle2;  // 中間位置
    private Vector3 posDown2;     // 下の位置
    private Vector3 targetPosition2;
    //[Header("Playerオブジェクトを入れる")] public GameObject player;// Playerオブジェクトを入れる
    [Header("相方のオブジェクトを入れる")] public GameObject partnerObj;// オブジェクトを入れる

    private int onCountL = 0;//左に乗っているオブジェクトの数 



    private void Start()
    {
        targetPosition = this.transform.position;

        posUp = this.transform.position + Vector3.up * widthY;
        posMiddle = this.transform.position;
        posDown = this.transform.position - Vector3.up * widthY;

        //相方のオブジェクト
        targetPosition2 = partnerObj.transform.position;
        posUp2 = partnerObj.transform.position + Vector3.up * widthY;
        posMiddle2 = partnerObj.transform.position;
        posDown2 = partnerObj.transform.position - Vector3.up * widthY;
    }

    private void Update()
    {
        
        

        // 床を目的地へ移動
        if (onCountL == partonerFloor.TotalMassR)
        {
            targetPosition = posMiddle;//中間位置に移動
            targetPosition2 = posMiddle2;
        }
        else if (onCountL <= partonerFloor.TotalMassR)
        {
            targetPosition = posUp; // onCountが1以下になったら上に移動
            targetPosition2 = posDown2;
        }
        else if (onCountL > partonerFloor.TotalMassR)
        {
            targetPosition = posDown; // onCountが1以上になったら下に移動
            targetPosition2 = posUp2;
        }

        //Debug.Log(partonerFloor.OnCount);


        this.transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        partnerObj.transform.position = Vector3.MoveTowards(partnerObj.transform.position, targetPosition2, speed * Time.deltaTime);

        
    }

    // オブジェクトが床の上に乗った時に呼ばれる
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            //player.gameObject.transform.parent = this.gameObject.transform;
            //onCountL += 2;
            collision.transform.SetParent(transform);
            
        }
        if (collision.gameObject.CompareTag("moveObj"))
        {
            collision.transform.SetParent(transform);
            onCountL += 1;

            

        }
        
        
    }

    // オブジェクトが床の上に離れた時に呼ばれる 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
            //player.gameObject.transform.parent = null;

            //onCountL -= 2;
        }
        if (collision.gameObject.CompareTag("moveObj"))
        {
            
            collision.transform.parent = null;

            
            
            onCountL -= 1;
        }
    }
    
}
