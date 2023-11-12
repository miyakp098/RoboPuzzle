using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class VolumeManager : MonoBehaviour
{
    public ButtonTextColorTMP[] buttons;
    private static int currentIndex = 0;
    public static int CurrentIndex
    {
        get { return currentIndex; }
        set { currentIndex = value; }
    }
    public GameObject Select;
    public AudioClip clip;

    private void Start()
    {
        SetActiveButton(currentIndex);
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && currentIndex > 0)
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameManager.instance.PlaySE(clip);
            Select.SetActive(true);
            SetActiveButton(0);
            currentIndex = 0;
            this.gameObject.SetActive(false);
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
                Select.SetActive(true);
                this.gameObject.SetActive(false);
                break;
            case 1:
                // buttons[1]の処理
                Debug.Log("Button 1 clicked!");
                break;
            case 2:
                // buttons[2]の処理
                Debug.Log("Button 2 clicked!");
                break;
            case 3:
                // buttons[3]の処理
                Debug.Log("Button 3 clicked!");
                break;
            default:
                Debug.LogError("Invalid button index!");
                break;
        }
    }
}

