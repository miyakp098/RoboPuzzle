using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera mainCamera;
    public Camera subCamera;
    public Camera moveCamera;

    public float moveRate; // 背景の移動速度の係数（0.5ならカメラの半分の速度で移動）
    private Vector3 lastCameraPosition; // 最後にカメラがいた位置

    void Start()
    {
        
        lastCameraPosition = mainCamera.transform.position;
    }

    void FixedUpdate()
    {
        if (mainCamera.enabled)
        {
            Vector3 deltaMovement = mainCamera.transform.position - lastCameraPosition;
            transform.position += new Vector3(deltaMovement.x * moveRate, deltaMovement.y * moveRate);
            lastCameraPosition = mainCamera.transform.position;
        }
        else if (subCamera.enabled)
        {
            Vector3 deltaMovement = subCamera.transform.position - lastCameraPosition;
            transform.position += new Vector3(deltaMovement.x * moveRate, deltaMovement.y * moveRate);
            lastCameraPosition = subCamera.transform.position;
        }
        else if (moveCamera.enabled)
        {
            Vector3 deltaMovement = moveCamera.transform.position - lastCameraPosition;
            transform.position += new Vector3(deltaMovement.x * moveRate, deltaMovement.y * moveRate);
            lastCameraPosition = moveCamera.transform.position;
        }
       
    }
}
