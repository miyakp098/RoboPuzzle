using UnityEngine;



public class pushPull : MonoBehaviour
{

    private float speed = 1.5f;//歩くスピード
    private float jumpForce = 350f;//ジャンプ力
    private Rigidbody2D rb2d;
    private Animator anim;//アニメーション
    private SpriteRenderer spriteRenderer;
    private int currentValue = 1;
    public float x1 = 30;
    public float x2 = 28;

    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (this.gameObject.transform.position.x > x1)
        {
            currentValue = -1;
        }
        else if (this.gameObject.transform.position.x < x2)
        {
            currentValue = 1;
        }
        float x = currentValue;






        //押し引きの操作とアニメーション

        {
            anim.SetBool("isCatch", true);
            if (x > 0)//引く処理
            {
                anim.SetBool("isPull", true);
            }
            else if (x < 0)//押す処理
            {
                anim.SetBool("isPush", true);
            }

            else if (x < 0)//引く処理
            {
                anim.SetBool("isPull", true);
            }
            else if (x > 0)//押す処理
            {
                anim.SetBool("isPush", true);
            }
        }
        //魔法を撃つ処理        
        {
            {
                // anim.SetTrigger();
            }
        }
    }



    //Player周りの接触判定
    private void FixedUpdate()
    {

        {
            //移動処理
            float x = currentValue;
            rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);
        }

    }




}
