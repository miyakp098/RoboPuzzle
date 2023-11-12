using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMoveSound : MonoBehaviour
{
    public AudioClip clip;
    private bool isPlaying;


    // Update is called once per frame
    void Update()
    {
        //音の処理
        // isPlayingがfalseの場合、再生を開始
        if ((PlayerA.PlayBoxMoveAudio|| PlayerB.PlayBoxMoveAudio) && !isPlaying)
        {
            GameManager.instance.PlaySE2(clip);
            isPlaying = true;
        }
        // playAudioがfalseの場合、再生を停止
        else if ((!PlayerA.PlayBoxMoveAudio && !PlayerB.PlayBoxMoveAudio) && isPlaying)
        {
            GameManager.instance.StopSE(clip);
            isPlaying = false;
        }
        
    }
}
