using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DebugScene : MonoBehaviour
{
    void Update()
    {
        // JキーとKキーが同時に押された場合にシーンを切り替える
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.K))
        {
            SwitchScene();
        }
    }

    void SwitchScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(nextSceneIndex);
    }
}
