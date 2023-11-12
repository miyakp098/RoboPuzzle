using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBResetPOsi : MonoBehaviour
{
    public GameObject gameObj;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = gameObj.transform.position;
        StartCoroutine(ResetLoop());
    }

    IEnumerator ResetLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.5f);
            gameObj.transform.position = initialPosition;
        }
    }
}
