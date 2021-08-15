using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : PersistentSingleton<StageManager>
{
    public int curStageNum;
    [SerializeField] private Text curStageText;

    [SerializeField] private string[] mapNames;

    [SerializeField] private Animator ani;
    private static readonly int Start1 = Animator.StringToHash("Start");

    public int stageNumData;
    public StageLevel stageLevelData;  // 난이도
    
    private void Start()
    {
        stageNumData = PlayerPrefs.GetInt("StageNumData", 0);
        print($"Success to Load StageNumData! StageNumData : {stageNumData}");
        stageLevelData = (StageLevel)PlayerPrefs.GetInt("StageLevelData", 3);
        print($"Success to Load StageLevelData! StageLevelData : {stageLevelData}");
    }

    public void Fade(bool isNextLv)
    {
        if (curStageNum <= 0) curStageNum = stageNumData;
        if (isNextLv)
        {
            curStageNum++;

            stageNumData = curStageNum;
            PlayerPrefs.SetInt("StageNumData", stageNumData);
            print($"Success to Save StageNumData! stageNumData : {stageNumData}");
        }

        ani.SetTrigger(Start1);

        curStageText.text = mapNames[curStageNum];
    }

    public void SetStageLevel(int stageLevel)
    {
        stageLevelData = (StageLevel)stageLevel;
        PlayerPrefs.SetInt("StageLevelData", (int)stageLevelData);
        print($"Success to Save StageLevelData! StageLevelData : {stageLevelData}");
    }
    
    #region GameStart
    // 처음부터
    public void StartNewGame()
    {
        stageNumData = 0;
        curStageNum = stageNumData;
        PlayerPrefs.SetInt("StageNumData", stageNumData);
    }

    // 이어하기
    public void Continue()
    {
        if (stageNumData > 0)
        {
            curStageNum = stageNumData;
            Fade(false);
        }
        else
        {
            print($"StageNumData is wrong! StageNumData : {stageNumData}");
        }
    }
    #endregion

    public void GoToNextMap()
    {
        SceneManager.LoadScene(curStageNum);
    }
    
    #region LoadScene
    // 매인 메뉴
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    // 다시 시작
    public void LoadCurrentStage()
    {
        print("asdf");
        Fade(false);
    }
    
    public void LoadStage(int stageNum)
    {
        curStageNum = stageNum;
        // SceneManager.LoadScene($"Stage_{curStageNum}(min)");

        stageNumData = curStageNum;
        PlayerPrefs.SetInt("StageNumData", stageNumData);
        print($"Success to Save StageNumData! StageNumData : {stageNumData}");

        Fade(false);
    }
    #endregion
}