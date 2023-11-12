using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerLerp : MonoBehaviour
{
    public GameObject playerA;
    public GameObject playerB;

    // カメラが移動する目的の位置
    private Vector3 targetPosition;

    // カメラの移動速度を調整するための変数。数値を変えることで移動の速さを変更できます。
    public float lerpSpeed = 5.0f;

    private void Start()
    {
        // カメラの初期位置を設定
        if (playerA)
        {
            
            targetPosition = new Vector3(playerA.transform.position.x, transform.position.y, transform.position.z);
            transform.position = targetPosition;
        }
    }

    void FixedUpdate()
    {
        if (playerB.activeInHierarchy && !PlayerA.PlayerMoveA && PlayerA.PlayerMoveB)
        {
            targetPosition = new Vector3(playerB.transform.position.x, transform.position.y, transform.position.z);
            //transform.position = targetPosition;
        }
        else
        {
            targetPosition = new Vector3(playerA.transform.position.x, transform.position.y, transform.position.z);
            //transform.position = targetPosition;
        }

        // カメラの位置をスムーズに目的の位置に移動させる
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }
}
