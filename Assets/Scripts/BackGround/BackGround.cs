using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Camera mainCamera;
    public Camera subCamera;
    public Camera moveCamera;

    public GameObject backgroundMain;
    public GameObject backgroundSub;
    public GameObject backgroundMove;

    void Update()
    {
        if (mainCamera.enabled)
        {
            backgroundMain.SetActive(true);
            backgroundSub.SetActive(false);
            backgroundMove.SetActive(false);
        }
        else if (subCamera.enabled)
        {
            backgroundMain.SetActive(false);
            backgroundSub.SetActive(true);
            backgroundMove.SetActive(false);
        }
        else if(moveCamera.enabled)
        {
            backgroundMain.SetActive(false);
            backgroundSub.SetActive(false);
            backgroundMove.SetActive(true);
        }
    }

    
}

//using UnityEngine;

//public class BackgroundFollow : MonoBehaviour
//{
//    public Camera mainCamera;
//    public Camera subCamera;
//    public Camera moveCamera;

//    private Vector3 offset; // 初期の差分を保存するための変数。
//    private Camera activeCamera; // 現在アクティブなカメラを参照するための変数。



//    void Start()
//    {
//        // 初期のアクティブなカメラを設定します。
//        SetActiveCamera();

//        // 初期の差分を計算して保存します。
//        offset = transform.position - activeCamera.transform.position;

//    }

//    void Update()
//    {
//        SetActiveCamera();

//        // アクティブなカメラの位置に offset を加える。
//        Vector3 newPosition = activeCamera.transform.position + offset;



//        // 背景の位置を設定します。
//        transform.position = newPosition;
//    }

//    void SetActiveCamera()
//    {
//        if (mainCamera.enabled)
//        {
//            activeCamera = mainCamera;
//        }
//        else if (subCamera.enabled)
//        {
//            activeCamera = subCamera;
//        }
//        else if (moveCamera.enabled)
//        {
//            activeCamera = moveCamera;
//        }
//    }
//}
