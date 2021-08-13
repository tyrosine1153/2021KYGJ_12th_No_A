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

    private void Start()
    {
        curStageNum = StageManager.Instance.curStageNum;

        if (curStageNum == 2) wave = GameObject.Find("Wave_group");
    }

    public void PlayerHeartUpdate()
    {
        for (int i = 0; i < hearts.Length; i++)
            hearts[i].SetActive(true);
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
                    warningText.text = warningText.text = $"태양열까지 남은시간:{Mathf.Floor(beamCoolTime)}초";
                    if(beamCoolTime < 0)
                        warningText.text = warningText.text = $"0초";
                }
                break;
            case 2:
                if (beamCoolTime >= 0)
                {
                    float distance = Mathf.Floor(transform.position.x - wave.transform.position.x) / 2;
                    warningText.text = warningText.text = $"파도와의 거리:{distance}M";
                }
                break;
        }
    }
}
