using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }

    [SerializeField] UI_Stats stats;
    [SerializeField] UI_InteractIndicator leftInteractIndicator;
    [SerializeField] UI_InteractIndicator rightInteractIndicator;
    [SerializeField] UI_ActionBar actionBar;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameUI;

    public static UI_Stats Stats => Instance.stats;
    public static UI_InteractIndicator LeftInteractIndicator => Instance.leftInteractIndicator;
    public static UI_InteractIndicator RightInteractIndicator => Instance.rightInteractIndicator;
    public static UI_ActionBar ActionBar => Instance.actionBar;
    public static bool ShowingMainMenu => Instance.mainMenu.activeSelf;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }

    private void Start()
    {
        SwitchToMainMenu();
        Player.Instance.Input.OnPauseKeyPress += PauseGame;
    }

    private void OnDisable()
    {
        Player.Instance.Input.OnPauseKeyPress -= PauseGame;
    }

    public static void ShowCursor(bool show)
    {
        if (show)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SwitchToMainMenu()
    {
        gameUI.SetActive(false);
        mainMenu.SetActive(true);
        ShowCursor(true);
    }

    public void SwitchToGameUI()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        ShowCursor(false);
        Time.timeScale = 1;
    }

    public static void PauseGame()
    {
        if (Instance.gameUI.activeSelf)
        {
            Time.timeScale = 0;
            Instance.SwitchToMainMenu();
        }
        else
        {
            Time.timeScale = 1;
            Instance.SwitchToGameUI();
        }
    }
}
