using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 0.5f;

    private bool isFading = false;

    private void Update()
    {
        if (!isFading && GameObject.Find("Player").transform.position.y < -2)
        {
            StartCoroutine(FadeAndReload());
        }
    }

    public IEnumerator FadeAndReload()
    {
        isFading = true;

        // 暗転
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, t / fadeDuration);
            yield return null;
        }
        fadeImage.color = Color.black;

        // シーンのリロード
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // 明るく戻る
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, 1 - (t / fadeDuration));
            yield return null;
        }
        fadeImage.color = Color.clear;

        isFading = false;
    }

    public IEnumerator FadeOutBlack()
    {
        
        // 暗転
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, t / fadeDuration);
            yield return null;
        }
        fadeImage.color = Color.black;
        // 次のシーンのロード
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator FadeStartScene()
    {

        // 暗転
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, t / fadeDuration);
            yield return null;
        }
        fadeImage.color = Color.black;
        // 次のシーンのロード
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene("0_StartScene");
    }

}
