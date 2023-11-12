using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private AudioSource audioSource = null;

    private float volumeBGM = 0.5f;
    public float VolumeBGM
    {
        get { return volumeBGM; }
    }

    // 外部から音量を読み取るためのプロパティ
    public float Volume
    {
        get
        {
            if (audioSource != null)
                return audioSource.volume;
            else
                return -1; // エラー値または適切な値を返します
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySE(AudioClip clip)
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("オーディオソースが設定されていません");
        }
    }

    public void PlaySE2(AudioClip clip)
    {
        if (audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.loop = true;  // ループ再生を有効にします
            audioSource.Play();
        }
        else
        {
            Debug.Log("オーディオソースが設定されていません");
        }
    }

    public void StopSE(AudioClip clip)
    {
        if (audioSource != null && audioSource.isPlaying && audioSource.clip == clip)
        {
            audioSource.Stop();
        }
    }
    //音量調節
    public void ChangeVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = Mathf.Clamp(volume, 0f, 1f);
        }
        else
        {
            Debug.Log("オーディオソースが設定されていません");
        }
    }

    // VolumeBGMの値を変更するためのメソッド
    public void SetVolumeBGM(float value)
    {
        volumeBGM = Mathf.Clamp(value, 0f, 1f);
    }
}
