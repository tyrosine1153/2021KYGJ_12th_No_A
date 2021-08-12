using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject exitAskPanel;
    [SerializeField] GameObject newGamePanel;

    [SerializeField] GameObject prologueOb;

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
        prologueOb.SetActive(true);
    }
    public void Continue()
    {

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
