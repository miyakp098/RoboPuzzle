using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InMoveObj : MonoBehaviour
{
    public GameObject objImage;

    private void Start()
    {
        objImage.SetActive(false);
    }

    void Update()
    {
        if (ObjInOut.InMoveObj)
        {
            objImage.SetActive(true);
        }
        else
        {
            objImage.SetActive(false);
        }
    }
}
