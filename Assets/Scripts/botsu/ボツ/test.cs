using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Rigidbody2D rigit2D;

    public float jumpForce = 350.0f;
    public float walkForce = 38.0f;
    public float maxWalkSpeed = 4.0f;

    void Start()
    {
        this.rigit2D = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 30;
    }

    bool pushR = false;
    public void RButtonPushDown()
    {
        pushR = true;
    }

    public void RButtonPushUp()
    {
        pushR = false;
    }

    bool pushL = false;
    public void LButtonPushDown()
    {
        pushL = true;
    }

    public void LButtonPushUp()
    {
        pushL = false;
    }

    bool tap = false;
    public void Tap()
    {
        tap = true;
    }

    void Update()
    {
        //�L�����R���L�[�{�[�h
        if (Input.GetKeyDown("a"))
        {
            pushL = true;
        }
        if (Input.GetKeyUp("a"))
        {
            pushL = false;
        }

        if (Input.GetKeyDown("d"))
        {
            pushR = true;
        }
        if (Input.GetKeyUp("d"))
        {
            pushR = false;
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetKey("w"))
        {
            tap = true;
        }


        //�W�����v
        if (tap == true && this.rigit2D.velocity.y == 0)
        {
            this.rigit2D.AddForce(transform.up * this.jumpForce);

        }
        tap = false;
        //���E�ړ�
        int key = 0;

        if (pushR)
        {
            key = 1;
        }
        if (pushL)
        {
            key = -1;
        }

        //�v���C���̑��x
        float speedx = Mathf.Abs(this.rigit2D.velocity.x);

        //�X�s�[�h����
        if (speedx < maxWalkSpeed)
        {
            this.rigit2D.AddForce(transform.right * key * this.walkForce);
        }

        //���������ɉ����Ĕ��]
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //���������̏���
        if (transform.position.y < -2)
        {
            Debug.Log("������");
            this.transform.position -= new Vector3(-3, 1, 0);
        }
    }
}
