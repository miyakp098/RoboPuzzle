using UnityEngine;

public class OverHeadMsg : MonoBehaviour
{
    public Transform targetTran;

    void Update()
    {
        transform.position = RectTransformUtility.WorldToScreenPoint(
             Camera.main,
             targetTran.position + Vector3.up);
    }
}
