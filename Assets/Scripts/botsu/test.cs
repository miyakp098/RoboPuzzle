using UnityEngine;

public class FollowMovingObject2D : MonoBehaviour
{
    private Transform movingObjectTransform;  // 動くオブジェクトのTransform
    private bool isOnTop = false;             // 上に乗っているかどうかのフラグ
    private Vector2 lastMovingObjectPosition; // 前回の動くオブジェクトの位置

    private void Update()
    {
        if (isOnTop && movingObjectTransform != null)
        {
            Vector2 offset = movingObjectTransform.position - (Vector3)lastMovingObjectPosition;
            transform.position += (Vector3)offset;
            lastMovingObjectPosition = movingObjectTransform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("moveObj")) // MovingObjectには適切なタグを設定する
        {
            isOnTop = true;
            movingObjectTransform = other.transform;
            lastMovingObjectPosition = movingObjectTransform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("moveObj"))
        {
            isOnTop = false;
            movingObjectTransform = null;
        }
    }
}
