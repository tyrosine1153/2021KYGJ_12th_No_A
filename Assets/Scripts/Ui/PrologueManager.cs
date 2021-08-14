using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueManager : MonoBehaviour
{
    [SerializeField] bool isViewwAll;
    [SerializeField] StageManager stageManager;

    [SerializeField] Sprite[] cuts;
    [SerializeField] string[] texts;
    [SerializeField] int cutNum;

    [SerializeField] Image contents;
    [SerializeField] Text text;
    [SerializeField] GameObject touchScreentoStart;
    [SerializeField] GameObject nextBtn;
    [SerializeField] GameObject backBtn;
    private void Start()
    {
        isViewwAll = false;
    }

    public void Next()
    {
        isViewwAll = false;
        touchScreentoStart.SetActive(false);
        cutNum++;
        nextBtn.SetActive(true);
        backBtn.SetActive(true);
        if (cutNum == 6)
        {
            nextBtn.SetActive(false);
            isViewwAll = true;
            touchScreentoStart.SetActive(true);
        }

        PrologSet();
    }
    public void Back()
    {
        isViewwAll = false;
        touchScreentoStart.SetActive(false);
        cutNum--;
        nextBtn.SetActive(true);
        backBtn.SetActive(true);
        if (cutNum == 0)
        {
            backBtn.SetActive(false);
        }

        PrologSet();
    }
    public void PrologReset()
    {
        backBtn.SetActive(false);
        nextBtn.SetActive(true);

        contents.sprite = cuts[cutNum];
        text.text = texts[cutNum];
    }

    void PrologSet()
    {
        contents.sprite = cuts[cutNum];
        text.text = texts[cutNum];
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
