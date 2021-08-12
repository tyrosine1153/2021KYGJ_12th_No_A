using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueManager : MonoBehaviour
{
    [SerializeField] bool isViewwAll;
    [SerializeField] StageManager stageManager;

    private void Start()
    {
        isViewwAll = false;
    }

    public void Skip()
    {
        stageManager.Fade(true); //¥Ÿ¿Ω∏ ¿∏∑Œ
    }

    public void ViewAll()
    {
        isViewwAll = true;
    }

    public void ClickScreen()
    {
        if (isViewwAll)
        {
            stageManager.Fade(true); //¥Ÿ¿Ω∏ ¿∏∑Œ
            isViewwAll = false;
        }
    }
}
