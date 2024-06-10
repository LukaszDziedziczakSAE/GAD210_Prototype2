using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [field: SerializeField] public Rigidbody Rigidbody {  get; private set; }
    [field: SerializeField] public CapsuleCollider Collider { get; private set; }
    [field: SerializeField] public InputReader Input { get; private set; }
    [field: SerializeField] public Player_Movement Movement { get; private set; }
    [field: SerializeField] public Camera Camera { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);

        UI.Stats.DrawStatList();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public static Stat_Base[] Stats
    {
        get
        {
            return Instance.GetComponents<Stat_Base>();
        }
    }

    public Stat_Base GetStat(System.Type type)
    {
        foreach (Stat_Base stat in Stats)
        {
            if (stat.GetType() == type) return stat;
        }

        Debug.LogError("Player doesn't have a " + type.ToString().Replace("Stat_", "") + " stat");
        return null;
    }
}
