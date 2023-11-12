using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public ButtonTextColorTMP[] buttons;
    private int currentIndex = 0;

    public FadeController fadeController;
    public PauseGame pauseGame;
    public GameObject Option;
    public GameObject Manual;
    public AudioClip clip;

    private void Start()
    {
        SetActiveButton(currentIndex);
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)) && currentIndex > 0)
        {
            GameManager.instance.PlaySE(clip);
            currentIndex--;
            SetActiveButton(currentIndex);
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && currentIndex < buttons.Length - 1)
        {
            GameManager.instance.PlaySE(clip);
            currentIndex++;
            SetActiveButton(currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.instance.PlaySE(clip);
            ExecuteButtonAction(currentIndex); // 現在アクティブなボタンに対応する処理を実行
        }
    }

    private void SetActiveButton(int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == index)
            {
                buttons[i].OnPointerEnter(null);
            }
            else
            {
                buttons[i].OnPointerExit(null);
            }
        }
    }

    // アクティブなボタンのインデックスに応じて異なる処理を実行
    private void ExecuteButtonAction(int index)
    {
        switch (index)
        {
            case 0:
                // buttons[0]の処理
                Debug.Log("Button 0 clicked!");
                pauseGame.ResumeGame();
                break;
            case 1:
                // buttons[1]の処理
                Debug.Log("Button 1 clicked!");
                pauseGame.ResumeGame();
                fadeController.StartCoroutine(fadeController.FadeAndReload());
                break;
            case 2:
                // buttons[2]の処理
                Debug.Log("Button 2 clicked!");
                Option.SetActive(true);
                this.gameObject.SetActive(false);
                break;
            case 3:
                // buttons[3]の処理
                Debug.Log("Button 3 clicked!");
                Manual.SetActive(true);
                this.gameObject.SetActive(false);
                break;
            case 4:
                // buttons[3]の処理
                Debug.Log("Button 4 clicked!");
                pauseGame.ResumeGame();
                fadeController.StartCoroutine(fadeController.FadeStartScene());
                break;
            default:
                Debug.LogError("Invalid button index!");
                break;
        }
    }
}
