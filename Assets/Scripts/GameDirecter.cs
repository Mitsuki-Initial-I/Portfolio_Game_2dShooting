using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameDirecter : MonoBehaviour
{
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
}
