using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMoveFloor : MonoBehaviour
{
    public GameObject moveFloor;
    public GameObject buttonObj;
    public float speed = 3.0f;  // 移動速度

    private bool isCollidingPlayerA = false;
    private bool isCollidingPlayerB = false;

    private bool isCollidingMoveObj = false;

    private bool hasPlayedAudio = false; // 追加: SEを再生したかのフラグ

    [Header("床のX方向の移動幅")] public float widthX = 3f;
    [Header("床のY方向の移動幅")] public float widthY = 0f;

    private Vector3 startPos;
    private Vector3 endPos;

    //SE
    public AudioClip clip;

    void Start()
    {
        startPos = moveFloor.transform.position;
        endPos = moveFloor.transform.position + Vector3.right * widthX + Vector3.up * widthY;
    }

    void Update()
    {
        Vector3 targetPos = (isCollidingPlayerB || isCollidingPlayerA || isCollidingMoveObj) ? endPos : startPos;
        Vector3 direction = (targetPos - moveFloor.transform.position).normalized;
        Vector3 moveSpeed = direction * speed * Time.deltaTime;

        float proximityThreshold = 0.01f;
        if ((targetPos - moveFloor.transform.position).sqrMagnitude > moveSpeed.sqrMagnitude + proximityThreshold)
        {
            moveFloor.transform.position += moveSpeed;
        }
        else
        {
            moveFloor.transform.position = targetPos;
        }

        if (isCollidingPlayerB || isCollidingPlayerA || isCollidingMoveObj)
        {
            //buttonの処理
            buttonObj.transform.localScale = new Vector3(1, 0.5f, 1);

            // 追加: SEを再生していない場合に再生
            if (!hasPlayedAudio)
            {
                GameManager.instance.PlaySE(clip);
                hasPlayedAudio = true; // フラグを更新
            }
        }
        else if (!isCollidingPlayerB && !isCollidingPlayerA && !isCollidingMoveObj)
        {
            //buttonの処理
            buttonObj.transform.localScale = new Vector3(1, 1, 1);
            hasPlayedAudio = false; // フラグをリセット
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingPlayerA = true;
        }
        if (collision.gameObject.CompareTag("PlayerB"))
        {
            isCollidingPlayerB = true;
        }
        if (collision.gameObject.CompareTag("moveObj"))
        {
            isCollidingMoveObj = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingPlayerA = false;
        }
        if (collision.gameObject.CompareTag("PlayerB"))
        {
            isCollidingPlayerB = false;
        }
        if (collision.gameObject.CompareTag("moveObj"))
        {
            isCollidingMoveObj = false;
        }
    }
}
