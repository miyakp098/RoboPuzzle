using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DoubleBoxColliderSize : MonoBehaviour
{
    void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size *= 2;
    }
}
