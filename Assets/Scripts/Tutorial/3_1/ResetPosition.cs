using System.Collections;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(ResetLoop());
    }

    IEnumerator ResetLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.5f);
            transform.position = initialPosition;
        }
    }
}
