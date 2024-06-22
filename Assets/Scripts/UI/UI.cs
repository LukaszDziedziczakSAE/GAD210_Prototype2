using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }

    [SerializeField] UI_Stats stats;
    [SerializeField] UI_InteractIndicator leftInteractIndicator;
    [SerializeField] UI_InteractIndicator rightInteractIndicator;

    public static UI_Stats Stats => Instance.stats;
    public static UI_InteractIndicator LeftInteractIndicator => Instance.leftInteractIndicator;
    public static UI_InteractIndicator RightInteractIndicator => Instance.rightInteractIndicator;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }
}
