using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookWithPlayer : MonoBehaviour
{
    public Transform playerTransform; // プレイヤーのTransform
    private Rigidbody2D _playerRigidbody;
    public float followSpeed = 1.0f;  // どれくらいスムーズに追尾するか

    

    Vector2 helpPos;

    private bool oneExe = false;

    private BoxCollider2D _boxCollider;

    private Animator anim;//アニメーション

    private void Start()
    {
        if (playerTransform != null)
        {
            _playerRigidbody = playerTransform.GetComponent<Rigidbody2D>();
            if (_playerRigidbody == null)
            {
                Debug.LogWarning("Rigidbody 2D not found on the player.");
            }
        }
        else
        {
            Debug.LogWarning("Player Transform is not set.");
        }

        _boxCollider = GetComponent<BoxCollider2D>();
        if (_boxCollider == null)
        {
            Debug.LogWarning("Box Collider not found on this object.");
        }

        this.anim = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        
        //Debug.Log(playerTransform.position.x - this.transform.position.x);
        if (playerTransform != null)
        {
            //PlayerのY方向のスピード
            float velY = _playerRigidbody.velocity.y;

            //PlayerAの頭上に箱の判定があるかつPLayerAのYのスピードが0の時
            if (BoxOnHedA.HelpPlayerA && velY == 0)
            {
                
                if (!oneExe)//一度だけ実行
                {
                    //Playerの頭上に本を移動、コライダーをオン、本を横のアニメーションに変更
                    helpPos = new Vector2(BoxOnHedA.OnBovPos.x, playerTransform.position.y + 1.75f);
                    
                    if (_boxCollider != null)
                    {
                        _boxCollider.enabled = true;
                    }
                    anim.SetBool("BoxOnHed", true);
                    oneExe = true;
                    //本のポジションをPlayerの頭上に維持
                    this.transform.position = helpPos;
                }
                

                //箱が本の上に乗った時、少し上に持ち上げる 
                Vector2 upPosition = new Vector2(helpPos.x, helpPos.y + 0.5f);// Lerp関数を使って現在の位置から計算した位置へスムーズに移動
                Vector2 newPosition = Vector2.Lerp(this.transform.position, upPosition, 2f * Time.deltaTime);
                transform.position = newPosition;

                if (Mathf.Abs(playerTransform.position.x - this.transform.position.x) > 0.7f)
                {
                    BoxOnHedA.HelpPlayerA = false;
                    oneExe = false;
                    anim.SetBool("BoxOnHed", false);
                }

            }
            else if (BoxOnHedA.HelpPlayerA)
            {
                //箱が本の上に乗った時、少し上に持ち上げる 
                Vector2 upPosition = new Vector2(helpPos.x, helpPos.y + 0.5f);// Lerp関数を使って現在の位置から計算した位置へスムーズに移動
                Vector2 newPosition = Vector2.Lerp(this.transform.position, upPosition, 2f * Time.deltaTime);
                transform.position = newPosition;

                if (Mathf.Abs(playerTransform.position.x - this.transform.position.x) > 0.7f)
                {
                    BoxOnHedA.HelpPlayerA = false;
                    oneExe = false;
                    anim.SetBool("BoxOnHed", false);
                }
            }
            else
            {
                if (_boxCollider != null)
                {
                    _boxCollider.enabled = false;
                }
                Vector2 targetPosition;

                // プレイヤーのスケールが-1の場合
                if (playerTransform.localScale.x < 0)
                {
                    // (x+1, y+2) の位置を計算
                    targetPosition = new Vector2(playerTransform.position.x + 1, playerTransform.position.y + 2);
                }
                else
                {
                    // プレイヤーの位置の斜め上 (x-1, y+2) の位置を計算
                    targetPosition = new Vector2(playerTransform.position.x - 1, playerTransform.position.y + 2);
                }

                // Lerp関数を使って現在の位置から計算した位置へスムーズに移動
                Vector2 newPosition = Vector2.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
                transform.position = newPosition;
            }
                
        }
    }
}
