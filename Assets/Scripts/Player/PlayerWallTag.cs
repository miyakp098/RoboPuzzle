using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallTag : MonoBehaviour
{
    public GameObject playerAWall;
    public GameObject playerBWall;


    void Update()
    {
        if (PlayerA.PlayerMoveA)
        {
            playerAWall.SetActive(false);
            playerBWall.SetActive(true);

        }else if (PlayerA.PlayerMoveB)
        {
            playerAWall.SetActive(true);
            playerBWall.SetActive(false);
        }
        else
        {
            playerAWall.SetActive(false);
            playerBWall.SetActive(false);
        }
    }
}
