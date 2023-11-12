using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightFloor : MonoBehaviour
{
    private List<Rigidbody2D> objectsOnPlatform = new List<Rigidbody2D>();
    private float totalMass = 0f;

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
    private bool isMoving = false;
    private bool onPlayer = false;
    private bool isDelayCoroutineRunning = false;

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
        //Debug.Log($"{totalMass},{partonerFloor.TotalMassR}");
        // 現在の位置が目的地と同じでない場合、isMovingをtrueにする
        if (transform.position != targetPosition)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        
        this.transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        partnerObj.transform.position = Vector3.MoveTowards(partnerObj.transform.position, targetPosition2, speed * Time.deltaTime);

        if (isMoving && (onPlayer || partonerFloor.onPlayer))
        {
            PlayerA.Speed = 0f;
            PlayerA.JumpForce = 0f;

            PlayerB.Speed = 0f;
            PlayerB.JumpForce = 0f;
        }
        else
        {
            PlayerA.Speed = 4f;
            PlayerA.JumpForce = 400f;

            PlayerB.Speed = 4f;
            PlayerB.JumpForce = 400f;
        }
        // 床を目的地へ移動
        if (totalMass == partonerFloor.TotalMassR)
        {
            targetPosition = posMiddle;//中間位置に移動
            targetPosition2 = posMiddle2;
        }
        else if (totalMass <= partonerFloor.TotalMassR)
        {
            targetPosition = posUp; // onCountが1以下になったら上に移動
            targetPosition2 = posDown2;
        }
        else if (totalMass > partonerFloor.TotalMassR)
        {
            targetPosition = posDown; // onCountが1以上になったら下に移動
            targetPosition2 = posUp2;
        }

        // 遅延してMoveTowardsを呼び出す
        //if (!isDelayCoroutineRunning)
        //{
        //    StartCoroutine(DelayedMove());
        //}
    }

    IEnumerator DelayedMove()
    {
        isDelayCoroutineRunning = true;

        yield return new WaitForSeconds(0.1f);

        // 床を目的地へ移動
        if (totalMass == partonerFloor.TotalMassR)
        {            
            targetPosition = posMiddle;//中間位置に移動
            targetPosition2 = posMiddle2;
        }
        else if (totalMass <= partonerFloor.TotalMassR)
        {
            targetPosition = posUp; // onCountが1以下になったら上に移動
            targetPosition2 = posDown2;
        }
        else if (totalMass > partonerFloor.TotalMassR)
        {
            targetPosition = posDown; // onCountが1以上になったら下に移動
            targetPosition2 = posUp2;
        }

        isDelayCoroutineRunning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerB"))//Playerタグに当たったとき
        {
            onPlayer = true;
            collision.transform.SetParent(transform);
            //乗ったオブジェクトのrigidbodyを取得する
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null && !objectsOnPlatform.Contains(rb))
            {
                objectsOnPlatform.Add(rb);
                totalMass += rb.mass;

                // MassWatcherがあれば、そのイベントに購読します
                MassWatcher watcher = collision.gameObject.GetComponent<MassWatcher>();
                if (watcher != null)
                {
                    watcher.OnMassChanged.AddListener(HandleMassChange);
                }
            }

        }
        if (collision.gameObject.CompareTag("moveObj"))//moveObjタグに当たったとき
        {
            collision.transform.SetParent(transform);

            //乗ったオブジェクトのrigidbodyを取得する
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null && !objectsOnPlatform.Contains(rb))
            {
                objectsOnPlatform.Add(rb);
                totalMass += rb.mass;

                // MassWatcherがあれば、そのイベントに購読します
                MassWatcher watcher = collision.gameObject.GetComponent<MassWatcher>();
                if (watcher != null)
                {
                    watcher.OnMassChanged.AddListener(HandleMassChange);
                }
            }


        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerB"))// Playerタグに当たったとき
        {
            onPlayer = false;
            collision.transform.parent = null;
            ///乗ったオブジェクトのrigidbodyを破棄する
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null && objectsOnPlatform.Contains(rb))
            {
                objectsOnPlatform.Remove(rb);
                totalMass -= rb.mass;

                // MassWatcherがあれば、そのイベントの購読を解除します
                MassWatcher watcher = collision.gameObject.GetComponent<MassWatcher>();
                if (watcher != null)
                {
                    watcher.OnMassChanged.RemoveListener(HandleMassChange);
                }
            }
        }
        if (collision.gameObject.CompareTag("moveObj"))//moveObjタグに当たったとき
        {

            collision.transform.parent = null;

            ///乗ったオブジェクトのrigidbodyを破棄する
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null && objectsOnPlatform.Contains(rb))
            {
                objectsOnPlatform.Remove(rb);
                totalMass -= rb.mass;

                // MassWatcherがあれば、そのイベントの購読を解除します
                MassWatcher watcher = collision.gameObject.GetComponent<MassWatcher>();
                if (watcher != null)
                {
                    watcher.OnMassChanged.RemoveListener(HandleMassChange);
                }
            }

        }
        
    }

    // 質量が変化したときの処理
    private void HandleMassChange(float change)
    {
        totalMass += change;
    }
}
