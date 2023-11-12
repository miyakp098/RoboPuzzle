using System.Collections;
using TMPro; // TextMeshProの名前空間を忘れずに
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextColorChangerLastScene : MonoBehaviour
{
    public TMP_Text textMeshPro; // InspectorからTextMeshProUGUIコンポーネントを割り当ててください
    public float duration = 0.35f; // 色の変更にかかる時間

    // Startメソッドでコルーチンを開始します
    private void Start()
    {
        // 色変更のコルーチンを開始
        StartCoroutine(ChangeTextColor());
    }

    private IEnumerator ChangeTextColor()
    {
        // 現在の時刻を記録
        float startTime = Time.time;

        // 黒から白への変化
        while (Time.time - startTime < duration)
        {
            // 経過時間の割合を計算
            float t = (Time.time - startTime) / duration;

            // 色を徐々に変更
            textMeshPro.color = Color.Lerp(Color.black, Color.white, t);

            yield return null; // 次のフレームまで待機
        }

        // 1秒間待機
        yield return new WaitForSeconds(1f);

        // 白から黒への変化
        startTime = Time.time; // 現在の時刻をリセット
        while (Time.time - startTime < duration)
        {
            // 経過時間の割合を計算
            float t = (Time.time - startTime) / duration;

            // 色を徐々に変更
            textMeshPro.color = Color.Lerp(Color.white, Color.black, t);

            yield return null; // 次のフレームまで待機
        }
        Debug.Log("aaa");
        SceneManager.LoadScene("0_StartScene");
    }
}

