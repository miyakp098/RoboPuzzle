using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partnerFloor : MonoBehaviour
{
    private List<Rigidbody2D> objectsOnPlatform = new List<Rigidbody2D>();
    

    //[Header("Playerオブジェクトを入れる")] public GameObject player;// Playerオブジェクトを入れる
    private float totalMass = 0f;//左に乗っているオブジェクトの数    
    public float TotalMassR
    {
        get { return totalMass; }
        set { totalMass = value; }
    }

    public bool onPlayer = false;

    // オブジェクトが床の上に乗った時に呼ばれる
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerB"))
        {
            onPlayer = true;
            collision.transform.SetParent(transform);
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
            collision.transform.SetParent(transform);

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

    // オブジェクトが床の上に離れた時に呼ばれる 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerB"))
        {
            onPlayer = false;
            collision.transform.parent = null;
            
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

            collision.transform.parent = null;
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
