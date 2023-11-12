using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObjTrigger2 : MonoBehaviour
{
    private List<Rigidbody2D> objectsOnPlatform = new List<Rigidbody2D>();
    

    public bool isActive = false;
    public bool colorChange = false;
    private SpriteRenderer spriteRenderer;
    Color spriteColor = new Color32(230, 230, 230, 255);

    private float totalMass = 0f;//乗っているオブジェクトの数
    public float TotalMass
    {
        get { return totalMass; }
        set { totalMass = value; }
    }

    private bool onMoveObj = false;//上にオブジェクトが載っているかどうか
    public bool OnMoveObj
    {
        get { return onMoveObj; }
        set { onMoveObj = value; }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
            if ((collision.gameObject.CompareTag("Player") && collision is BoxCollider2D) || (collision.gameObject.CompareTag("PlayerB") && collision is BoxCollider2D))
            {

                collision.gameObject.transform.SetParent(this.transform);
                //this.transform.SetParent(collision.gameObject.transform);
                onMoveObj = true;
                //Debug.Log($"上にオブジェクトが乗っている,{this.gameObject.name}");

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

            if (collision.gameObject.CompareTag("moveObj"))
            {
                collision.gameObject.transform.SetParent(this.transform);
                //this.transform.SetParent(collision.gameObject.transform);
                onMoveObj = true;
                //Debug.Log($"上にオブジェクトが乗っている,{this.gameObject.name}");

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
                if (colorChange)
                {
                    spriteRenderer = collision.GetComponent<SpriteRenderer>();
                    spriteRenderer.color = spriteColor;
                }

            }
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive)
        {
            if ((collision.gameObject.CompareTag("Player") && collision is BoxCollider2D) || (collision.gameObject.CompareTag("PlayerB") && collision is BoxCollider2D))
            {

                collision.gameObject.transform.SetParent(this.transform);
                onMoveObj = true;

            }

            if (collision.gameObject.CompareTag("moveObj"))
            {
                collision.gameObject.transform.SetParent(this.transform);
                onMoveObj = true;

                if (colorChange)
                {
                    spriteRenderer = collision.GetComponent<SpriteRenderer>();
                    spriteRenderer.color = spriteColor;
                }
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") && collision is BoxCollider2D) || (collision.gameObject.CompareTag("PlayerB") && collision is BoxCollider2D))// Playerタグに当たったとき
        {
            collision.gameObject.transform.SetParent(null);
            //this.transform.SetParent(null);
            onMoveObj = false;
            //Debug.Log("上にオブジェクトが乗っていない");

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

        if (collision.gameObject.CompareTag("moveObj"))
        {
            collision.gameObject.transform.SetParent(null);
            //this.transform.SetParent(null);
            onMoveObj = false;
            //Debug.Log("上にオブジェクトが乗っていない");

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
            if (colorChange)
            {
                spriteRenderer = collision.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color32(255, 255, 255, 255);
            }
        }

    }

    //子オブジェクトを全て親オブジェクトから解除する
    public void DetachAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.SetParent(null);
        }
        onMoveObj = false;
    }

    // 質量が変化したときの処理
    private void HandleMassChange(float change)
    {
        totalMass += change;
    }
}
