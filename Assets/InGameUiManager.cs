using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUiManager : PersistentSingleton<InGameUiManager>
{
    [SerializeField] GameObject[] hearts;
    [SerializeField] Text warningText;

    [SerializeField] GameObject wave;

    [SerializeField] float beamCoolTime;
    [SerializeField] int curStageNum;

    [SerializeField] Image[] itemImages;
    private static readonly Color existColor = new Color(1, 1, 1, 1);
    private static readonly Color notExistColor = new Color(1, 1, 1, 0.5f);

    [SerializeField] private GameObject retryAsk;
    [SerializeField] private Animator gameOverAnimator;
    private static readonly int FadeIn = Animator.StringToHash("FadeIn");

    private void Start()
    {
        curStageNum = StageManager.Instance.curStageNum;

        if (curStageNum == 2) wave = GameObject.Find("Wave_group");
        
        Invoke(nameof(ItemSlotUpdate), 0.2f);


        RetryAskActive(false);
    }
    

    public void ItemSlotUpdate()
    {
        var curItem = GameManager.Instance.curItem;
        foreach (var item in curItem)
        {
            itemImages[(int) item.Key].color = item.Value ? existColor : notExistColor;
        }
    }

    public void PlayerHeartUpdate()
    {
        for (int i = 0; i < GameManager.Instance.maxHP; i++)
            hearts[i].SetActive(false);
        for (int i = 0; i < GameManager.Instance.curHP; i++)
            hearts[i].SetActive(true);
    }

    public void BeamTimer(float time)
    {
        beamCoolTime = time;
    }

    private void Update()
    {
        switch (curStageNum)
        {
            case 1:
                if(beamCoolTime >= 0)
                {
                    beamCoolTime -= Time.deltaTime;
                    warningText.text = $"태양열까지 남은시간:{Mathf.Floor(beamCoolTime)}초";
                    if(beamCoolTime < 0)
                        warningText.text = $"0초";
                }
                break;
            case 2:
                if (beamCoolTime >= 0)
                {
                    float distance = Mathf.Floor(transform.position.x - wave.transform.position.x) / 2;
                    warningText.text = $"파도와의 거리:{distance}M";
                }
                break;
        }
    }

    public void LoadMainMenuScene()
    {
        StageManager.Instance.LoadMainMenuScene();
    }

    public void LoadCurrentScene()
    {
        StageManager.Instance.LoadCurrentScene();
    }

    public void ShowGameOver()
    {
        gameOverAnimator.SetTrigger(FadeIn);
    }

    public void RetryAskActive(bool active)
    {
        retryAsk.SetActive(active);
    }
}
