using UnityEngine;
using UnityEngine.UI;

public class DualVolumeController : MonoBehaviour
{
    private AudioSource audioSource1; // 対象のAudioSource1
    private AudioSource audioSource2; // 対象のAudioSource2
    

    public Slider volumeSlider1;     // AudioSource1の音量を調節するスライダー
    public Slider volumeSlider2;     // AudioSource2の音量を調節するスライダー
    public AudioClip clip;

    public float sliderIncrement = 0.05f; // キー入力1回あたりのスライダーの変化量

    private void Start()
    {
        audioSource1 = GetComponent<AudioSource>();
        ////スライダーをクリックできないようにする
        //volumeSlider1.interactable = false;
        //volumeSlider2.interactable = false;

        // スライダー1の初期設定
        if (volumeSlider1 && audioSource1)
        {
            audioSource1.volume = 0;
        }

        //// スライダー2の初期設定
        //if (volumeSlider2 && audioSource2)
        //{
            
        //    volumeSlider2.value = audioSource2.volume;
        //    volumeSlider2.onValueChanged.AddListener(SetVolume2);
        //}
        //this.gameObject.SetActive(false);
    }

    private void Update()
    {
        //BGMの読み込み
        volumeSlider1.value = GameManager.instance.VolumeBGM;
        audioSource1.volume = volumeSlider1.value;
        // SEのあたい読み込み
        volumeSlider2.value = GameManager.instance.Volume;

        
        if (VolumeManager.CurrentIndex == 1)
        {
            HandleKeyInputForSlider1();
        }else if(VolumeManager.CurrentIndex == 2)
        {
            HandleKeyInputForSlider2();
        }
    }

    private void HandleKeyInputForSlider1()
    {
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && volumeSlider1.value < volumeSlider1.maxValue)
        {
            volumeSlider1.value += sliderIncrement;
            GameManager.instance.SetVolumeBGM(GameManager.instance.VolumeBGM + sliderIncrement);

        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && volumeSlider1.value > volumeSlider1.minValue)
        {
            volumeSlider1.value -= sliderIncrement;
            GameManager.instance.SetVolumeBGM(GameManager.instance.VolumeBGM - sliderIncrement);
        }
    }

    private void HandleKeyInputForSlider2()
    {
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && volumeSlider2.value < volumeSlider2.maxValue)
        {
            GameManager.instance.ChangeVolume(GameManager.instance.Volume + sliderIncrement);
            GameManager.instance.PlaySE(clip);
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && volumeSlider2.value > volumeSlider2.minValue)
        {
            GameManager.instance.ChangeVolume(GameManager.instance.Volume - sliderIncrement);
            GameManager.instance.PlaySE(clip);
        }
    }

    public void SetVolume1(float volume)
    {
        audioSource1.volume = volume;
    }

    public void SetVolume2(float volume)
    {
        audioSource2.volume = volume;
    }
}
