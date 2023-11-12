using UnityEngine;

public class OverHeadMsgCreater : MonoBehaviour
{
    [SerializeField]
    RectTransform canvasRect;

    [SerializeField]
    OverHeadMsg overHeadMsgPrefab;

    OverHeadMsg overHeadMsg;

    void Start()
    {
        overHeadMsg = Instantiate(overHeadMsgPrefab, canvasRect);
        overHeadMsg.targetTran = transform;
    }
}