using TMPro;
using UnityEngine;

public class DisplayTMPTextAbove2DObject : MonoBehaviour
{
    public RectTransform canvasRectTransform;
    //public Transform PlayerPos;
    public TMP_Text textObject;
    public Vector2 offset = new Vector2(0, 0); // このオフセットはピクセル単位です。

    public Camera mainCamera;

    //private void Awake()
    //{
    //    mainCamera = Camera.main;
    //}

    private void FixedUpdate() // Update()の代わりにLateUpdate()を使用して、カメラの移動後にテキストの位置を更新します。
    {
        // 2Dオブジェクトの上の位置をスクリーン座標で計算
        Vector2 worldPos = new Vector2(this.transform.position.x + offset.x, transform.position.y + offset.y);
        Vector2 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        textObject.rectTransform.position = screenPos;


        
        textObject.text = PlayerA.TextReadBook;
    }

    public void DisplayMessage(string message)
    {
        textObject.text = message;
    }
}
