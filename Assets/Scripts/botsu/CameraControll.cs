using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public GameObject mainCameraObj;      //メインカメラ格納用
    public GameObject subCameraObj;       //サブカメラ格納用

    public Camera mainCameraA;      //メインカメラ格納用
    public Camera subCameraA;       //サブカメラ格納用


    //単位時間ごとに実行される関数
    void Update()
    {
        
        // mainCameraのy位置を固定する
        if (mainCameraA.gameObject.activeSelf)
        {
            SetCameraYPosition(mainCameraObj);
        }
        if (subCameraA.gameObject.activeSelf)
        {
            SetCameraYPosition(subCameraObj);
        }
    }


    void SetCameraYPosition(GameObject camera)
    {
        Vector3 cameraPosition = camera.transform.position;
        cameraPosition.y = 4.5f;  // yのカメラの高さ
        camera.transform.position = cameraPosition;
    }
}


