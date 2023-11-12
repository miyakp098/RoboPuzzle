using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            ReloadCurrentScene();
        }
    }

    public void ReloadCurrentScene()
    {
        // 現在のシーンのインデックスを取得
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 現在のシーンを再ロード
        SceneManager.LoadScene(currentSceneIndex);
    }
}
