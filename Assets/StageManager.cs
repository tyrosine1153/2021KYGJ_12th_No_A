using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : PersistentSingleton<StageManager>
{
    [SerializeField] int curStageNum;
    [SerializeField] Text curStageText;

    [SerializeField] string[] mapNames;

    [SerializeField] Animator ani;


    public void Fade(bool isNextLv)
    {
        if (isNextLv) curStageNum++;

        ani.SetTrigger("Start");

        curStageText.text = mapNames[curStageNum];
    }

    public void GoToNextMap()
    {
        SceneManager.LoadScene("Stage_" + curStageNum + "(min)");
    }
}
