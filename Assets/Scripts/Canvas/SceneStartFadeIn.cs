using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneStartFadeIn : MonoBehaviour
{
    public Image fadeImage;        // 画面全体のImageへの参照
    public float fadeDuration = 0.5f;  // フェードの所要時間

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        // 初期状態を完全に暗転に設定
        fadeImage.color = Color.black;

        // 明るく戻る
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, 1 - (t / fadeDuration));
            yield return null;
        }

        // フェードが完了したらImageの透明度を完全に透明にする
        fadeImage.color = Color.clear;
    }
}

