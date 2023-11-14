using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSound : MonoBehaviour
{
    public AudioClip clip;
    private bool isPlaying;




    private void Start()
    {
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool shouldPlay = (PlayerA.PlayerHorizontal != 0 || PlayerB.PlayerHorizontal != 0) &&
              (PlayerA.Speed != 0 || PlayerB.Speed != 0) &&
              !PauseGame.IsPaused &&
              !PlayerA.IsUsingSpell &&
              (PlayerA.PlayerMoveA || PlayerA.PlayerMoveB) &&
              (!PlayerA.IsJump) && (!PlayerB.IsJump);
        //音の処理
        // isPlayingがfalseの場合、再生を開始
        if (shouldPlay && !isPlaying)
        {
            GameManager.instance.PlaySE2(clip);
            isPlaying = true;
        }
        // playAudioがfalseの場合、再生を停止
        else if(!shouldPlay && isPlaying)
        {
            GameManager.instance.StopSE(clip);
            isPlaying = false;
        }
        
    }
}
