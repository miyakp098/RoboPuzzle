using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFloor3 : MonoBehaviour
{
    [Header("����")] public float speed = 2.0f;
    [Header("��(-1,0,+1)")] public int side = 1;
    [Header("�c(-1,0,+1)")] public int vertical = 0;
    public GameObject player;

    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    

    private void FixedUpdate()
    {
        float sin = Mathf.Sin(Time.time);
        if (rb != null)
        {
            Vector2 targetPoint = new Vector2(this.transform.position.x + 100 * side, this.transform.position.y + 100 * vertical);
            //���ݒn���玟�̃|�C���g�ւ̃x�N�g�����쐬
            Vector2 toVector = Vector2.MoveTowards(this.transform.position, targetPoint, sin * speed * Time.deltaTime);

            //���̃|�C���g�ֈړ�
            rb.MovePosition(toVector);

                        
        }
        

    }

    
}
