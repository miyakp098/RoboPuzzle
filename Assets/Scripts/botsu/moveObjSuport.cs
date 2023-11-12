using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObjSuport : MonoBehaviour
{

    public GameObject moveObject;

    
    
    void Update()
    {
        Vector3 moveObj = moveObject.transform.position;

        this.transform.position = new Vector2(moveObj.x, moveObj.y);
    }
}
