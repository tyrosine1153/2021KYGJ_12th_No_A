using UnityEngine;
using UnityEngine.UI;

public class PrologueManager : MonoBehaviour
{
    [SerializeField] private bool isViewAll;
    [SerializeField] private StageManager stageManager;

    [SerializeField] private Sprite[] cuts;
    [SerializeField] private string[] texts;
    [SerializeField] private int cutNum;

    [SerializeField] private Image contents;
    [SerializeField] private Text text;
    [SerializeField] private GameObject touchScreentoStart;
    [SerializeField] private GameObject nextBtn;
    [SerializeField] private GameObject backBtn;

    private void Start()
    {
        isViewAll = false;
    }

    public void Next()
    {
        isViewAll = false;
        touchScreentoStart.SetActive(false);
        cutNum++;
        nextBtn.SetActive(true);
        backBtn.SetActive(true);
        if (cutNum == 6)
        {
            nextBtn.SetActive(false);
            isViewAll = true;
            touchScreentoStart.SetActive(true);
        }

        PrologSet();
    }

    public void Back()
    {
        isViewAll = false;
        touchScreentoStart.SetActive(false);
        cutNum--;
        nextBtn.SetActive(true);
        backBtn.SetActive(true);
        if (cutNum == 0) backBtn.SetActive(false);

        PrologSet();
    }

    public void PrologReset()
    {
        backBtn.SetActive(false);
        nextBtn.SetActive(true);

        contents.sprite = cuts[cutNum];
        text.text = texts[cutNum];
    }

    private void PrologSet()
    {
        contents.sprite = cuts[cutNum];
        text.text = texts[cutNum];
    }

    public void Skip()
    {
        stageManager.Fade(true); //다음맵으로
    }

    public void ViewAll()
    {
        isViewAll = true;
    }

    public void ClickScreen()
    {
        if (isViewAll)
        {
            stageManager.Fade(true); //다음맵으로
            isViewAll = false;
        }
    }
}