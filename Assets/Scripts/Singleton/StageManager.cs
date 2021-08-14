using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : PersistentSingleton<StageManager>
{
    public int curStageNum;
    [SerializeField] private Text curStageText;

    [SerializeField] private string[] mapNames;

    [SerializeField] private Animator ani;

    public int stageData;
    private static readonly int Start1 = Animator.StringToHash("Start");

    private void Start()
    {
        stageData = PlayerPrefs.GetInt("StageData", 0);
        print($"Success to Load StageData! stageData : {stageData}");
    }

    public void Fade(bool isNextLv)
    {
        if (curStageNum <= 0) curStageNum = stageData;
        if (isNextLv)
        {
            curStageNum++;

            stageData = curStageNum;
            PlayerPrefs.SetInt("StageData", stageData);
            print($"Success to Save StageData! stageData : {stageData}");
        }

        ani.SetTrigger(Start1);

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
        stageData = 0;
        curStageNum = stageData;
        PlayerPrefs.SetInt("StageData", stageData);
    }

    // 이어하기
    public void Continue()
    {
        if (stageData > 0)
        {
            curStageNum = stageData;
            SceneManager.LoadScene(stageData);
        }
        else
        {
            print($"StageData is wrong! StageData : {stageData}");
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

        stageData = curStageNum;
        PlayerPrefs.SetInt("StageData", stageData);
        print($"Success to Save StageData! stageData : {stageData}");

        Fade(false);
    }
}