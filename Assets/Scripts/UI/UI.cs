using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }

    [SerializeField] UI_Stats stats;

    public static UI_Stats Stats => Instance.stats;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }
}
