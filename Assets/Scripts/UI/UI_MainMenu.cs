using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{

    public void PressStartButton()
    {
        UI.Instance.SwitchToGameUI();

    }

    public void OnPressRestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void PressExitButton()
    {
        Application.Quit();
    }
}
