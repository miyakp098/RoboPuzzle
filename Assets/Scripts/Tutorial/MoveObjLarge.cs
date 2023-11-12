using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjResize : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 originalPosition;

    private int size;

    private void Start()
    {
        originalScale = transform.localScale;
        if(originalScale.x == 1)
        {
            size = 1;
        }else if(originalScale.x == 2)
        {
            size = 2;
        }
        originalPosition = transform.position;
    }

    private void Update()
    {
        if(size == 1)
        {
            if (this.transform.localScale.x == 2)
            {
                StartCoroutine(ResetScale());
            }
        }
        if (size == 2)
        {
            if (this.transform.localScale.x == 1)
            {
                StartCoroutine(ResetScale());
            }
        }



    }

    private IEnumerator ResetScale()
    {
        yield return new WaitForSeconds(1f);
        transform.localScale = originalScale; // 1秒後に大きさを元に戻す
        transform.position = originalPosition;    // 1秒後にポジションも元に戻す
    }
}
