using UnityEditor;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject exitAskPanel;
    [SerializeField] private GameObject newGamePanel;

    [SerializeField] private GameObject prologueOb;

    public void newGamePanelAct(bool active)
    {
        newGamePanel.SetActive(active);
    }

    public void ExitPanelAct(bool active)
    {
        exitAskPanel.SetActive(active);
    }

    public void StartNewGame()
    {
        StageManager.Instance.StartNewGame();
        prologueOb.SetActive(true);
    }

    public void Continue()
    {
        StageManager.Instance.Continue();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}