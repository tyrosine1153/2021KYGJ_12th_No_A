using UnityEngine;
using UnityEngine.UI;

public class InGameUiManager : PersistentSingleton<InGameUiManager>
{
    private static readonly Color existColor = new Color(1, 1, 1, 1);
    private static readonly Color notExistColor = new Color(1, 1, 1, 0.5f);
    private static readonly int FadeIn = Animator.StringToHash("FadeIn");
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private Text warningText;

    [SerializeField] private GameObject wave;
    [SerializeField] private bool isNear;

    [SerializeField] private float beamCoolTime;
    [SerializeField] private int curStageNum;

    [SerializeField] private Image[] itemImages;

    [SerializeField] private GameObject retryAsk;
    [SerializeField] private Animator gameOverAnimator;

    private Transform _playerTransform;

    private void Start()
    {
        curStageNum = StageManager.Instance.curStageNum;

        if (curStageNum >= 6) wave = GameObject.Find("Wave_group");

        Invoke(nameof(ItemSlotUpdate), 0.2f);

        _playerTransform = PlayerScript.Instance.gameObject.GetComponent<Transform>();

        RetryAskActive(false);
    }

    private void Update()
    {
        if (curStageNum < 6)
        {
            if (beamCoolTime >= 0)
            {
                beamCoolTime -= Time.deltaTime;
                warningText.text = $"열폭풍까지 남은시간:{Mathf.Floor(beamCoolTime)}초";
                if (beamCoolTime < 0)
                    warningText.text = "0초";
            }
        }
        else
        {
            if (beamCoolTime >= 0)
            {
                var distance = Mathf.Floor(_playerTransform.position.x - wave.transform.position.x) + 7;
                warningText.text = $"파도와의 거리:{distance}M";
                if (distance <= 5)
                {
                    if (!isNear)
                    {
                        EffectSoundManager.Instance.PlayEffect(8, true);
                        isNear = true;
                    }
                }
                else if (isNear)
                {
                    isNear = false;
                }
            }
        }
    }


    public void ItemSlotUpdate()
    {
        var curItem = GameManager.Instance.CurItem;
        foreach (var item in curItem) itemImages[(int) item.Key].color = item.Value ? existColor : notExistColor;
    }

    public void PlayerHeartUpdate()
    {
        for (var i = 0; i < GameManager.Instance.maxHP; i++)
            hearts[i].SetActive(false);
        for (var i = 0; i < GameManager.Instance.curHP; i++)
            hearts[i].SetActive(true);
    }

    public void BeamTimer(float time)
    {
        beamCoolTime = time;
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