using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float startPos = 5;
    public float endPos = 58;


    void Update()
    {
        
        Vector3 playerPos = this.player.transform.position;
        if (playerPos.x < startPos)
        {
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }
        else if(playerPos.x > endPos)
        {
            transform.position = new Vector3(endPos, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(playerPos.x, transform.position.y, transform.position.z);
        }
        


    }
}
