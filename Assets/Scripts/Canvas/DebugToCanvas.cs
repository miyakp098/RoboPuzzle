using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DebugToCanvas : MonoBehaviour
{
    public Text debugText; // Canvas上のTextコンポーネントへの参照
    private List<string> logMessages = new List<string>();
    private const int maxLines = 5;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // 新しいログをリストに追加
        logMessages.Add(logString);

        // リストの長さが最大行数を超えた場合、古いログを削除
        while (logMessages.Count > maxLines)
        {
            logMessages.RemoveAt(0);
        }

        // リストの内容をTextに表示
        debugText.text = string.Join("\n", logMessages);
    }
}
