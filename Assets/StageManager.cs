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
        if (curStageNum <= 0) curStageNum = StageData; 
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
        SceneManager.LoadScene(curStageNum);
    }

    // 씬 다시 시작
    public void LoadCurrentScene()
    {
        LoadStage(curStageNum);
    }

    // 처음부터
    public void StartNewGame()
    {
        StageData = 0;
        curStageNum = StageData;
        PlayerPrefs.SetInt("StageData", StageData);
    }

    // 이어하기
    public void Continue()
    {
        if (StageData > 0)
        {
            curStageNum = StageData;
            SceneManager.LoadScene(StageData);
        }
        else
        {
            print($"StageData is wrong! StageData : {StageData}");
        }
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    
    public void LoadStage(int stageNum)
    {
        curStageNum = stageNum;
        // SceneManager.LoadScene($"Stage_{curStageNum}(min)");

        StageData = curStageNum;
        PlayerPrefs.SetInt("StageData", StageData);
        print($"Success to Save StageData! stageData : {StageData}");
        
        Fade(false);
    }
}
