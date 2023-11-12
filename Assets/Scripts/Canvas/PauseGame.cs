using UnityEngine;
using UnityEngine.UI;  // UIクラスを使用するために必要

public class PauseGame : MonoBehaviour
{
    public Image dimImage;  // Unityエディタからアサインする
    public GameObject Select;
    private static bool isPaused = false;
    public static bool IsPaused
    {
        get { return isPaused; }
        set { isPaused = value; }
    }
    public GameObject SelectWindow;

    private void Start()
    {
        SelectWindow.SetActive(false);
        ResumeGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused && Select.activeInHierarchy)
            {
                ResumeGame();
            }
            else
            {
                PauseTheGame();
            }
            Debug.Log(Select.activeInHierarchy);
        }
        
    }

    void PauseTheGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        
        // 画面を薄暗くする
        Color dimColor = dimImage.color;
        dimColor.a = 0.65f;  // 透明度を50%に設定
        dimImage.color = dimColor;

        // 必要に応じて追加のコード
        SelectWindow.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        
        // 画面の薄暗さを解除
        Color dimColor = dimImage.color;
        dimColor.a = 0f;  // 透明度を0%に設定
        dimImage.color = dimColor;

        // 必要に応じて追加のコード
        SelectWindow.SetActive(false);
    }
}
