using System.Collections;
using UnityEngine;



public class idou : MonoBehaviour
{
    
    private float speed = 2;//歩くスピード
    private float jumpForce = 400f;//ジャンプ力
    private Rigidbody2D rb2d;    
    private Animator anim;//アニメーション
    public float x1 = 1;
    public float x2 = -1;

    private int currentValue = 1;

    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    
    void Update()
    {
        if(this.gameObject.transform.position.x > x1)
        {
            currentValue = -1;
        }
        else if(this.gameObject.transform.position.x < x2)
        {
            currentValue = 1;
        }
        float x = currentValue;

        //移動処理
        {

            
            {

                //スプライトの向きを変える
                if (x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    
                }
                else if (x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    
                }

                
            }

            //アニメーションの処理
            anim.SetFloat("Speed", Mathf.Abs(x * speed));//歩くアニメーション

        }
        
    }

    

    //Player周りの接触判定
    private void FixedUpdate()
    {
        
        {
            //移動処理
            float x = currentValue;//左-1・なにもしない0・右1
            rb2d.velocity = new Vector2(x * speed , rb2d.velocity.y);
        }

    }

    
    

}
