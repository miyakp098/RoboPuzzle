using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TitleManager : MonoBehaviour

{
    public Image fadeImage;
    public float fadeDuration = 0.5f;
    public GameObject SelectWindow;
    public GameObject enterKeySprite;

    public ButtonTextColorTMP[] buttons;
    private int currentIndex = 0;

    public GameObject Option;
    public AudioClip clip;

    private void Start()
    {
        SelectWindow.SetActive(true);
        SetActiveButton(currentIndex);
        fadeImage.color = Color.clear;
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
    }

    private void SetActiveButton(int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == index)
            {
                buttons[i].OnPointerEnter(null);
                enterKeySprite.SetActive(false);
            }
            else
            {
                buttons[i].OnPointerExit(null);
                enterKeySprite.SetActive(true);
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
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                StartCoroutine(RoadNextScene());
                Debug.Log("Button 0 clicked!");
                break;
            case 1:
                // buttons[1]の処理
                Debug.Log("Button 1 clicked!");
                Option.SetActive(true);
                this.gameObject.SetActive(false);
                break;
            
            default:
                Debug.LogError("Invalid button index!");
                break;
        }
    }


    public IEnumerator RoadNextScene()
    {
        //SelectWindow.SetActive(false);
        // 暗転
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, t / fadeDuration);
            yield return null;
        }
        fadeImage.color = Color.black;

        // 次のシーンのロード
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
