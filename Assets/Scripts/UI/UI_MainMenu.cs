using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{

    public void PressStartButton()
    {
        UI.Instance.SwitchToGameUI();

    }

    public void PressExitButton()
    {
        Application.Quit();
    }
}
