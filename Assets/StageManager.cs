using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : PersistentSingleton<StageManager>
{
    public int curStageNum;
    [SerializeField] Text curStageText;

    [SerializeField] string[] mapNames;

    [SerializeField] Animator ani;

    public int StageData;

    private void Start()
    {
        StageData = PlayerPrefs.GetInt("StageData", 0);
        print($"Success to Load StageData! stageData : {StageData}");

    }

    public void Fade(bool isNextLv)
    {
        if (isNextLv)
        {
            curStageNum++;
            
            StageData = curStageNum;
            PlayerPrefs.SetInt("StageData", StageData);
            print($"Success to Save StageData! stageData : {StageData}");
        }

        ani.SetTrigger("Start");

        curStageText.text = mapNames[curStageNum];
    }

    public void GoToNextMap()
    {
        SceneManager.LoadScene($"Stage_{curStageNum}(min)");
    }

    // 씬 다시 시작
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 처음부터
    public void StartNewGame()
    {
        PlayerPrefs.SetInt("StageData", 1);
    }

    // 이어하기
    public void Continue()
    {
        if (StageData > 0)
        {
            SceneManager.LoadScene($"Stage_{StageData}(min)");
        }
        else
        {
            print($"StageData is wrong! StageData : {StageData}");
        }
    }
}
