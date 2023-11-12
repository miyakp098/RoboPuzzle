using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject mainCameraObj;      //メインカメラ格納用
    public GameObject subCameraObj;       //サブカメラ格納用

    public Camera mainCamera;
    public Camera subCamera;
    public Camera moveCamera;

    public float timePerUnit = 0.5f; // 1ユニットあたりの時間
    private float transitionTime; // 実際の移行にかかる時間

    private bool isMainActive = true; // 現在のカメラの状態
    private bool transitioning = false;

    private void Start()
    {
        mainCamera.enabled = true;
        subCamera.enabled = false;
        moveCamera.enabled = false;
    }
    
    


    public void SwitchCameras()
    {
        if (!transitioning)
        {
            if (isMainActive)
            {
                transitionTime = Vector3.Distance(mainCamera.transform.position, subCamera.transform.position) * timePerUnit;
                StartCoroutine(TransitionFromMainToSub());
            }
            else
            {
                transitionTime = Vector3.Distance(subCamera.transform.position, mainCamera.transform.position) * timePerUnit;
                StartCoroutine(TransitionFromSubToMain());
            }
        }
    }

    IEnumerator TransitionFromMainToSub()
    {
        transitioning = true;

        moveCamera.transform.position = mainCamera.transform.position;
        moveCamera.transform.rotation = mainCamera.transform.rotation;
        moveCamera.enabled = true;
        mainCamera.enabled = false;

        float elapsedTime = 0.0f;

        while (elapsedTime < transitionTime)
        {
            moveCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, subCamera.transform.position, elapsedTime / transitionTime);
            moveCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, subCamera.transform.rotation, elapsedTime / transitionTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        subCamera.enabled = true;
        moveCamera.enabled = false;
        isMainActive = false;
        transitioning = false;
    }

    IEnumerator TransitionFromSubToMain()
    {
        transitioning = true;

        moveCamera.transform.position = subCamera.transform.position;
        moveCamera.transform.rotation = subCamera.transform.rotation;
        moveCamera.enabled = true;
        subCamera.enabled = false;

        float elapsedTime = 0.0f;

        while (elapsedTime < transitionTime)
        {
            moveCamera.transform.position = Vector3.Lerp(subCamera.transform.position, mainCamera.transform.position, elapsedTime / transitionTime);
            moveCamera.transform.rotation = Quaternion.Lerp(subCamera.transform.rotation, mainCamera.transform.rotation, elapsedTime / transitionTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.enabled = true;
        moveCamera.enabled = false;
        isMainActive = true;
        transitioning = false;
    }

    void SetCameraYPosition(GameObject camera)
    {
        Vector3 cameraPosition = camera.transform.position;
        cameraPosition.y = 4.5f;  // yのカメラの高さ
        camera.transform.position = cameraPosition;
    }

}

