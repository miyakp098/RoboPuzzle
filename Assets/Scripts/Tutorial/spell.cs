using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellLargeSmall : MonoBehaviour
{
    private float speed = 2;//歩くスピード
    private float jumpForce = 400f;//ジャンプ力
    private Rigidbody2D rb2d;
    private Animator anim;//アニメーション
    public bool Large = true;


    [Header("魔法のプレハブ")] public GameObject magicShotPrefabLarge; // 弾のプレハブ
    [Header("魔法のプレハブ")] public GameObject magicShotPrefabSmall; // 弾のプレハブ


    private bool isUsingSpell = false; // Spellアニメーションを使っているかどうかを追跡
    private bool canShoot = true; // 発射可能かどうかのフラグ
    private bool playerDirectionL = false;//プレイヤーの向きが左を向いているか


    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (Large)
        {
            StartCoroutine(SetSpellTrigger());
        }else if (!Large)
        {
            StartCoroutine(SetSpellTrigger2());
        }
        
    }

    private IEnumerator SetSpellTrigger()
    {
        while (true)
        {
            anim.SetTrigger("useSpell1");
            yield return new WaitForSeconds(5.5f);
            
        }
    }

    private IEnumerator SetSpellTrigger2()
    {
        while (true)
        {
            anim.SetTrigger("useSpell2");
            yield return new WaitForSeconds(5.5f);

        }
    }


    
    // 魔法1：弾を発射するメソッド（大きくする）
    public void ShootLarge()
    {
        if (canShoot)
        {

            //発射位置
            Vector2 playerPosForword = playerDirectionL ? new Vector2(this.transform.position.x - 0.6f, this.transform.position.y + 0.85f) : new Vector2(this.transform.position.x + 0.6f, this.transform.position.y + 0.85f);
            // 弾のプレハブから新しい弾オブジェクトを生成
            GameObject magicShot = Instantiate(magicShotPrefabLarge, playerPosForword, Quaternion.identity);

            // 弾を発射する方向を設定（ここでは上向きに設定）

            magicShot.transform.right = playerDirectionL ? -transform.right : transform.right;

            // 発射後、次の発射まで一時停止する
            canShoot = false;
            // 1秒後に再び発射可能にする
            Invoke("EnableShooting", 1f);
        }
    }

    // 魔法2：弾を発射するメソッド（小さくする）
    public void ShootSmall()
    {
        if (canShoot)
        {

            //発射位置
            Vector2 playerPosForword = playerDirectionL ? new Vector2(this.transform.position.x - 0.6f, this.transform.position.y + 0.85f) : new Vector2(this.transform.position.x + 0.6f, this.transform.position.y + 0.85f);
            // 弾のプレハブから新しい弾オブジェクトを生成
            GameObject magicShot = Instantiate(magicShotPrefabSmall, playerPosForword, Quaternion.identity);

            // 弾を発射する方向を設定（ここでは上向きに設定）

            magicShot.transform.right = playerDirectionL ? -transform.right : transform.right;

            // 発射後、次の発射まで一時停止する
            canShoot = false;
            // 1秒後に再び発射可能にする
            Invoke("EnableShooting", 1f);
        }
    }


    // 発射を再び可能にするメソッド
    private void EnableShooting()
    {
        canShoot = true;
    }

    // アニメーションイベントなどを使用してアニメーションの終了時に呼び出すメソッド
    public void OnSpellAnimationEnd()
    {
        isUsingSpell = false; // Spellアニメーションが終了したことを示す

        //textReadBook = "";
    }
}
