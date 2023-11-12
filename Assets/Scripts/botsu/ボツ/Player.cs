using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;//�����X�s�[�h

    public float jumpForce = 200f;//�W�����v��

    public LayerMask groundLayer;
    public LayerMask touchLayer;//�C���X�y�N�^�[���烌�C���[�}�X�N��I�ׂ�悤�ɂ���

    private Rigidbody2D rb2d;

    private Animator amin;//�A�j���[�V����

    private SpriteRenderer spRenderer;

    private bool isGround = false;//�n�ʂ̔���

    private bool onObject = false;//���̏�ɂ��锻��

    public static bool isTouch = false;//c�𓮂����Ƃ��̐ڐG����
    float pullKeisuu = 1.0f;

    private moveFloor1 moveObj = null;

    private string pullKey = "k";//��������Ƃ��̃L�[

    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.amin = GetComponent<Animator>();
        this.spRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");//��-1�E�Ȃɂ����Ȃ�0�E�E1

        float velX = rb2d.velocity.x;
        float velY = rb2d.velocity.y;
        float maxSpeed = 5.0f;


        rb2d.AddForce(Vector2.right * x * speed * pullKeisuu);//�������ɗ͂�������

        amin.SetFloat("Speed", Mathf.Abs(x * speed));//�����A�j���[�V����

        if (velX > maxSpeed * pullKeisuu)
        {
            rb2d.velocity = new Vector2(maxSpeed * pullKeisuu, velY);
        }
        if (velX < -maxSpeed * pullKeisuu)
        {
            rb2d.velocity = new Vector2(-maxSpeed * pullKeisuu, velY);
        }


        if (Input.GetKey(pullKey) == false)//���ʂ̎�(pullKey��false�̎�)
        {
            pullKeisuu = 1.0f;
            //�X�v���C�g�̌�����ς���
            if (x < 0)
            {
                spRenderer.flipX = true;
            }
            else if (x > 0)
            {
                spRenderer.flipX = false;
            }

            //�W�����v
            if (Input.GetButtonDown("Jump") & isGround)//�n�ʂ̏�
            {
                rb2d.AddForce(Vector2.up * jumpForce);
            }
            if (Input.GetButtonDown("Jump") & onObject)//�I�u�W�F�N�g�̏�
            {
                rb2d.AddForce(Vector2.up * jumpForce);
            }
        }

        if (Input.GetKey(pullKey))//��������Ƃ��̎�(pullKey��true�̎�)
        {
            pullKeisuu = 0.5f;
        }


        //�ړ����x��ݒ�
        Vector2 addVelocity = Vector2.zero;
        if (moveObj != null)
        {
            addVelocity = moveObj.GetVelocity();
        }
        rb2d.velocity = new Vector2(velX, velY);





        //���������̏���
        if (transform.position.y < -2)
        {
            this.transform.position = new Vector2(0, 1);

        }

    }

    private void FixedUpdate()
    {
        //���ꔻ��       

        Vector2 groundPos =
            new Vector2(
                transform.position.x,
                transform.position.y
            );

        Vector2 groundArea = new Vector2(0.3f, 0.2f);//���ꔻ��G���A

        Debug.DrawLine(groundPos + groundArea, groundPos - groundArea, Color.red);

        //�n�ʂ̏�
        isGround =
            Physics2D.OverlapArea(
                groundPos + groundArea,
                groundPos - groundArea,
                groundLayer
            );
        //�I�u�W�F�N�g�̏�
        onObject =
            Physics2D.OverlapArea(
                groundPos + groundArea,
                groundPos - groundArea,
                touchLayer
            );

        //Debug.Log(isGround);


        //���ݔ���
        if (spRenderer.flipX == false)
        {
            Vector2 touchPos =
            new Vector2(
                transform.position.x + 0.45f,
                transform.position.y + 0.5f
            );
            Vector2 touchArea = new Vector2(0.2f, 0.5f);//���ݔ���G���A

            Debug.DrawLine(touchPos + touchArea, touchPos - touchArea, Color.blue);

            isTouch =
                Physics2D.OverlapArea(
                    touchPos + touchArea,
                    touchPos - touchArea,
                    touchLayer
                );
        }
        else if (spRenderer.flipX == true)
        {
            Vector2 touchPos =
            new Vector2(
                transform.position.x - 0.55f,
                transform.position.y + 0.5f
            );
            Vector2 touchArea = new Vector2(0.2f, 0.5f);//���ݔ���G���A

            Debug.DrawLine(touchPos + touchArea, touchPos - touchArea, Color.blue);

            isTouch =
                Physics2D.OverlapArea(
                    touchPos + touchArea,
                    touchPos - touchArea,
                    touchLayer
                );
        }

        

        //Debug.Log(isTouch);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "MoveFloor")
        {
            //���������痣�ꂽ
            moveObj = null;
        }
    }
}
