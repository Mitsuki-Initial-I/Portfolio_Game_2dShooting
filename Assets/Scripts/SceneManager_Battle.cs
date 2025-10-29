using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct EnemyGenerateBlueprint
{
    public MoveType moveType;   // 移動パターン
    public int phase;           // フェーズ
    public int posPoint;        // 出現場所
}

public class SceneManager_Battle : MonoBehaviour
{
    [SerializeField]
    GameObject settingPanel;
    [SerializeField]
    TextMeshProUGUI timeText;
    [SerializeField]
    List<EnemyGenerateBlueprint> generateData = new List<EnemyGenerateBlueprint>();
    [SerializeField]
    GameObject enemyObjPrefab;
    [SerializeField]
    int phaseMaxNum;

    const float PHASEMAXTIME = 40f;
    const int FRAMERATE = 60;
    List<GameObject> activeEnemy = new List<GameObject>();
    Vector3 posNumData = Vector2.zero;
    float timeCount;
    float timer;
    float posOffset = 10f;
    int time_minutes;
    int phaseCount;
    bool phaseRunning = false;
    bool enemygeneratorCheck = false;



    void GetCamraScale()
    {
        Camera cam = Camera.main;
        Vector3 worldTopRigh;
        Vector3 worldBottomLeft;
        float depth = cam.transform.position.z * -1;
        Vector3 bottomLeft = new Vector3(0, 0, depth);
        worldBottomLeft = cam.ScreenToWorldPoint(bottomLeft);

        Vector3 topRight = new Vector3(Screen.width, Screen.height, depth);
        worldTopRigh = cam.ScreenToWorldPoint(topRight);

        posNumData = new Vector3(worldTopRigh.x,worldTopRigh.y,Math.Abs(worldBottomLeft.y - worldTopRigh.y) / 10);

        Debug.Log(posNumData);
    }

    IEnumerator GenerateEnemyProcess()
    {
        for (int i = 0; i < phaseMaxNum; i++)
        {
            phaseCount = i + 1;
            enemygeneratorCheck = false;
            timer = 0f;
            Debug.Log("フェーズ"+ phaseCount);
            yield return StartCoroutine(StartPhase(phaseCount));
        }
    }

    IEnumerator StartPhase(int phase)
    {
        Debug.Log("フェーズ" + phaseCount+"の処理開始");
        phaseRunning = true;
        foreach (var prefab in generateData)
        {
            if (prefab.phase == phase)
            {
                var enemy = Instantiate(enemyObjPrefab);
                var enemy_move = enemy.AddComponent<EnemyMoveController>();
                enemy.transform.position = new Vector3(posNumData.x+ posOffset, posNumData.y - posNumData.z * prefab.posPoint, 0);
                enemy_move.spawner = this;
                enemy_move.moveType = prefab.moveType;
                enemy_move.deadPoint = -(posNumData.x + posOffset);
                activeEnemy.Add(enemy);
            }
            yield return null;
        }
        enemygeneratorCheck = true;
        timer = timeCount + (FRAMERATE* time_minutes);
        while (phaseRunning)
        {
            if (activeEnemy.Count == 0)
            {
                break;
            }
            if ((timeCount + (FRAMERATE * time_minutes)) >= timer + PHASEMAXTIME)
            {
                break;
            }
            yield return null;
        }
        Debug.Log("フェーズ" + phaseCount + "の処理終了");
        yield return new WaitForSeconds(1);
    }

    public void EnemyDeath(GameObject enemy)
    {
        if (activeEnemy.Contains(enemy))
        {
            activeEnemy.Remove(enemy);
        }
    }

    private void Start()
    {
        GetCamraScale();
        StartCoroutine(GenerateEnemyProcess());
    }
    private void FixedUpdate()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= FRAMERATE)
        {
            time_minutes++;
            timeCount -= FRAMERATE;
        }

        if (time_minutes == 0)
            timeText.text = timeCount.ToString("f1");
        else
            timeText.text = $" {time_minutes}:{timeCount.ToString("00")}";

    }

    private void Update()
    {
        if ((phaseCount == phaseMaxNum && (timeCount + (FRAMERATE * time_minutes)) >= timer+ PHASEMAXTIME) || (phaseCount == phaseMaxNum && activeEnemy.Count == 0 && enemygeneratorCheck))
        {
            Debug.Log("フェーズカウント:" + phaseCount);
            Debug.Log("フェーズ最大:" + phaseMaxNum);
            Debug.Log("フェーズ時間:" + timer);
            Debug.Log("敵の数:" + activeEnemy.Count);
            GotoResult();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            settingPanel.SetActive(!settingPanel.activeSelf);
        }
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