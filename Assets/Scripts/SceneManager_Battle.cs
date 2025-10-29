using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager_Battle : MonoBehaviour
{
    [SerializeField]
    GameObject settingPanel;
    [SerializeField]
    TextMeshProUGUI timeText;

    float timeCount;
    int time_minutes;

    private void FixedUpdate()
    {

        timeCount += Time.deltaTime;
        if (timeCount >= 60)
        {
            time_minutes++;
            timeCount = 0;
        }

        if (time_minutes == 0)
            timeText.text = timeCount.ToString("f1");
        else
            timeText.text = $" {time_minutes}:{timeCount.ToString("00")}";
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            settingPanel.SetActive(!settingPanel.activeSelf);
    }

    public void GotoResult()
    {
        SceneChange.SceneChangeProcess(ScenesName.Result);
    }
    public void GotoEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
